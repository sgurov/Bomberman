using Assets.Scripts.BaseClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts
{
    public class Player : CharacterBase
    {
        private List<GameObject> powerups = new List<GameObject>();
        private int bombs = 3;
        private bool wallPass = false;
        private float explosionDistance = 1.0f;

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
                    speed = (int)Speed.Fast;
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

        public int GetBombs()
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
    }
}
