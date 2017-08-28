using Assets.Scripts.BaseClasses;
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
    public class PlayerBase : CharacterBase
    {
        private List<GameObject> powerups = new List<GameObject>();
        private int bombs = 3;
        private bool wallPass = false;
        private float explosionDistance = 1.0f;
        private float explosionDelay = 2.0f;
        public AudioClip dropBombSound;
        public AudioClip deathSound;
        public AudioClip powerupSound;
        private NetworkAnimator netAnimator;
        
        public void AddPowerup(GameObject powerup)
        {
            powerups.Add(powerup);
        }

        protected void SetPlayerPowerups()
        {
            foreach (var powerup in powerups)
            {
                String name = powerup.name;

                if (name.Contains("Bombs"))
                    bombs++;
                else if (name.Contains("Speed"))
                {
                    speed = (int)Enums.Speed.Fast;
                    networkAnimator.animator.SetFloat("Speed", speed);
                }
                else if (name.Contains("Flames"))
                    explosionDistance += 1.0f;
                else if (name.Contains("WallPass"))
                    wallPass = true;
            }
        }

        public override int GetSpeed()
        {
            return speed;
        }

        public override bool CanWallPass()
        {
            return wallPass;
        }

        public override int GetBombs()
        {
            return bombs;
        }

        public float GetExplosionDistance()
        {
            return explosionDistance;
        }

        public void ReduceBombsCount()
        {
            bombs--;
        }

        protected void PlayPowerupSound()
        {
            audioSource.PlayOneShot(powerupSound);
        }

        protected void PlayBombDropSound()
        {
            audioSource.pitch = 3;
            audioSource.PlayOneShot(dropBombSound);
            audioSource.pitch = 1;
        }

        protected virtual void PlayDeathSound()
        {
            audioSource.PlayOneShot(deathSound);
        }

        protected virtual void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Powerup")
            {
                PlayPowerupSound();
                AddPowerup(other.gameObject);
                
                //other.gameObject.SetActive(false);
                if (isClient)
                {
                    CmdHidePowerup(other.transform.name);
                }
                SetPlayerPowerups();
            }
        }

        protected void DropBomb()
        {
            DynamicObjectsGeneratorBase dynamicObjectsGenerator = ObjectsCreator.GetDynamicObjects();
            float bombRadius = 0.5f;
            GameObject bomb = dynamicObjectsGenerator.GetBomb();
            Vector3 position = transform.position;
            position.y = bombRadius;
            GameObject bombObject = Instantiate(bomb, position, Quaternion.identity) as GameObject;

            if (isServer)
            {
                NetworkServer.Spawn(bombObject);
            }

            StartCoroutine(Exploder.Explode(this, bombObject, explosionDelay, GetExplosionDistance(),
                DestroyExplodedObjects));
            
        }

        protected void DestroyExplodedObjects(RaycastHit[] objects)
        {
            foreach (var item in objects)
            {
                switch (item.transform.tag)
                {
                    case "Player":
                        //GameObject.Destroy(item.transform.gameObject);
                        if (!item.transform.GetComponent<PlayerBase>().dead)
                        {
                            netAnimator = item.transform.GetComponent<NetworkAnimator>();
                            netAnimator.animator.SetFloat("Speed", 0);
                            item.transform.GetComponent<PlayerBase>().dead = true;
                            netAnimator.SetTrigger("Death");
                            netAnimator.animator.ResetTrigger("Death");
                        }
                        //dead = true;
                        break;
                    case "Enemy":
                        GameObject.Destroy(item.transform.gameObject);
                        break;
                    case "Brick":
                        GameObject.Destroy(item.transform.gameObject);
                        break;
                }
            }
        }

        [Command]

        void CmdHidePowerup(string name)
        {
            GameObject powerup = GameObject.Find(name);

            if (powerup != null)
            {
                powerup.SetActive(false);
            }

            RpcHidePowerup(name);
        }

        [ClientRpc]

        void RpcHidePowerup(string name)
        {
            GameObject powerup = GameObject.Find(name);

            if (powerup != null)
            {
                powerup.SetActive(false);
            }
        }
    }
}
