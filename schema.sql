-- Database Schema - Bus Timetable System

-- Stores individual bus stops with their name, address, and GPS coordinates
CREATE TABLE IF NOT EXISTS BusStops (
    StopID INTEGER PRIMARY KEY AUTOINCREMENT,
    StopName TEXT NOT NULL,
    Location TEXT NOT NULL DEFAULT '',   --readable address
    Latitude REAL NOT NULL DEFAULT 0.0,  --GPS latitude
    Longitude REAL NOT NULL DEFAULT 0.0   --GPS longitude
);

-- Stores named bus routes with an optional description
CREATE TABLE IF NOT EXISTS BusRoutes (

    RouteID INTEGER PRIMARY KEY AUTOINCREMENT,
    RouteName TEXT NOT NULL,
    Description TEXT NOT NULL DEFAULT ''

);

-- Maps stops to routes, defining their order and travel details between consecutive stops
CREATE TABLE IF NOT EXISTS RouteStops (

    RouteID  INTEGER NOT NULL REFERENCES BusRoutes(RouteID),
    StopID INTEGER NOT NULL REFERENCES BusStops(StopID),
    NextStopID INTEGER,   -- NULL if this is the last stop on  the route
    StopOrder  INTEGER NOT NULL,    -- Position of the stop along the route
    DistanceKm REAL NOT NULL DEFAULT 0.0, -- Distance to the next stop in kilometres
    TravelMins INTEGER NOT NULL DEFAULT 0,   -- Estimated travel time to the next stop
    PRIMARY KEY (RouteID, StopID)
);

-- Stores departure/arrival times for a route and seat availability
CREATE TABLE IF NOT EXISTS Schedules (
    ScheduleID  INTEGER PRIMARY KEY AUTOINCREMENT,
    RouteID  INTEGER NOT NULL REFERENCES BusRoutes(RouteID),
    DepartureTime TEXT NOT NULL,  -- format: HH:MM
    ArrivalTime TEXT NOT NULL,  -- format: HH:MM
    Capacity INTEGER NOT NULL DEFAULT 50,  -- number of seats on the bus
    SeatsBooked  INTEGER NOT NULL DEFAULT 0   --number of seats already reserved
);

-- Stores passenger details
CREATE TABLE IF NOT EXISTS Passengers (

    PassengerID INTEGER PRIMARY KEY AUTOINCREMENT,
    FullName TEXT NOT NULL,
    Email TEXT NOT NULL DEFAULT ''  -- passenger email
);

-- Stores ticket bookings for passengers on a schedule
CREATE TABLE IF NOT EXISTS Tickets (
    TicketID    INTEGER PRIMARY KEY AUTOINCREMENT,
    PassengerID INTEGER NOT NULL REFERENCES Passengers(PassengerID),
    ScheduleID  INTEGER NOT NULL REFERENCES Schedules(ScheduleID),
    BookingDate TEXT  NOT NULL,  -- when booked
    Price  REAL NOT NULL DEFAULT 0.0,  -- ticket price
    Status TEXT NOT NULL DEFAULT 'Active'  -- active/cancelled
);
