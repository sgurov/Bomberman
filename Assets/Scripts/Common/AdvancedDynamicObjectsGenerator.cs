using Assets.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BaseClasses
{
    class AdvancedDynamicObjectsGenerator : DynamicObjectsGenerator
    {
        public override GameObject GetPlayer()
        {
            return (GameObject)Resources.Load("Characters/Player1", typeof(GameObject));
        }

        public override GameObject GetEnemy()
        {
            return (GameObject)Resources.Load("Characters/Enemy1", typeof(GameObject));
        }
    }
}
