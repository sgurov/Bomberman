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

        public static void Rotate(MonoBehaviour gameObjectBehavior, Vector3 vector3, TypeOfVector3 typeOfVector)
        {
            switch (typeOfVector)
            {
                case TypeOfVector3.Direction:
                    if (vector3 != Vector3.zero)
                    {
                        gameObjectBehavior.transform.rotation = Quaternion.LookRotation(vector3);
                    }
                    break;
                case TypeOfVector3.NextPosition:
                    gameObjectBehavior.transform.LookAt(vector3);
                    break;
            }
        }

        public static bool CharacterCapsuleCast(MonoBehaviour gameObjectBehavior)
        {
            float castDistance = 0.1f;
            int mask = (1 << 2);
            CharacterController characterController = gameObjectBehavior.GetComponent<CharacterController>();
            Vector3 point1 = gameObjectBehavior.transform.position + characterController.center + Vector3.up *
                -characterController.height * 0.5f;
            Vector3 point2 = point1 + Vector3.up * characterController.height;

            point1.y += characterController.radius;
            point2.y -= characterController.radius;

            if (Physics.CapsuleCast(point1, point2, characterController.radius, 
                gameObjectBehavior.transform.forward, castDistance, ~mask))
                return true;

            return false;
        }

        public static bool CharacterCapsuleCast(MonoBehaviour gameObjectBehavior, Vector3 direction)
        {

            float castDistance = 0.1f;
            int mask = (1 << 2);
            CharacterController characterController = gameObjectBehavior.GetComponent<CharacterController>();
            Vector3 point1 = gameObjectBehavior.transform.position + characterController.center + Vector3.up *
                -characterController.height * 0.5f;
            Vector3 point2 = point1 + Vector3.up * characterController.height;

            point1.y += characterController.radius;
            point2.y -= characterController.radius;

            if (Physics.CapsuleCast(point1, point2, characterController.radius, direction, castDistance, ~mask))
                return true;

            return false;
        }

        public enum TypeOfVector3
        {
            Direction,
            NextPosition
        }

        public static void SetGameOverText()
        {
            StaticObjectsGeneratorBase objectsGenerator;
            objectsGenerator = new StaticObjectsGenerator();
            Canvas canvas = objectsGenerator.GetCanvas();
            UnityEngine.Object.Instantiate(canvas);
        }
    }
}
