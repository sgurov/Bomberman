using Assets.Scripts.BaseClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    class KeyboardAnimatedMove : ICharacterBehavior
    {
        private Vector3 direction;
        private float duration = 0.5f;

        public bool CanMove(CharacterBase gameObjectBehavior)
        {
            direction = PhysicsHelper.GetDirectionByKeyboard();
            return direction != Vector3.zero;
        }

        public void Move(CharacterBase gameObjectBehavior)
        {
            //Helper.Rotate(gameObjectBehavior, direction);
            
            
            if (CanMove(gameObjectBehavior))
            {
                Vector3 currentPosition = gameObjectBehavior.transform.position;
                Vector3 nextPosition = gameObjectBehavior.transform.position + direction;

                PhysicsHelper.Rotate(gameObjectBehavior, nextPosition, Enums.TypeOfVector3.NextPosition);

                if (!PhysicsHelper.CharacterSphereCast(gameObjectBehavior))
                    gameObjectBehavior.StartCoroutine(MakeMove(gameObjectBehavior, currentPosition,
                            nextPosition, duration));
            }
        }

        private IEnumerator MakeMove(MonoBehaviour gameObjectBehavior, Vector3 start, Vector3 finish, float duration)
        {
            
            float elapsedTime = 0;
            float ratio = 0;
            
            while (ratio < duration)
            {
                elapsedTime += Time.deltaTime;
                ratio = elapsedTime / duration;
                if (!PhysicsHelper.CharacterSphereCast(gameObjectBehavior))
                    gameObjectBehavior.transform.position = Vector3.Lerp(start, finish, ratio);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
