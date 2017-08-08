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

        public bool CanMove(CharacterBase gameObjectBehavior)
        {
            direction = Helper.GetDirectionByKeyboard();
            return direction != Vector3.zero;
        }

        public void Move(CharacterBase gameObjectBehavior)
        {
            //Helper.Rotate(gameObjectBehavior, direction);
            
            
            if (CanMove(gameObjectBehavior))
            {
                Vector3 currentPosition = gameObjectBehavior.transform.position;
                Vector3 nextPosition = gameObjectBehavior.transform.position + direction;

                Helper.Rotate(gameObjectBehavior, nextPosition, Helper.TypeOfVector3.NextPosition);

                if (!Helper.CharacterCapsuleCast(gameObjectBehavior))
                    gameObjectBehavior.StartCoroutine(MakeMove(gameObjectBehavior, currentPosition,
                            nextPosition, 0.5f));
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
                if (!Helper.CharacterCapsuleCast(gameObjectBehavior))
                    gameObjectBehavior.transform.position = Vector3.Lerp(start, finish, ratio);
                yield return new WaitForEndOfFrame();
            }
        }
    }
}
