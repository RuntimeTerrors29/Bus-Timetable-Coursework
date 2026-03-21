using BusTimetable.Database;
using BusTimetable.DataStructures;
using BusTimetable.Menu;

const string dbPath = "bus.db";

Console.WriteLine("Bus Timetable & Ticketing System");
Console.WriteLine("Starting up...");

var db        = new DatabaseManager(dbPath);
var stops     = new BusTimetable.DataStructures.BusStopHashTable();
var timetable = new BusTimetable.DataStructures.TimetableList();
var tickets   = new BusTimetable.DataStructures.TicketList();

db.EnsureSchema();
db.SeedIfEmpty();
db.LoadStops(stops);
db.LoadSchedules(timetable);
db.LoadTickets(tickets);
var passengers = db.LoadPassengers();

var menu = new MenuController(stops, timetable, tickets, passengers, db);
menu.Run();

Console.WriteLine("\nGoodbye!");
