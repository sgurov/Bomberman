using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace Assets.Scripts.BaseClasses
{
    public abstract class GameFieldBase : MonoBehaviour
    {
        protected int columnCount = 9;
        protected int rowCount = 11;
        protected int brickWallsCount = 20;
        public int[,] field;

        public abstract void GenerateGameField();
    }
}