using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Algorithms
{
    public class BinaryHeap
    {
        private List<Node> list;

        public BinaryHeap(int capacity)
        {
            list = new List<Node>(capacity);
        }

        public int Count
        {
            get { return list.Count; }
        }

        public void Add(Node value)
        {
            list.Add(value);

            int valueIndex = Count - 1;
            int parentIndex = (valueIndex - 1) / 2;

            while (valueIndex > 0 && list[valueIndex].F < list[parentIndex].F ||
                (valueIndex > 0 && list[valueIndex].F == list[parentIndex].F && 
                list[valueIndex].h < list[parentIndex].h))
            {
                Node temp = list[valueIndex];
                list[valueIndex] = list[parentIndex];
                list[parentIndex] = temp;

                valueIndex = parentIndex;
                parentIndex = (valueIndex - 1) / 2;
            }
        }

        public Node GetRoot()
        {
            Node result = list[0];
            list.RemoveAt(0);
            return result;
        }

        public bool Contains(Node node)
        {
            foreach (var item in list)
            {
                if (IsSamePositions(item.position, node.position))
                    return true;
            }

            return false;
        }

        private static bool IsSamePositions(Vector3 current, Vector3 next)
        {
            bool x = (int)Math.Round(current.x) == (int)Math.Round(next.x);
            bool z = (int)Math.Round(current.z) == (int)Math.Round(next.z);

            return x && z;
        }
    }
}
