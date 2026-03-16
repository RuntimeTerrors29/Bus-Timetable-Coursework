using BusTimetable.Models;

namespace BusTimetable.DataStructures
{
    // simple linked list for passengers
    public class PassengerList
    {
        private class Node
        {
            public Passenger Data;
            public Node? Next;
            public Node(Passenger data) { Data = data; Next = null; }
        }

        private Node? _head;

        public void Add(Passenger passenger)
        {
            var node = new Node(passenger) { Next = _head };
            _head = node;
        }

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