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
        public abstract GameObject GetBomb();
        public abstract GameObject GetExplosion();
        public abstract Canvas GetGameOverText();
        public abstract Canvas GetSpeedImage();
        public abstract Canvas GetBombsImage();
        public abstract Canvas GetFlamesImage();
        public abstract Canvas GetWallpassImage();
    }
}
