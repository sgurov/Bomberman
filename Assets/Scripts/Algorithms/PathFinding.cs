using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Algorithms
{
    public static class PathFinding
    {
        public static void FindPath(List<Node> path, Node startNode, Node targetNode)
        {
            BinaryHeap open = new BinaryHeap(1000);
            List<Node> close = new List<Node>();
            open.Add(startNode);

            while (open.Count > 0)
            {
                Node currentNode = open.GetRoot();
                close.Add(currentNode);

                if (IsSamePositions(currentNode.position, targetNode.position))
                {
                    CreatePath(path, startNode, currentNode);
                    return;
                }

                foreach (var neighbor in GetNeighbors(currentNode))
                {
                    if (Contains(close, neighbor))
                        continue;

                    double movementCostToNeighbor = currentNode.g + GetDistance(currentNode, neighbor);
                    if (movementCostToNeighbor < neighbor.g || !open.Contains(neighbor))
                    {
                        neighbor.g = movementCostToNeighbor;
                        neighbor.h = GetDistance(neighbor, targetNode);
                        neighbor.parent = currentNode;
                    }

                    if (!open.Contains(neighbor))
                        open.Add(neighbor);
                }
            }
        }

        private static bool Contains(List<Node> list, Node item)
        {
            foreach (var element in list)
            {
                if (IsSamePositions(element.position, item.position))
                    return true;
            }

            return false;
        }

        private static double GetDistance(Node start, Node finish)
        {
            return Math.Abs(start.position.x - finish.position.x) + Math.Abs(start.position.z - finish.position.z);
        }

        private static void CreatePath(List<Node> path, Node start, Node finish)
        {
            //path = new List<Node>();
            Node currentNode = finish;

            while (currentNode != start)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            path.Reverse();
        }

        private static List<Node> GetNeighbors(Node node)
        {
            float castDistance = 1.0f;
            List<Node> neighbors = new List<Node>();
            RaycastHit raycastHit;

            if (!Physics.Raycast(node.position, Vector3.forward, out raycastHit, castDistance))
                neighbors.Add(new Node(node.position + Vector3.forward));
            else if (raycastHit.collider.tag == "Player" || raycastHit.collider.tag == "Enemy")
                neighbors.Add(new Node(node.position + Vector3.forward));
            if (!Physics.Raycast(node.position, Vector3.back, out raycastHit, castDistance))
                neighbors.Add(new Node(node.position + Vector3.back));
            else if (raycastHit.collider.tag == "Player" || raycastHit.collider.tag == "Enemy")
                neighbors.Add(new Node(node.position + Vector3.back));
            if (!Physics.Raycast(node.position, Vector3.left, out raycastHit, castDistance))
                neighbors.Add(new Node(node.position + Vector3.left));
            else if (raycastHit.collider.tag == "Player" || raycastHit.collider.tag == "Enemy")
                neighbors.Add(new Node(node.position + Vector3.left));
            if (!Physics.Raycast(node.position, Vector3.right, out raycastHit, castDistance))
                neighbors.Add(new Node(node.position + Vector3.right));
            else if (raycastHit.collider.tag == "Player" || raycastHit.collider.tag == "Enemy")
                neighbors.Add(new Node(node.position + Vector3.right));

            return neighbors;
        }

        private static bool IsSamePositions(Vector3 current, Vector3 next)
        {
            bool x = (int)Math.Round(current.x) == (int)Math.Round(next.x);
            bool z = (int)Math.Round(current.z) == (int)Math.Round(next.z);

            return x && z;
        }

    }
}
