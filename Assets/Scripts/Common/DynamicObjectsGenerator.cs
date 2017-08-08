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
    }
}
