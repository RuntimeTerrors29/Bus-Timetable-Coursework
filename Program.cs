using BusTimetable.Database;
using BusTimetable.DataStructures;
using BusTimetable.Menu;

Console.WriteLine("=== Bus Timetable & Ticketing System ===");
Console.WriteLine();

Console.Write("Enter database path (press Enter for default 'bus.db'): ");
string input  = Console.ReadLine()?.Trim() ?? "";
string dbPath = string.IsNullOrEmpty(input) ? "bus.db" : input;

Console.WriteLine($"\nUsing database: {dbPath}");
Console.WriteLine("Loading data...");

var db        = new DatabaseManager(dbPath);
var stops     = new BusStopHashTable();
var timetable = new TimetableList();
var tickets   = new TicketList();

db.EnsureSchema();
db.SeedIfEmpty();
db.LoadStops(stops);
db.LoadSchedules(timetable);
db.LoadTickets(tickets);
var passengers = db.LoadPassengers();

Console.WriteLine($"  Loaded {stops.Count} stops, {timetable.Count} schedules, " +
                  $"{tickets.Count} tickets, {passengers.Count} passengers.");

var menu = new MenuController(stops, timetable, tickets, passengers, db);
menu.Run();

Console.WriteLine("\nGoodbye!");
