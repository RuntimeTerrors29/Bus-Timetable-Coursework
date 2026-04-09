utreah - Ugur Ersoy
Tz0r - Luis Oliveira 
stefan291201 - Florin Stefan Grosu
AkamMo159 - Akram Mohamed 
Bogdan Nicolescu - Bogdan Nicolescu

# Bus Timetable & Ticketing System
CST2550 Group Coursework Middlesex University London

## What is this?
A console app for managing bus timetables and ticket bookings, built for our CST2550 group coursework. Uses C# .NET 8 with SQLite for storage. All data structures (hash table, linked lists) are written from scratch no built-in collections like `List<T>` or `Dictionary`.

## Requirements

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8)
- Visual Studio 2022 or just the `dotnet` CLI

Check your version:
```bash
dotnet --version
```

## NuGet Packages

These get restored automatically when you run `dotnet restore`, nothing to install manually.

**Main project (BusTimetable.csproj)**
- `Microsoft.Data.Sqlite` 10.0.3

**Test project (BusTimetable.Tests.csproj)**
- `MSTest.TestFramework` 3.1.1
- `MSTest.TestAdapter` 3.1.1
- `Microsoft.NET.Test.Sdk` 17.8.0

## Running the app

```bash
dotnet restore
dotnet run --project BusTimetable.csproj
```

On first run it creates a `bus.db` SQLite file and seeds it with the data from `sample_data.sql`.

## Running the tests

```bash
dotnet test "BusTimetable.Tests/BusTimetable.Tests.csproj"
```

For per-test output:
```bash
dotnet test "BusTimetable.Tests/BusTimetable.Tests.csproj" --logger "console;verbosity=detailed"
```

To run a specific class:
```bash
dotnet test --filter "ClassName=DataStructuresTests"
```

## Project structure

```
Bus-Timetable-Coursework/
├── BusTimetable.csproj
├── BusTimetable.sln
├── Program.cs
├── Models/
│   ├── BusStop.cs
│   ├── BusRoute.cs
│   ├── Schedule.cs
│   ├── Ticket.cs
│   └── Passenger.cs
├── DataStructures/
│   ├── BusStopHashTable.cs       # hash table, separate chaining
│   ├── TimetableList.cs          # sorted linked list by departure time
│   ├── TicketList.cs
│   └── PassengerList.cs
├── Database/
│   ├── DatabaseManager.cs
│   ├── schema.sql
│   └── sample_data.sql
├── Menu/
│   └── MenuController.cs
└── BusTimetable.Tests/
    ├── DataStructuresTests.cs
    ├── BusStopTests.cs
    ├── BusRouteTests.cs
    ├── ScheduleTests.cs
    ├── TicketTests.cs
    └── PassengerTests.cs
```

## Data structures used

| Structure | How it works | Used for |
|---|---|---|
| BusStopHashTable | Separate chaining, array of linked lists | Stop lookup by ID — O(1) avg |
| TimetableList | Sorted singly linked list | Schedules ordered by departure time |
| TicketList | Singly linked list | Ticket storage and cancellation |
| PassengerList | Singly linked list | Passenger storage |
