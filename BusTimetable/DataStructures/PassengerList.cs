using BusTimetable.Models;

namespace BusTimetable.DataStructures
{
    // simple linked list for passengers
    // same pattern as TicketList, just for passengers
    // only need Add and GetById so keeping it simple
    public class PassengerList
    {
        private class Node
        {
            public Passenger Data;
            public Node? Next;
            public Node(Passenger data) { Data = data; Next = null; }
        }

        private Node? _head;
        private int _count;

        public int Count => _count;

        // O(1)
        public void Add(Passenger passenger)
        {
            var node = new Node(passenger) { Next = _head };
            _head = node;
            _count++;
        }

        // O(n)
        public Passenger? GetById(int passengerId)
        {
            var current = _head;
            while (current != null)
            {
                if (current.Data.PassengerID == passengerId)
                    return current.Data;
                current = current.Next;
            }
            return null;
        }
    }
}
