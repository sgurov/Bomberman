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
        public abstract Canvas GetCanvas();
        public abstract GameObject GetBomb();
        public abstract GameObject GetExplosion();
        public abstract GameObject[] GetPowerups();
        public abstract Canvas GetSpeedImage();
        public abstract Canvas GetBombsImage();
        public abstract Canvas GetFlamesImage();
        public abstract Canvas GetWallpassImage();
    }
}
