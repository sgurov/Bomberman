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
        private Animator animator;

        public bool CanMove(CharacterBase gameObjectBehavior)
        {
            if (Input.GetKey(KeyCode.Space) && gameObjectBehavior.GetBombs() > 0)
                return false;

            direction = PhysicsHelper.GetDirectionByKeyboard();

            return direction != Vector3.zero;
        }

        public void Move(CharacterBase gameObjectBehavior)
        {
            animator = gameObjectBehavior.gameObject.GetComponent<Animator>();
            if (CanMove(gameObjectBehavior))
            {
                speed = gameObjectBehavior.GetSpeed();
                animator.SetFloat("Speed", speed);
                RotateAndMove(gameObjectBehavior);
            }
            else
            {
                animator.SetFloat("Speed", 0.0f);
            }
        }

        private bool IsBorder(Vector3 position, Vector3 direction)
        {
            Vector3 maxPosition = Helper.GetMaxPosition();

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
            wallPass = gameObjectBehavior.CanWallPass();

            PhysicsHelper.Rotate(gameObjectBehavior, direction, Enums.TypeOfVector3.Direction);

            //if (!IsBorder(gameObjectBehavior.transform.position, direction))
            //{
                if (wallPass)
                {
                    gameObjectBehavior.transform.Translate(direction * Time.deltaTime * speed, Space.World);
                    //gameObjectBehavior.transform.position += direction * Time.deltaTime * speed;
                }
                else if (!PhysicsHelper.CharacterSphereCast(gameObjectBehavior))
                {
                    gameObjectBehavior.transform.Translate(direction * Time.deltaTime * speed, Space.World);
                    //gameObjectBehavior.transform.position += direction * Time.deltaTime * speed;
                }
            //}
        }
    }
}
