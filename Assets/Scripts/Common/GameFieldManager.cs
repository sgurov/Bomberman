using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts.Common
{
    public static class GameFieldManager
    {
        //public static int[,] fieldMatrix;

        /*public static Vector3 GeneratePlayerPosition(int[,] fieldMatrix)
        {
            System.Random random = new System.Random();

            while (true)
            {
                int column = random.Next(0, fieldMatrix.GetLength(0));
                int row = random.Next(0, fieldMatrix.GetLength(1));

                if (fieldMatrix[column, row] == (int)Enums.FieldState.Empty)
                {
                    fieldMatrix[column, row] = (int)Enums.FieldState.Player;
                    return new Vector3(column, 0.0f, row);
                }
            }
        }*/

        /*public static Vector3 GeneratePlayerPosition(SyncListStruct<Vector3> field, int maxRow, int maxColumn)
        {
            System.Random random = new System.Random();

            while (true)
            {
                int column = random.Next(0, maxColumn);
                int row = random.Next(0, maxRow);

                if (!field.Contains(new Vector3(column, 0, row)))
                {
                    field.Add(new Vector3(column, 0, row));
                    return new Vector3(column, 0.0f, row);
                }
            }
        }*/
    }
}
