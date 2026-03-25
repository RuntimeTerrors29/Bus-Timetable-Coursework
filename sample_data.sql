-- Sample Data for Database

-- Bus Stops (London area with real coordinates)
INSERT INTO BusStops (StopName, Location, Latitude, Longitude) VALUES
    ('Victoria Station',      'Westminster',     51.4952, -0.1441),
    ('Westminster Bridge',    'Westminster',     51.5007, -0.1246),
    ('Waterloo Station',      'Lambeth',         51.5031, -0.1132),
    ('London Bridge',         'Southwark',       51.5055, -0.0877),
    ('Bank',                  'City of London',  51.5133, -0.0886),
    ('Liverpool Street',      'Hackney',         51.5178, -0.0823),
    ('Paddington Station',    'Westminster',     51.5154, -0.1755),
    ('Marble Arch',           'Westminster',     51.5130, -0.1588),
    ('Oxford Circus',         'Westminster',     51.5154, -0.1419),
    ('King''s Cross',         'Islington',       51.5308, -0.1238),
    ('Angel Islington',       'Islington',       51.5322, -0.1058),
    ('Elephant & Castle',     'Southwark',       51.4940, -0.1003);

-- Bus Routes Available and that will be added into the database
INSERT INTO BusRoutes (RouteName, Description) VALUES
    ('Route 1 - Victoria to Bank',           'Express via Westminster and Waterloo'),
    ('Route 2 - Paddington to Liverpool St', 'Cross-city via Oxford Circus'),
    ('Route 3 - King''s Cross to Elephant',  'South London via City'),
    ('Route 4 - Victoria to Oxford Circus',  'Short hop via Westminster');

-- Route Stops — Route 1 (Victoria → Westminster → Waterloo → London Bridge → Bank)
INSERT INTO RouteStops (RouteID, StopID, NextStopID, StopOrder, DistanceKm, TravelMins) VALUES
    (1, 1, 2, 1, 1.2, 5),
    (1, 2, 3, 2, 0.9, 4),
    (1, 3, 4, 3, 1.5, 7),
    (1, 4, 5, 4, 0.6, 3),
    (1, 5, NULL, 5, 0.0, 0);

-- Route Stops — Route 2 (Paddington → Marble Arch → Oxford Circus → Bank → Liverpool St)
INSERT INTO RouteStops (RouteID, StopID, NextStopID, StopOrder, DistanceKm, TravelMins) VALUES
    (2, 7, 8, 1, 1.2, 5),
    (2, 8, 9, 2, 1.0, 5),
    (2, 9, 5, 3, 1.3, 6),
    (2, 5, 6, 4, 0.9, 4),
    (2, 6, NULL, 5, 0.0, 0);

-- Route Stops — Route 3 (King's Cross → Angel → Bank → London Bridge → Elephant)
INSERT INTO RouteStops (RouteID, StopID, NextStopID, StopOrder, DistanceKm, TravelMins) VALUES
    (3, 10, 11, 1, 0.9, 4),
    (3, 11, 5,  2, 2.1, 10),
    (3, 5,  4,  3, 0.6, 3),
    (3, 4,  12, 4, 1.0, 5),
    (3, 12, NULL, 5, 0.0, 0);

-- Route Stops — Route 4 (Victoria → Westminster → Oxford Circus)
INSERT INTO RouteStops (RouteID, StopID, NextStopID, StopOrder, DistanceKm, TravelMins) VALUES
    (4, 1, 2, 1, 1.2, 5),
    (4, 2, 9, 2, 1.7, 8),
    (4, 9, NULL, 3, 0.0, 0);

-- Schedules — Route 1
INSERT INTO Schedules (RouteID, DepartureTime, ArrivalTime, Capacity, SeatsBooked) VALUES
    (1, '07:00:00', '07:19:00', 50, 0),
    (1, '07:30:00', '07:49:00', 50, 0),
    (1, '08:00:00', '08:19:00', 50, 0),
    (1, '08:30:00', '08:49:00', 50, 0),
    (1, '09:00:00', '09:19:00', 60, 0),
    (1, '12:00:00', '12:19:00', 60, 0),
    (1, '17:00:00', '17:25:00', 60, 0),
    (1, '18:00:00', '18:25:00', 60, 0);

-- Schedules — Route 2
INSERT INTO Schedules (RouteID, DepartureTime, ArrivalTime, Capacity, SeatsBooked) VALUES
    (2, '07:15:00', '07:35:00', 45, 0),
    (2, '08:00:00', '08:20:00', 45, 0),
    (2, '09:00:00', '09:20:00', 55, 0),
    (2, '13:00:00', '13:20:00', 55, 0),
    (2, '17:30:00', '17:50:00', 55, 0);

-- Schedules — Route 3
INSERT INTO Schedules (RouteID, DepartureTime, ArrivalTime, Capacity, SeatsBooked) VALUES
    (3, '07:45:00', '08:07:00', 40, 0),
    (3, '09:30:00', '09:52:00', 40, 0),
    (3, '14:00:00', '14:22:00', 50, 0),
    (3, '18:30:00', '18:52:00', 50, 0);

-- Schedules — Route 4
INSERT INTO Schedules (RouteID, DepartureTime, ArrivalTime, Capacity, SeatsBooked) VALUES
    (4, '08:10:00', '08:23:00', 35, 0),
    (4, '10:00:00', '10:13:00', 35, 0),
    (4, '15:00:00', '15:13:00', 35, 0);

-- Sample Passengers
INSERT INTO Passengers (FullName, Email) VALUES
    ('Alice Johnson',  'alice@example.com'),
    ('Bob Smith',      'bob@example.com'),
    ('Carol Williams', 'carol@example.com'),
    ('David Brown',    'david@example.com'),
    ('Emma Davis',     'emma@example.com');

-- Sample Tickets
INSERT INTO Tickets (PassengerID, ScheduleID, BookingDate, Price, Status) VALUES
    (1, 1,  '2026-02-17T08:00:00', 3.50, 'Active'),
    (2, 3,  '2026-02-17T07:30:00', 3.50, 'Active'),
    (3, 9,  '2026-02-17T09:00:00', 3.50, 'Active'),
    (4, 13, '2026-02-17T10:00:00', 2.75, 'Active'),
    (5, 17, '2026-02-17T11:00:00', 2.75, 'Cancelled');

-- Update seat counts to match ticket bookings
UPDATE Schedules SET SeatsBooked = 1 WHERE ScheduleID IN (1, 3, 9, 13);
