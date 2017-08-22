using Assets.Scripts.BaseClasses;
using Assets.Scripts.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

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
                    animator.SetFloat("Speed", speed);
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
                other.gameObject.SetActive(false);
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
            StartCoroutine(Exploder.Explode(bombObject, explosionDelay, GetExplosionDistance(),
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
                        animator.SetTrigger("Death");
                        animator.SetFloat("Speed", 0);
                        dead = true;
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
    }
}
