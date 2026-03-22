# Bus Timetable & Ticketing System
CST2550 Group Coursework — Middlesex University London

## Overview
A console application for managing bus timetables and ticket bookings.
Built with C# .NET 8 and SQLite. Uses custom data structures throughout (no STL collections).

## Tech Stack
- Language: C# .NET 8
- Database: SQLite (via Microsoft.Data.Sqlite 8.0.0)
- Testing: MSTest 3.1.1
- Data Structures: Custom hash table, sorted linked list, singly linked lists

## How to Run
```bash
dotnet restore
dotnet run --project BusTimetable
```
Enter the path to your SQLite database when prompted, or press Enter for default (bus.db).

## How to Test
```bash
dotnet test
```

## Project Structure
```
BusTimetable/
  Models/          - BusStop, BusRoute, Schedule, Ticket, Passenger
  DataStructures/  - BusStopHashTable, TimetableList, TicketList, PassengerList
  Database/        - DatabaseManager, schema.sql, sample_data.sql
  Menu/            - MenuController
  Program.cs
BusTimetable.Tests/
  BusRouteTests.cs, BusStopTests.cs, ScheduleTests.cs
  PassengerTests.cs, TicketTests.cs, DataStructuresTests.cs
```

## Data Structures
| Structure | Backing | Use |
|---|---|---|
| BusStopHashTable | Separate chaining, array of linked lists | O(1) stop lookup by ID |
| TimetableList | Sorted single linked list | Departure-ordered schedules |
| TicketList | Single linked list | Ticket storage and lookup |
| PassengerList | Single linked list | Passenger storage |
