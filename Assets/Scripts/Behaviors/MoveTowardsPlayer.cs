using Assets.Scripts.Algorithms;
using Assets.Scripts.BaseClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.Behaviors
{
    public class MoveTowardsPlayer : ICharacterBehavior
    {
        public bool CanMove(CharacterBase gameObjectBehavior)
        {
            return true;
        }

        public void Move(CharacterBase gameObjectBehavior)
        {
            Animator animator = gameObjectBehavior.gameObject.GetComponent<Animator>();

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector3 playerPos = player.transform.position;

            int speed = gameObjectBehavior.GetSpeed();
            gameObjectBehavior.transform.LookAt(player.transform.position);

            animator.SetFloat("Speed", speed);
            gameObjectBehavior.transform.position += gameObjectBehavior.transform.forward * speed *
                Time.deltaTime;
        }
    }
}
