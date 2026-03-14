using BusTimetable.models;

namespace BusTimetable.DataStructures
{
    


    
    public  class TimetableList
    {
        // Linked list implementation for storing bus schedules, with methods for adding, removing, and retrieving schedules
        private class Node
        {
            public Schedule Data;
            public  Node? Next;

            public Node(Schedule data)
            {
                Data  = data;
                Next  = null;
            }
        }


        

        private Node?  head;
        private int  count;

        public int  Count => count;


    }
}