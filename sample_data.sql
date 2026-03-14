-- Sample Data :Bus stops and routes only
-- using a mix of central London locations
INSERT INTO BusStops (StopName, Location, Latitude, Longitude) VALUES
    ('Victoria Station',   'Westminster',    51.4952, -0.1441),
    ('Westminster Bridge', 'Westminster',    51.5007, -0.1246),
    ('Waterloo Station',   'Lambeth',        51.5031, -0.1132),
    ('London Bridge',      'Southwark',      51.5055, -0.0877),
    ('Bank',               'City of London', 51.5133, -0.0886),
    ('Liverpool Street',   'Hackney',        51.5178, -0.0823),
    ('Paddington Station', 'Westminster',    51.5154, -0.1755),
    ('Marble Arch',        'Westminster',    51.5130, -0.1588),
    ('Oxford Circus',      'Westminster',    51.5154, -0.1419),
    ('King''s Cross',      'Islington',      51.5308, -0.1238),
    ('Angel Islington',    'Islington',      51.5322, -0.1058),
    ('Elephant & Castle',  'Southwark',      51.4940, -0.1003);

-- routes that connect some of these stops
-- note: King''s has double quote escape because of apostrophe
INSERT INTO BusRoutes (RouteName, Description) VALUES
    ('Route 1 - Victoria to Bank',           'Express via Westminster and Waterloo'),
    ('Route 2 - Paddington to Liverpool St', 'Cross-city via Oxford Circus'),
    ('Route 3 - King''s Cross to Elephant',  'South London via City'),
    ('Route 4 - Victoria to Oxford Circus',  'Short hop via Westminster');
