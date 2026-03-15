# Bus Timetable & Ticketing System
CST2550 Group Coursework — Middlesex University London

## Overview
A console application for managing bus timetables and ticket bookings.
Built with C# .NET 8 and SQLite. Uses custom data structures (no STL collections).

## Tech Stack
- Language: C# .NET 8
- Database: SQLite (via Microsoft.Data.Sqlite)
- Testing: MSTest
- Data Structures: Custom hash table, linked lists (no List<T> or Dictionary<T>)

## How to Run
1. Install .NET 8 SDK
2. Run: `dotnet run --project BusTimetable`
3. Enter the path to your SQLite database when prompted (or press Enter for default)

## Project Structure
```
BusTimetable/
  Models/          - Domain models (BusStop, BusRoute, Schedule, Ticket, Passenger)
  DataStructures/  - Custom data structures (BusStopHashTable, TimetableList, TicketList, PassengerList)
  Database/        - DatabaseManager, schema.sql, sample_data.sql
  Menu/            - MenuController (all user interaction)
BusTimetable.Tests/  - Unit tests (MSTest)
```
