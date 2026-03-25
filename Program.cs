using BusTimetable.Database;
using BusTimetable.DataStructures;
using BusTimetable.Menu;

Console.WriteLine("=== Bus Timetable & Ticketing System ===");
Console.WriteLine("CST2550 Group Project — Middlesex University\n");

Console.Write("Enter database path (press Enter for default 'bus.db'): ");
string input  = Console.ReadLine()?.Trim() ?? "";
string dbPath = string.IsNullOrEmpty(input) ? "bus.db" : input;

Console.WriteLine($"\nConnecting to: {dbPath}");
Console.WriteLine("Loading data, please wait...");

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

Console.WriteLine($"\n  {stops.Count} stops loaded");
Console.WriteLine($"  {timetable.Count} schedules loaded");
Console.WriteLine($"  {tickets.Count} tickets loaded");
Console.WriteLine($"  {passengers.Count} passengers loaded");

var menu = new MenuController(stops, timetable, tickets, passengers, db);
menu.Run();

Console.WriteLine("\nThank you for using the Bus Timetable System. Goodbye!");
