using Assets.Scripts.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.BaseClasses
{
    public abstract class GameFieldBase : NetworkBehaviour
    {
        protected int columnCount = 9;
        protected int rowCount = 11;
        protected int brickWallsCount = 20;
        public int[,] field;

        public GameFieldBase()
        {
            GameFieldManager.fieldMatrix = new int[columnCount, rowCount];
        }

        public abstract void GenerateGameField();
    }
}