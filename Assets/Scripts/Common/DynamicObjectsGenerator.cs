using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.BaseClasses;
using UnityEngine;

namespace Assets.Scripts.Common
{
    class DynamicObjectsGenerator : DynamicObjectsGeneratorBase
    {
        public override GameObject GetPlayer()
        {
            return (GameObject)Resources.Load("Characters/Player", typeof(GameObject));
        }

        public override GameObject GetEnemy()
        {
            return (GameObject)Resources.Load("Characters/Enemy", typeof(GameObject));
        }

        public override GameObject GetBomb()
        {
            return (GameObject)Resources.Load("Bombs/Bomb", typeof(GameObject));
        }

        public override GameObject GetExplosion()
        {
            return (GameObject)Resources.Load("Effects/Explosion", typeof(GameObject));
        }

        public override Canvas GetGameOverText()
        {
            return (Canvas)Resources.Load("UI/GameOverText", typeof(Canvas));
        }

        public override Canvas GetSpeedImage()
        {
            return (Canvas)Resources.Load("UI/SpeedPowerup", typeof(Canvas));
        }

        public override Canvas GetBombsImage()
        {
            return (Canvas)Resources.Load("UI/BombsPowerup", typeof(Canvas));
        }

        public override Canvas GetFlamesImage()
        {
            return (Canvas)Resources.Load("UI/FlamesPowerup", typeof(Canvas));
        }

        public override Canvas GetWallpassImage()
        {
            return (Canvas)Resources.Load("UI/WallpassPowerup", typeof(Canvas));
        }
    }
}
