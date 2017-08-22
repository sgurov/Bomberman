using Assets.Scripts.BaseClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Behaviors
{
    public class RandomMove : ICharacterBehavior
    {
        private int speed;
        private bool changeDirection = true, continueDirection = false;
        private Vector3 currentDirection, nextDirection;
        private Vector3 nextPosition;
        private Animator animator;

        public bool CanMove(CharacterBase gameObjectBehavior)
        {
            return true;
        }

        public void Move(CharacterBase gameObjectBehavior)
        {
            animator = gameObjectBehavior.gameObject.GetComponent<Animator>();

            if (CanMove(gameObjectBehavior))
            {
                speed = gameObjectBehavior.GetSpeed();
                animator.SetFloat("Speed", speed);

                if (changeDirection)
                {
                    SetNewDirection();
                    nextPosition = gameObjectBehavior.transform.position + nextDirection;
                }
                
                if (CanMoveWithGivenDirection(gameObjectBehavior))
                {
                    RotateAndMove(gameObjectBehavior);
                }
                else
                {
                    changeDirection = true;
                }

                if (IsSamePositions(gameObjectBehavior.transform.position, nextPosition))
                {
                    changeDirection = true;
                    currentDirection = nextDirection;
                    continueDirection = true;
                }
            }
        }

        private bool IsSamePositions(Vector3 current, Vector3 next)
        {
            float error = 0.01f;

            if (current.x == next.x)
            {
                if (Math.Abs(current.z - next.z) < error)
                {
                    return true;
                }
            }
            else if (current.z == next.z)
            {
                if (Math.Abs(current.x - next.x) < error)
                {
                    return true;
                }
            }

            return false;
        }

        private void SetNewDirection()
        {
            nextDirection = Helper.GetRandomDirection();
            changeDirection = false;

            if (continueDirection)
            {
                while (nextDirection.x == -currentDirection.x || nextDirection.z == -currentDirection.z)
                {
                    nextDirection = Helper.GetRandomDirection();
                }
                continueDirection = false;
            }
        }

        private bool CanMoveWithGivenDirection(MonoBehaviour gameObjectBehavior)
        {
            return !PhysicsHelper.CharacterSphereCast(gameObjectBehavior, nextDirection);
        }

        private void RotateAndMove(MonoBehaviour gameObjectBehavior)
        {
            PhysicsHelper.Rotate(gameObjectBehavior, nextDirection, Enums.TypeOfVector3.Direction);
            gameObjectBehavior.transform.Translate(nextDirection * Time.deltaTime * speed, Space.World);
        }
    }
}