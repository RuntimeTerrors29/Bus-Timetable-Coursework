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
	// for some reason returns negative value which causes indexoutofrangeexception(on _buckets[index]) will look into it.
        private int Hash(int stopId)
        {
            return stopId % _size;
        }

        // add a stop to the table
        // if same ID already exists, just update it instead of duplicating
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
            newNode.Next     = _buckets[index];
            _buckets[index]  = newNode;
            _count++;
        }
    }
}
