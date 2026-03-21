using BusTimetable.Database;
using BusTimetable.DataStructures;
using BusTimetable.models;

namespace BusTimetable.Menu
{
    public class MenuController
    {


        private readonly BusStopHashTable _stops;
        private readonly TimetableList _timetable;
        private readonly TicketList _tickets;
        private readonly PassengerList _passengers;
        private readonly DatabaseManager _db;


        // Constructor takes all data structures and database manager as dependencies


        public MenuController(BusStopHashTable stops, TimetableList timetable,
                              TicketList tickets, PassengerList passengers, DatabaseManager db)
        {
            _stops = stops; _timetable = timetable;
            _tickets = tickets; _passengers = passengers; _db = db;
        }

        public void Run ()
        {

            Console.WriteLine("\n===  Bus Timetable & Ticketing System ===\n");
            bool running = true;
            while (running)
            {
                ShowMainMenu ();
                string choice =  Console.ReadLine()?.Trim() ?? "";
                switch (choice)
                {
                    case "1": ViewTimetable(); break;
                    case "2": SearchStops(); break;
                    case "3": BookTicket(); break;
                    case "4": CancelTicket(); break;
                    case "5": ViewMyTickets(); break;
                    case "6": AdminMenu(); break;
                    case "0": running = false; break;
                    default: Console.WriteLine("  Invalid  option, please try  again."); break;
                }
            }
        }


        // Private methods for each menu action
        private static void ShowMainMenu()
        {
            Console.WriteLine("\n--- Main Menu ---");
            Console.WriteLine("  1. View timetable");
            Console.WriteLine("  2. Search bus stops");
            Console.WriteLine("  3. Book a ticket");
            Console.WriteLine("  4. Cancel a ticket");
            Console.WriteLine("  5. View my tickets");
            Console.WriteLine("  6. Admin");
            Console.WriteLine("  0. Exit");
            Console.Write("\nSelect option: ");
        }


        private void ViewTimetable()
        {


            var all = _timetable.GetAll();
            Console.WriteLine($"\n  {all.Length} scheduled services:\n");
            foreach (var s in all)
                Console.WriteLine($"  {s}");
        }


        // Method to search bus stops by name and display results
        private void SearchStops()
        {
            Console.Write("\n  Enter stop name to search: ");
            string query = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrEmpty(query)) return;
            var results = _stops.SearchByName(query);
            Console.WriteLine($"\n  Found {results.Length} stop(s):");
            foreach (var stop in results)
                Console.WriteLine($"  {stop}");
        }

        private void BookTicket()
        {
            Console.Write("\n  Enter your full name: ");

            string name = Console.ReadLine()?.Trim() ?? "";
            Console.Write("  Enter your email: ");
            string email = Console.ReadLine()?.Trim() ?? "";
            Console.Write("  Enter Schedule ID to book: ");
            if (!int.TryParse(Console.ReadLine(), out int schedId)) { Console.WriteLine("  Invalid ID."); return; }
            var schedule = _timetable.GetById(schedId);
            if (schedule == null) { Console.WriteLine("  Schedule not found."); return; }
            if (schedule.IsFull) { Console.WriteLine("  Sorry, that service is fully booked."); return; }
            int passId = _db.AddPassenger(name, email);
            int ticketId = _db.AddBooking(passId, schedId, 3.50m);
            _passengers.Add(new Passenger(passId, name, email));
            schedule.SeatsBooked++;
            _tickets.Add(new Ticket(ticketId, passId, name, schedId, schedule.RouteName, DateTime.Now, 3.50m));
            Console.WriteLine($"\n  Booked! Ticket #{ticketId} — {schedule.RouteName} at {schedule.DepartureTime:hh\\:mm}");
        }


        // Method to cancel a ticket by ID, with error handling and seat count adjustment
        private void CancelTicket()
        {
            Console.Write("\n  Enter Ticket ID to cancel: ");

            if (!int.TryParse(Console.ReadLine(), out int ticketId)) { Console.WriteLine("  Invalid ID."); return; }
            var ticket = _tickets.GetById(ticketId);
            if (ticket == null) { Console.WriteLine("  Ticket not found."); return; }
            bool cancelled = _tickets.Cancel(ticketId);
            if (!cancelled) { Console.WriteLine("  Could not cancel (already cancelled?)."); return; }

            // fix for Bug 9 - decrement seat count when cancelling
            var schedule = _timetable.GetById(ticket.ScheduleID);
            if (schedule != null) schedule.SeatsBooked--;

            _db.CancelBooking(ticketId, ticket.ScheduleID);
            Console.WriteLine($"  Ticket #{ticketId} cancelled.");
        }

        private void ViewMyTickets()
        {

            Console.Write("\n  Enter Passenger ID: ");
            if (!int.TryParse(Console.ReadLine(), out int passId)) { Console.WriteLine("  Invalid ID."); return; }
            var myTickets = _tickets.GetByPassenger(passId);
            if (myTickets.Length == 0) { Console.WriteLine("  No tickets found."); return; }
            Console.WriteLine($"\n  Found {myTickets.Length} ticket(s):");
            foreach (var t in myTickets)
                Console.WriteLine($"  {t}");
        }


        // Placeholder for admin menu, can be expanded with actual admin features later
        private void AdminMenu()
        {

            Console.WriteLine("\n--- Admin Menu ---");
            Console.WriteLine("  Admin features coming soon.");
        }
    }
}


