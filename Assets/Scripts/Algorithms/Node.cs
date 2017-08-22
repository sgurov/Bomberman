using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Algorithms
{
    public class Node
    {
        public Node parent;
        public Vector3 position;
        public double g;
        public double h;

        public Node(Vector3 position)
        {
            this.position = position;
        }

        public double F
        {
            get { return g + h; }
        }
    }

}
