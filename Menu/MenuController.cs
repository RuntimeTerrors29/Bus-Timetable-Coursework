using BusTimetable.Database;
using BusTimetable.DataStructures;
using BusTimetable.Models;

namespace BusTimetable.Menu
{
    // handles all user interaction with the menu.
    // keeps refs to data structures and DB so changes stay in sync.
    public class MenuController
    {
        private readonly BusStopHashTable _stops;
        private readonly TimetableList _timetable;
        private readonly TicketList _tickets;
        private readonly PassengerList _passengers;
        private readonly DatabaseManager _db;

        public MenuController(BusStopHashTable stops, TimetableList timetable,
                              TicketList tickets, PassengerList passengers, DatabaseManager db)
        {
            _stops = stops; _timetable = timetable;
            _tickets = tickets; _passengers = passengers; _db = db;
        }
        // loops until the user picks exit
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
                    case "2": SearchStops();   break;
                    case "3": BookTicket();    break;
                    case "0": running = false; break;
                    default: Console.WriteLine("  Invalid option, please try again."); break;
                }
            }
        }

        private static void ShowMainMenu()
        {
            Console.WriteLine("\n--- Main Menu ---");
            Console.WriteLine("  1. View timetable");
            Console.WriteLine("  2. Search bus stops");
            Console.WriteLine("  3. Book a ticket");
            Console.WriteLine("  0. Exit");
            Console.Write("\nSelect option: ");
        }

        private void ViewTimetable()
        {
            var all = _timetable.GetAll();
            Console.WriteLine($"\n  {all.Length} scheduled services:\n"); // ✅ FIXED Bug #5: .Length not .Count
            foreach (var s in all)
                Console.WriteLine($"  {s}");
        }

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
            if (schedule.IsFull)  { Console.WriteLine("  Sorry, that service is fully booked."); return; }

            int passId   = _db.AddPassenger(name, email);
            int ticketId = _db.AddBooking(passId, schedId, 3.50m);

            _passengers.Add(new Passenger(passId, name, email));
            schedule.SeatsBooked++;

            var ticket = new Ticket(ticketId, passId, name, schedId, schedule.RouteName,
                                    DateTime.Now, 3.50m);
            _tickets.Add(ticket);

            Console.WriteLine($"\n  Booked! Ticket #{ticketId} — {schedule.RouteName} at {schedule.DepartureTime:hh\\:mm}");
        }
    }
}
