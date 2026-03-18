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
            _stops      = stops;
            _timetable  = timetable;
            _tickets    = tickets;
            _passengers = passengers;
            _db         = db;
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
                    case "0": running = false; break;
                    default:
                        Console.WriteLine("  Invalid option, please try again.");
                        break;
                }
            }
        }

        private static void ShowMainMenu()
        {
            Console.WriteLine("\n--- Main Menu ---");
            Console.WriteLine("  1. View timetable");
            Console.WriteLine("  2. Search bus stops");
            Console.WriteLine("  0. Exit");
            Console.Write("\nSelect option: ");
        }

        private void ViewTimetable()
        {
            var all = _timetable.GetAll();
            Console.WriteLine($"\n  {all.Count} scheduled services:\n");
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
    }
}
