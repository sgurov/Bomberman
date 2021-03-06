﻿using Assets.Scripts.BaseClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyBase : CharacterBase
    {
        public AudioClip attackSound;
        public AudioClip tauntSound;
        protected bool isCollision = false;

        public override int GetSpeed()
        {
            return speed;
        }

        public override bool CanWallPass()
        {
            return false;
        }

        public override int GetBombs()
        {
            return 0;
        }

        protected void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                animator.SetFloat("Speed", 0);
                isCollision = true;
                animator.SetTrigger("Attack");
                //Destroy(other.transform.parent.gameObject, 2);
            }
        }

        protected virtual void PlayAttackSound()
        {
            audioSource.PlayOneShot(attackSound);
        }

        protected virtual void PlayTauntSound()
        {
            audioSource.PlayOneShot(tauntSound);
        }
    }
}
