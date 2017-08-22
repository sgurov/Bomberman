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

        public override GameObject[] GetPowerups()
        {
            List<GameObject> result = new List<GameObject>();

            result.Add((GameObject)Resources.Load("Powerups/Bombs", typeof(GameObject)));
            result.Add((GameObject)Resources.Load("Powerups/Flames", typeof(GameObject)));
            result.Add((GameObject)Resources.Load("Powerups/Speed", typeof(GameObject)));
            result.Add((GameObject)Resources.Load("Powerups/WallPass", typeof(GameObject)));

            return result.ToArray();
        }
    }
}
