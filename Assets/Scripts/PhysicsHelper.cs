using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public static class PhysicsHelper
    {
        public static Vector3 GetDirectionByKeyboard()
        {
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                return Vector3.forward;
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                return Vector3.back;
            }

            if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            {
                return Vector3.left;
            }

            if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            {
                return Vector3.right;
            }

            return Vector3.zero;
        }

        public static void Rotate(MonoBehaviour gameObjectBehavior, Vector3 vector3, Enums.TypeOfVector3 typeOfVector)
        {
            switch (typeOfVector)
            {
                case Enums.TypeOfVector3.Direction:
                    if (vector3 != Vector3.zero)
                    {
                        gameObjectBehavior.transform.rotation = Quaternion.LookRotation(vector3);
                    }
                    break;
                case Enums.TypeOfVector3.NextPosition:
                    gameObjectBehavior.transform.LookAt(vector3);
                    break;
            }
        }

        public static bool CharacterCapsuleCast(MonoBehaviour gameObjectBehavior)
        {
            float height = 2.0f;
            float radius = 0.5f;
            float castDistance = 0.1f;
            int mask = (1 << 2);
            Vector3 point1 = gameObjectBehavior.transform.position + Vector3.up * -height * 0.5f;
            Vector3 point2 = point1 + Vector3.up * height;

            point1.y += radius;
            point2.y -= radius;

            if (Physics.CapsuleCast(point1, point2, radius, gameObjectBehavior.transform.forward, castDistance,
                ~mask))
                return true;

            return false;
        }

        public static bool CharacterCapsuleCast(MonoBehaviour gameObjectBehavior, Vector3 direction)
        {
            float height = 2.0f;
            float radius = 0.5f;
            float castDistance = 0.1f;
            int mask = (1 << 2);
            Vector3 point1 = gameObjectBehavior.transform.position + Vector3.up * -height * 0.5f;
            Vector3 point2 = point1 + Vector3.up * height;

            point1.y += radius;
            point2.y -= radius;

            if (Physics.CapsuleCast(point1, point2, radius, direction, castDistance, ~mask))
                return true;

            return false;
        }

        public static bool CharacterSphereCast(MonoBehaviour gameObjectBehavior)
        {
            float radius = 0.3f;
            float castDistance = 0.2f;
            int mask = (1 << 2);
            RaycastHit rayCastHit;

            Vector3 point = gameObjectBehavior.transform.position;
            point.x = (float)Math.Round(point.x, 1);
            point.z = (float)Math.Round(point.z, 1);
            point.y = radius;

            if (Physics.SphereCast(point, radius, gameObjectBehavior.transform.forward, out rayCastHit,
                castDistance, ~mask))
            {
                return true;
            }

            return false;
        }

        public static bool CharacterSphereCast(MonoBehaviour gameObjectBehavior, Vector3 direction)
        {
            float radius = 0.3f;
            float castDistance = 0.2f;
            int mask = (1 << 2);
            RaycastHit rayCastHit;

            Vector3 point = gameObjectBehavior.transform.position;
            point.x = (float)Math.Round(point.x, 1);
            point.z = (float)Math.Round(point.z, 1);
            point.y = radius;

            if (Physics.SphereCast(point, radius, direction, out rayCastHit, castDistance, ~mask))
            {
                return true;
            }

            return false;
        }
    }
}
