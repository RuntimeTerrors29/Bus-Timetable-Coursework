using BusTimetable.Models;

namespace BusTimetable.DataStructures
{

// we decided to use hash table to store bus stops, we are using seperate chaining to avoid collision
    public class BusStopHashTable
    {
        private class ChainNode
        {
            public BusStop Data;
            public ChainNode? Next;

            public ChainNode(BusStop data)
            {
                Data = data;
                Next = null;
            }
        }

        private readonly ChainNode?[] _buckets;
        private readonly int _size;
        private int _count;

        public int Count => _count;

        public BusStopHashTable(int size = 64)
        {
            _size    = size;
            _buckets = new ChainNode[_size];
            _count   = 0;
        }

        // hash function - mod the ID by table size to get bucket index
        // time complexity: O(1)
        private int Hash(int stopId)
        {
            return Math.Abs(stopId % _size); // Fixed a bug that causing negative index.
        }

        // add a stop to the table, if a stop already exists it updates rather than duplicating it
        // time complexity: O(1) average, O(n) worst case
        public void Add(BusStop stop)
        {
            int index = Hash(stop.StopID);
            ChainNode? current = _buckets[index];

            while (current != null)
            {
                if (current.Data.StopID == stop.StopID)
                {
                    current.Data = stop;
                    return;
                }
                current = current.Next;
            }

            var newNode = new ChainNode(stop);
            newNode.Next    = _buckets[index];
            _buckets[index] = newNode;
            _count++;
        }

        // find a stop by ID
        // time complexity: O(1) average
        public BusStop? GetById(int stopId)
        {
            int index = Hash(stopId);
            ChainNode? current = _buckets[index];

            while (current != null)
            {
                if (current.Data.StopID == stopId)
                    return current.Data;
                current = current.Next;
            }
            return null;
        }

        // search by exact name, not case sensitive so it has to be exact match
        // O(n) - must scan all buckets
        public BusStop? GetByName(string name)
        {
            for (int i = 0; i < _size; i++)
            {
                ChainNode? current = _buckets[i];
                while (current != null)
                {
                    if (current.Data.StopName.Equals(name, StringComparison.OrdinalIgnoreCase))
                        return current.Data;
                    current = current.Next;
                }
            }
            return null;
        }
    }
}
