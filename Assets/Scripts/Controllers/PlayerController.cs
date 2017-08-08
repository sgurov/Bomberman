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
    public class PlayerController : Player
    {
        private ICharacterBehavior characterBehavior;
        private float explosionDelay = 2.0f;

        void Start()
        {
            characterBehavior = new SimpleKeyboardMove();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (GetBombs() > 0)
                {
                    DropBomb();
                    ReduceBombsCount();
                }
            }
        }

        void FixedUpdate()
        {
            characterBehavior.Move(this);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Powerup")
            {
                AddPowerup(other.gameObject);
                other.gameObject.SetActive(false);
                SetPlayerPowerups();
            }
        }

        private void OnDestroy()
        {
            //Time.timeScale = 0;
        }

        private void DropBomb()
        {
            StaticObjectsGeneratorBase staticObjectsGenerator = ObjectsCreator.GetStaticObjects();

            GameObject bomb = staticObjectsGenerator.GetBomb();
            Vector3 position = transform.position;
            position.y = 0.5f;
            GameObject bombObject = Instantiate(bomb, position, Quaternion.identity) as GameObject;
            StartCoroutine(Exploder.Explode(bombObject, explosionDelay, GetExplosionDistance(), DestroyExplodedObjects));
        }

        private void DestroyExplodedObjects(RaycastHit[] objects)
        {
            foreach (var item in objects)
            {
                switch (item.transform.tag)
                {
                    case "Player":
                        GameObject.Destroy(item.transform.gameObject);
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
