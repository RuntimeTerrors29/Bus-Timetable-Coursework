using BusTimetable.models;

namespace BusTimetable.DataStructures
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


        
        private Node?  head;
        private int count;

        public int Count => count;

        public void InsertSorted(Schedule schedule)
        {
            var newNode = new Node(schedule);

            if (head == null || schedule.DepartureTime <= head.Data.DepartureTime)
            {
                newNode.Next = head;
                head = newNode;
                count++;
                return ;
            }

            var current  = head;
            while (current.Next != null &&
                   current.Next.Data.DepartureTime <= schedule.DepartureTime)
            {
                current = current.Next;
            }

            newNode.Next = current.Next;
            current.Next = newNode;
            count++;
        }

        // removes a schedule by its ID, returns true if removed, false if not found
        public bool Remove(int scheduleId)
        {
            if (head == null) return false;

            if   (head.Data.ScheduleID == scheduleId)
            {
                head =  head.Next;
                count--;
                return true;
            }

            var  current  = head;
            while (current.Next != null)
            {
                if (current.Next.Data.ScheduleID == scheduleId)
                {
                    current.Next = current.Next.Next;
                    count--;
                    return true;
                }
                current = current.Next;
            }
            return false;
        }

        public  Schedule ? GetById(int scheduleId)
        {
            var  current = head;
            while (current  != null)
            {
                if (current.Data.ScheduleID == scheduleId)
                    return  current.Data;
                current = current.Next;
            }
            return  null;
        }
        // get schedules for a specific route ID

        public  Schedule[] GetByRoute(int routeId)
        {
            int matchCount = 0;
            var current = head;
            while (current != null)
            {
                if  (current.Data.RouteID == routeId) matchCount++;
                current = current.Next;
            }

            var   results = new Schedule[matchCount];
            int i = 0;
            current = head;
            while  (current != null)
            {
                if (current.Data.RouteID == routeId)
                    results[i++] = current.Data;
                current = current.Next;
            }
            return results;
        }

        // get schedules from a given time onwards
        public  Schedule[] GetFromTime(TimeSpan fromTime)
        {
            int  matchCount = 0;
            var current = head;
            while (current != null)
            {
                if (current.Data.DepartureTime >= fromTime) matchCount++;
                current = current.Next;
            }

            var results = new Schedule[matchCount];
            int  i =  0;
            current = head ;
            while (current != null )
            {
                if (current.Data.DepartureTime >= fromTime)
                    results[i++] = current.Data;
                current = current.Next;
            }

            return  results;
        }


        // get schedules between two times
        
        public Schedule[] GetBetween(TimeSpan from, TimeSpan to)
        {
            int matchCount = 0;
            var current = head;
            while (current != null)
            {
                var departureTime = current.Data.DepartureTime;
                if (departureTime >= from && departureTime <= to) matchCount++;
                if (departureTime > to) break;
                current = current.Next;
            }

            var results =  new Schedule[matchCount];
            int i = 0;
            current = head;
            while (current != null)
            {
                var departureTime = current.Data.DepartureTime;
                if (departureTime >= from && departureTime <= to)
                    results[i++] = current.Data;
                if (departureTime > to) break;
                current = current.Next;
            }
            return  results;
        }

        // returns all schedules in  the list


        public Schedule[] GetAll()
        {
            var all = new Schedule[count];
            var current = head;
            int i = 0;

            while (current != null)
            {
                all[i++] = current.Data;
                current = current.Next;
            }
            return  all;
        }

        // update an existing schedule by its ID, returns true if updated, false if not found
        public bool Update(Schedule updated)
        {
            var  current = head;
            while (current != null)
            {
                if (current.Data.ScheduleID ==  updated.ScheduleID)
                {
                    current.Data = updated;
                    return true;
                }
                current  = current.Next;
            }
            return false;
        }
    }
}

