using BusTimetable.Database;
using BusTimetable.DataStructures;
using BusTimetable.Models;

namespace BusTimetable.Menu
{
    // handles all user interaction with the menu
    // keeps refs to data structures and DB so changes stay in sync
    public class MenuController
    {
        private readonly BusStopHashTable _stops;
        private readonly TimetableList _timetable;
        private readonly TicketList _tickets;
        private readonly PassengerList _passengers;
        private readonly DatabaseManager _db;

        // Constructor takes all data structures and database manager as dependencies
        public MenuController(BusStopHashTable stops, TimetableList timetable, TicketList tickets, PassengerList passengers, DatabaseManager db)
        {
            _stops = stops;
            _timetable = timetable;
            _tickets = tickets;
            _passengers = passengers;
            _db = db;
        }

        // loops until user picks exit
        public void Run()
        {
            Console.WriteLine("\n=== Bus Timetable & Ticketing System ===\n");

            bool running = true;
            while (running)
            {
                ShowMainMenu();
                string choice = Console.ReadLine()?.Trim() ?? "";

                switch (choice)
                {
                    case "1": ViewTimetable(); break;
                    case "2": SearchStops(); break;
                    case "3": BookTicket(); break;
                    case "4": CancelTicket(); break;
                    case "5": CheckTicketStatus(); break;
                    case "6": AdminMenu(); break;
                    case "0": running = false; break;
                    default:
                        Console.WriteLine("  Invalid option, please try again.");
                        break;
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
            Console.WriteLine("  5. Check ticket status");
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
            if (string.IsNullOrEmpty(name)) { Console.WriteLine("  Name cannot be empty."); return; }

            Console.Write("  Enter your email: ");
            string email = Console.ReadLine()?.Trim() ?? "";

            Console.Write("  Enter Schedule ID to book: ");
            if (!int.TryParse(Console.ReadLine(), out int schedId)) { Console.WriteLine("  Please enter a valid number."); return; }

            var schedule = _timetable.GetById(schedId);
            if (schedule == null) { Console.WriteLine($"  No schedule found with ID {schedId}."); return; }
            if (schedule.IsFull) { Console.WriteLine("  Sorry, that service is fully booked."); return; }

            int passId = _db.AddPassenger(name, email);
            int ticketId = _db.AddBooking(passId, schedId, 3.50m);
            _passengers.Add(new Passenger(passId, name, email));
            schedule.SeatsBooked++;

            var ticket = new Ticket(ticketId, passId, name, schedId, schedule.RouteName,
                                    DateTime.Now, 3.50m);
            _tickets.Add(ticket);

            Console.WriteLine($"\n  Booking confirmed! Ticket #{ticketId} — {schedule.RouteName} at {schedule.DepartureTime:hh\\:mm}");
        }

        // Method to cancel a ticket by ID, with error handling and seat count adjustment
        private void CancelTicket()
        {
            Console.Write("\n  Enter Ticket ID to cancel: ");
            if (!int.TryParse(Console.ReadLine(), out int ticketId)) { Console.WriteLine("  Please enter a valid number."); return; }

            var ticket = _tickets.GetById(ticketId);
            if (ticket == null) { Console.WriteLine($"  No ticket found with ID {ticketId}."); return; }

            bool cancelled = _tickets.Cancel(ticketId);
            if (!cancelled) { Console.WriteLine("  Could not cancel — this ticket may already be cancelled."); return; }

            var schedule = _timetable.GetById(ticket.ScheduleID);
            if (schedule != null) schedule.SeatsBooked--;

            _db.CancelBooking(ticketId, ticket.ScheduleID);
            Console.WriteLine($"  Ticket #{ticketId} has been cancelled.");
        }

        private void CheckTicketStatus()
        {
            Console.Write("\n  Enter Ticket ID: ");
            if (!int.TryParse(Console.ReadLine(), out int ticketId)) { Console.WriteLine("  Please enter a valid number."); return; }

            var ticket = _tickets.GetById(ticketId);
            if (ticket == null) { Console.WriteLine($"  No ticket found with ID {ticketId}."); return; }

            Console.WriteLine($"\n  Ticket #{ticket.TicketID}");
            Console.WriteLine($"  Passenger : {ticket.PassengerName}");
            Console.WriteLine($"  Route     : {ticket.ScheduleInfo}");
            Console.WriteLine($"  Booked on : {ticket.BookingDate:dd MMM yyyy HH:mm}");
            Console.WriteLine($"  Price     : £{ticket.Price:0.00}");
            Console.WriteLine($"  Status    : {ticket.Status}");
        }

        private void AdminMenu()
        {
            ShowAdminMenu(); // extracted into own method - mirrors ShowMainMenu pattern
            string choice = Console.ReadLine()?.Trim() ?? "";
            if (choice == "1") UpdateCapacity();
        }

        // extracted from AdminMenu() for consistency with ShowMainMenu()
        private static void ShowAdminMenu()
        {
            Console.WriteLine("\n--- Admin Menu ---");
            Console.WriteLine("  1. Update schedule capacity");
            Console.WriteLine("  0. Back");
            Console.Write("\nSelect: ");
        }

        // Updates the max passenger capacity for a scheduled service
        private void UpdateCapacity()
        {
            Console.Write("\n  Enter Schedule ID: ");
            if (!int.TryParse(Console.ReadLine(), out int schedId)) { Console.WriteLine("  Please enter a valid number."); return; }

            var schedule = _timetable.GetById(schedId);
            if (schedule == null) { Console.WriteLine($"  No schedule found with ID {schedId}."); return; }

            Console.Write($"  Current capacity: {schedule.Capacity}. New capacity: ");
            if (!int.TryParse(Console.ReadLine(), out int newCap) || newCap < schedule.SeatsBooked)
            {
                Console.WriteLine($"  Invalid — new capacity must be at least {schedule.SeatsBooked} (seats already booked).");
                return;
            }

            schedule.Capacity = newCap;
            _timetable.Update(schedule);
            _db.UpdateSchedule(schedule);
            Console.WriteLine($"  Schedule #{schedId} capacity updated to {newCap}.");
        }

        private int GetOrCreatePassenger(string name, string email)
        {
            int passId = _db.AddPassenger(name, email);
            _passengers.Add(new Passenger(passId, name, email));
            return passId;
        }
    }
}
