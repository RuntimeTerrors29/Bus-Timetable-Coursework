using BusTimetable.models;

namespace  BusTimetable.DataStructures
{
    public class TimetableList
    {
         private class Node
        {
            public Schedule Data;
            public Node? Next;

            public Node(Schedule data)
            {
                Data = data;
                Next = null;
            }
        }

        // head of the linked list and count of schedules
        private Node? head;
        private int count;

        public int Count => count;

        // insert into correct position to keep list sorted by departure time
        public void InsertSorted(Schedule schedule)
        {
            var newNode = new Node(schedule);

            if (head == null || schedule.DepartureTime <= head.Data.DepartureTime)
            {
                newNode.Next = head;
                head = newNode;
                count++;
                return;
            }

            var current = head;
            while (current.Next != null &&
                   current.Next.Data.DepartureTime <= schedule.DepartureTime)
            {
                current = current.Next;
            }

            newNode.Next = current.Next;
            current.Next = newNode;
            count++;
        }

        // remove a schedule by its ID, returns true if found and removed
        public bool Remove(int scheduleId)
        {
            if (head == null) return  false;

            if  (head.Data.ScheduleID ==  scheduleId)
            { 
                head = head.Next;
                count--;
                return true;
            }


            // traverse the list to find the schedule to remove
            var current = head;

            while  (current.Next != null)
            {
                if  (current.Next.Data.ScheduleID == scheduleId)
                {
                    current.Next = current.Next.Next;
                    count--;
                    return true;
                }
                current = current.Next;
            }
             return false;
        }
    }
}

