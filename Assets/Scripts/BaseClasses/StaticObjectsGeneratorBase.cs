using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BaseClasses
{
    public abstract class StaticObjectsGeneratorBase
    {
        public abstract GameObject GetConcreteWall();
        public abstract GameObject GetBrickWall();
        public abstract GameObject GetFloor();
        public abstract GameObject[] GetPowerups();
    }
}
