using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BaseClasses
{
    public abstract class DynamicObjectsGeneratorBase
    {
        public abstract GameObject GetPlayer();
        public abstract GameObject GetEnemy();
    }
}
