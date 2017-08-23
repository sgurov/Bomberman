using Assets.Scripts.BaseClasses;
using Assets.Scripts.Behaviors;
using Assets.Scripts.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class PlayerController : PlayerBase
    {
        private ICharacterBehavior characterBehavior;

        protected override void Start()
        {
            base.Start();
            characterBehavior = new SimpleKeyboardMove();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (GetBombs() > 0)
                {
                    DropBomb();
                    animator.SetTrigger("DropBomb");
                    ReduceBombsCount();
                }
            }
        }

        void FixedUpdate()
        {
            if (!dead)
            {
                Vector3 position = transform.position;
                position.y = 0;
                transform.position = position;
                characterBehavior.Move(this);
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            base.OnTriggerEnter(other);

            if (other.gameObject.tag == "Enemy")
            {
                animator.SetFloat("Speed", 0);
                
                if (!dead)
                {
                    StartCoroutine(DoDie());
                    dead = true;
                }
            }
        }

        private void OnDestroy()
        {
            //Time.timeScale = 0;
        }

        private IEnumerator DoDie()
        {
            yield return new WaitForSeconds(1);
            animator.SetTrigger("Death");
        }

        protected override void PlayStepSound()
        {
            base.PlayStepSound();
        }

        protected override void PlayDeathSound()
        {
            base.PlayDeathSound();
        }
    }
}
