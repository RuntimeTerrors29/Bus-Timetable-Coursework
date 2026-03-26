using BusTimetable.models;

namespace BusTimetable.DataStructures
{
    public class TicketList
    {
        private  class Node
        {
            public Ticket Data;
            public  Node? Next;

            public Node(Ticket data)
            {
                Data  = data;
                Next = null;
            }
        }

        // A simple linked list to store tickets. Not optimized for search, but meets the requirement of not using built-in collections

        private Node? head;
        private int count;



        public int Count => count;

        public void Add(Ticket ticket)
        {
            var node = new Node(ticket);
            node.Next = head;
            head = node;
            count++;
        }

       
        public  Ticket? GetById(int ticketId)
        {
            var current = head;
            while (current  !=  null)
            {
                if (current.Data.TicketID == ticketId)
                    return current.Data;
                current =  current.Next;
            }
            return  null;
        }


        // Returns an array of tickets for the given passenger ID. If no tickets are found, returns an empty array.
        public Ticket[] GetByPassenger(int passengerId)
        {
            int matchCount = 0;
            var current = head;
            while (current != null)
            {
                if (current.Data.PassengerID == passengerId) matchCount++;
                current = current.Next;
            }

            var results = new Ticket[matchCount];
            int  i = 0;
            current = head;
            while  (current != null)
            {
                if  (current.Data.PassengerID == passengerId)
                    results[i++] = current.Data;
                current = current.Next;
            }
            return  results;
        }

        // returns false if ticket not found or already cancelled

        public bool  Cancel(int ticketId)
        {
            var current = head;
            while (current !=  null)
            {
                if (current.Data.TicketID == ticketId)
                {

                    if (current.Data.Status == "Cancelled")
                        return false;
                    current.Data.Status = "Cancelled";
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        // Returns all tickets in the list. If the list is empty, returns an empty array.
        public Ticket[] GetAll()
        {
            var all = new Ticket[count];
            var current = head;
            int i = 0;

            while (current != null)
            {
                all[i++]  = current.Data;
                current  = current.Next;
            }
            return all;
        }
    }
}
