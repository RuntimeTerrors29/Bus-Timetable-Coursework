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
            Console.WriteLine("  0. Exit");
            Console.Write("\nSelect option: ");
        }
    }
}
