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
    }
}
