using Assets.Scripts.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public static class Helper
    {
        public static Vector3 GetRandomDirection()
        {
            int random = UnityEngine.Random.Range(0, 4);

            switch (random)
            {
                case 0:
                    return Vector3.forward;
                case 1:
                    return Vector3.back;
                case 2:
                    return Vector3.left;
                case 3:
                    return Vector3.right;
            }

            return Vector3.zero;
        }

        public static Vector3 GetMaxPosition()
        {
            GameObject[] concreteWalls = GameObject.FindGameObjectsWithTag("Concrete");
            float x = concreteWalls[0].transform.position.x;
            float z = concreteWalls[0].transform.position.z;

            foreach (var wall in concreteWalls)
            {
                if (wall.transform.position.x > x)
                    x = wall.transform.position.x;
                if (wall.transform.position.z > z)
                    z = wall.transform.position.z;
            }

            return new Vector3(x, 0.0f, z);
        }
    }
}
