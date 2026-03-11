-- Table for storing the bus stop information
CREATE TABLE IF NOT EXISTS BusStops (
    
    StopID INTEGER PRIMARY KEY AUTOINCREMENT,  --Unique stop identifier
    StopName TEXT NOT NULL,               --Name of the stop
    Location TEXT NOT NULL DEFAULT '',    --Address/location  description
    Latitude REAL NOT NULL DEFAULT 0.0,   --GPS latitude
    Longitude REAL NOT NULL DEFAULT 0.0   --GPS longitude

);
--Table for storing the bus route information
CREATE TABLE IF NOT EXISTS BusRoutes  (

    RouteID  INTEGER PRIMARY KEY AUTOINCREMENT,  --UNique route identifier
    RouteName TEXT NOT NULL,              --Route name/number
    Description TEXT NOT NULL DEFAULT ''  --Route description
);
