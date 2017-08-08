using Assets.Scripts.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class SimpleKeyboardMove : ICharacterBehavior
    {
        private int speed;
        private bool wallPass;
        private Vector3 direction;

        public bool CanMove(CharacterBase gameObjectBehavior)
        {
            direction = Helper.GetDirectionByKeyboard();

            return direction != Vector3.zero;
        }

        public void Move(CharacterBase gameObjectBehavior)
        {
            if (CanMove(gameObjectBehavior))
            {
                RotateAndMove(gameObjectBehavior);
            }
        }

        private Vector3 GetMaxPosition()
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

        private bool IsBorder(Vector3 position, Vector3 direction)
        {
            Vector3 maxPosition = GetMaxPosition();

            if (position.x <= 0 && direction == Vector3.left)
                return true;
            if (position.z <= 0 && direction == Vector3.back)
                return true;
            if (position.x >= maxPosition.x - 1 && direction == Vector3.right)
                return true;
            if (position.z >= maxPosition.z - 1 && direction == Vector3.forward)
                return true;

            return false;
        }

        private void RotateAndMove(CharacterBase gameObjectBehavior)
        {
            speed = gameObjectBehavior.GetSpeed();
            wallPass = gameObjectBehavior.CanWallPass();

            Helper.Rotate(gameObjectBehavior, direction, Helper.TypeOfVector3.Direction);

            if (!wallPass)
            {
                if (!Helper.CharacterCapsuleCast(gameObjectBehavior))
                {
                    gameObjectBehavior.transform.Translate(direction * Time.deltaTime * speed, Space.World);
                }
            }
            else if (!IsBorder(gameObjectBehavior.transform.position, direction))
            {
                gameObjectBehavior.transform.Translate(direction * Time.deltaTime * speed, Space.World);
            }
        }
    }
}
