using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Scripts.BaseClasses;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts
{
    class StaticObjectsGenerator: StaticObjectsGeneratorBase
    {
        public override GameObject GetConcreteWall()
        {
            return (GameObject)Resources.Load("Walls/ConcreteWall", typeof(GameObject));
        }

        public override GameObject GetBrickWall()
        {
            return (GameObject)Resources.Load("Walls/BrickWall", typeof(GameObject));
        }

        public override GameObject GetFloor()
        {
            return (GameObject)Resources.Load("Walls/Floor", typeof(GameObject));
        }

        public override Canvas GetCanvas()
        {
            return (Canvas)Resources.Load("UI/Canvas", typeof(Canvas));
        }

        public override GameObject GetBomb()
        {
            return (GameObject)Resources.Load("Bombs/Bomb", typeof(GameObject));
        }

        public override GameObject GetExplosion()
        {
            return (GameObject)Resources.Load("Effects/Explosion", typeof(GameObject));
        }

        public override GameObject[] GetPowerups()
        {
            List<GameObject> result = new List<GameObject>();

            result.Add((GameObject)Resources.Load("Powerups/Bombs", typeof(GameObject)));
            result.Add((GameObject)Resources.Load("Powerups/Flames", typeof(GameObject)));
            result.Add((GameObject)Resources.Load("Powerups/Speed", typeof(GameObject)));
            result.Add((GameObject)Resources.Load("Powerups/WallPass", typeof(GameObject)));

            return result.ToArray();
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
