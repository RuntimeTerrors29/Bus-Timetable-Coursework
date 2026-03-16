using BusTimetable.Models;

namespace BusTimetable.DataStructures
{
    // simple linked list for tickets, no sorting needed.
    public class TicketList
    {
        private class Node
        {
            public Ticket Data;
            public Node? Next;
            public Node(Ticket data) { Data = data; Next = null; }
        }

        private Node? _head;
        private int _count;
        public int Count => _count;

        // O(1) - prepend to front 
        public void Add(Ticket ticket)
        {
            var node = new Node(ticket) { Next = _head };
            _head = node;
            _count++;
        }

        // O(n) - linear scan
        public Ticket? GetById(int ticketId)
        {
            var current = _head;
            while (current != null)
            {
                if (current.Data.TicketID == ticketId)
                    return current.Data;
                current = current.Next;
            }
            return null;
        }

        // two-pass: count then fill - avoids List<T>
        // O(n)
        public Ticket[] GetByPassenger(int passengerId)
        {
            int matchCount = 0;
            var current = _head;
            while (current != null)
            {
                if (current.Data.PassengerID == passengerId) matchCount++;
                current = current.Next;
            }

            var results = new Ticket[matchCount];
            int i = 0;
            current = _head;
            while (current != null)
            {
                if (current.Data.PassengerID == passengerId)
                    results[i++] = current.Data;
                current = current.Next;
            }
            return results;
        }

        public bool Cancel(int ticketId)
        {
            var current = _head;
            while (current != null)
            {
                if (current.Data.TicketID == ticketId)
                {
                    current.Data.Status = "Cancelled";
                    return true; 
                }
                current = current.Next;
            }
            return false;
        }

        // this is flatten to an array, O(n)
        public Ticket[] GetAll()
        {
            var all     = new Ticket[_count];
            var current = _head;
            int i       = 0;
            while (current != null)
            {
                all[i++] = current.Data;
                current  = current.Next;
            }
            return all;
        }
    }
}
