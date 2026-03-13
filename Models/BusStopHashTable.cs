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
            _size   = size;
            _buckets = new ChainNode[_size];
            _count  = 0;
        }
    }
}
