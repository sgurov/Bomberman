using Assets.Scripts.BaseClasses;
using Assets.Scripts.Behaviors;
using Assets.Scripts.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

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
            if (!isLocalPlayer)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (GetBombs() > 0 && !isDead)
                {
                    //DropBomb();
                    networkAnimator.animator.SetTrigger("DropBomb");
                    
                    if (isClient)
                    {
                        CmdDropBomb();
                    }

                    ReduceBombsCount();
                }
            }
        }

        void FixedUpdate()
        {
            if (!isLocalPlayer)
            {
                return;
            }

            if (!isDead)
            {
                Vector3 position = transform.position;
                position.y = 0;
                transform.position = position;
                characterBehavior.Move(this);
            }
        }

        protected override void OnTriggerEnter(Collider other)
        {
            if (!isLocalPlayer)
            {
                return;
            }

            base.OnTriggerEnter(other);

            if (other.gameObject.tag == "Enemy")
            {
                networkAnimator.animator.SetFloat("Speed", 0);
                isDead = true;
                gameObject.GetComponentInChildren<MeshCollider>().isTrigger = false;
                StartCoroutine(DoDie());
            }
        }

        private void OnDestroy()
        {
            //Time.timeScale = 0;
        }

        private IEnumerator DoDie()
        {
            yield return new WaitForSeconds(1);
            networkAnimator.SetTrigger("Death");
            networkAnimator.animator.ResetTrigger("Death");
            //this.gameObject.SetActive(false);
            //Destroy(this, 3);
        }

        protected override void PlayStepSound()
        {
            base.PlayStepSound();
        }

        protected override void PlayDeathSound()
        {
            base.PlayDeathSound();
        }

        [Command]

        void CmdDropBomb()
        {
            DropBomb();
            RpcDropBomb();
        }

        [ClientRpc]

        void RpcDropBomb()
        {
            DropBomb();
        }
    }
}
