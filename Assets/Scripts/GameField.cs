using Assets.Scripts.BaseClasses;
using Assets.Scripts.Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Assets.Scripts
{
    public class GameField: GameFieldBase
    {
        private StaticObjectsGeneratorBase staticObjects;
        private DynamicObjectsGeneratorBase dynamicObjects;
        private float wallOffset = 0.5f;
        private GameObject concreteWall, floor, brickWall;
        private GameObject player, enemy;
        private GameObject[] powerups;

        void Start()
        {
            Initialize();

            GenerateGameField();

            //AddPlayerToField();
            StartCoroutine(AddEnemyToField());
        }

        void Update()
        {
        }

        public override void GenerateGameField()
        {
            GenerateFloor();
            GenerateBorders();
            GenerateConcreteWalls();
            GenerateBrickWalls();
            GeneratePowerups();
        }

        private void GenerateBorders()
        {
            GenerateHorizontalBorders();
            GenerateVerticalBorders();            
        }

        private void GenerateConcreteWalls()
        {
            for (int i = 0; i < columnCount; i++)
            {
                for (int j = 0; j < rowCount; j++)
                {
                    if (i % 2 != 0 && j % 2 != 0)
                    {
                        Vector3 position = new Vector3(i, wallOffset, j);
                        var gameObject = Instantiate(concreteWall, position, Quaternion.identity);
                        NetworkServer.Spawn(gameObject);
                        field[i, j] = (int)Enums.FieldState.Filled;
                    }
                }
            }
        }

        private void GenerateFloor()
        {
            for (int i = -1; i < columnCount + 1; i++)
            {
                for (int j = -1; j < rowCount + 1; j++)
                {
                    var gameObject = Instantiate(floor, new Vector3(i, 0.0f, j), Quaternion.identity);
                    NetworkServer.Spawn(gameObject);
                }
            }
        }

        private void GenerateBrickWalls()
        {
            System.Random random = new System.Random();

            for (int i = 0; i < brickWallsCount; i++)
            {
                while (true)
                {
                    int row = random.Next(0, rowCount);
                    int column = random.Next(0, columnCount);

                    if (!IsConcreteWall(row, column) && field[column, row] == (int)Enums.FieldState.Empty)
                    {
                        var gameObject = Instantiate(brickWall, new Vector3(column, wallOffset, row),
                            Quaternion.identity);
                        NetworkServer.Spawn(gameObject);
                        field[column, row] = (int)Enums.FieldState.Filled;
                        break;
                    }
                }
            }
        }

        private void GenerateHorizontalBorders()
        {
            for (int i = -1; i < columnCount + 1; i++)
            {
                Vector3 position = new Vector3(i, wallOffset, -1);
                var gameObject = Instantiate(concreteWall, position, Quaternion.identity);
                NetworkServer.Spawn(gameObject);

                position = new Vector3(i, wallOffset, rowCount);
                gameObject = Instantiate(concreteWall, position, Quaternion.identity);
                NetworkServer.Spawn(gameObject);
            }
        }

        private void GenerateVerticalBorders()
        {
            for (int i = -1; i < rowCount + 1; i++)
            {
                Vector3 position = new Vector3(-1, wallOffset, i);
                var gameObject = Instantiate(concreteWall, position, Quaternion.identity);
                NetworkServer.Spawn(gameObject);

                position = new Vector3(columnCount, wallOffset, i);
                gameObject = Instantiate(concreteWall, position, Quaternion.identity);
                NetworkServer.Spawn(gameObject);
            }
        }

        private void GeneratePowerups()
        {
            System.Random random = new System.Random();

            foreach (var powerup in powerups)
            {
                while (true)
                {
                    int row = random.Next(0, rowCount);
                    int column = random.Next(0, columnCount);

                    if (!IsConcreteWall(row, column) && field[column, row] == 
                        (int)Enums.FieldState.Filled)
                    {
                        var gameObject = Instantiate(powerup, new Vector3(column, wallOffset, row), 
                            Quaternion.identity);
                        NetworkServer.Spawn(gameObject);
                        break;
                    }
                }
            }
        }

        private bool IsConcreteWall(int i, int j)
        {
            return (i % 2 != 0 && j % 2 != 0);
        }

        private void AddPlayerToField()
        {
            //Vector3 position = GameFieldManager.GeneratePlayerPosition();
            //Vector3 position = new Vector3(0.0f, 0.0f, 0.0f);

            //player = dynamicObjects.GetPlayer();

            //position.y += player.transform.lossyScale.y;

            //var gameObject = Instantiate(player, position, Quaternion.identity);
            //NetworkServer.Spawn(gameObject);
        }

        private IEnumerator AddEnemyToField()
        {
            yield return new WaitForSeconds(1);
            Vector3 position = GenerateEnemyPosition();
            enemy = dynamicObjects.GetEnemy();
            //Vector3 position = new Vector3(columnCount - 1, 0.0f, rowCount - 1);
            //position.y += enemy.transform.lossyScale.y;
            var gameObject = Instantiate(enemy, position, Quaternion.identity);
            NetworkServer.Spawn(gameObject);
            
        }

        private Vector3 GenerateEnemyPosition()
        {
            System.Random random = new System.Random();

            while (true)
            {
                int column = random.Next(0, columnCount);
                int row = random.Next(0, rowCount);

                if (field[column, row] == (int)Enums.FieldState.Empty)
                {
                    Vector3 position = new Vector3(column, 0.0f, row); 

                    //position.y += enemy.transform.lossyScale.y;

                    field[column, row] = (int)Enums.FieldState.Enemy;
                    return position;

                }
            }
        }

        private void Initialize()
        {
            staticObjects = ObjectsCreator.GetStaticObjects();
            dynamicObjects = ObjectsCreator.GetDynamicObjects();

            floor = staticObjects.GetFloor();
            concreteWall = staticObjects.GetConcreteWall();
            brickWall = staticObjects.GetBrickWall();
            powerups = staticObjects.GetPowerups();
        }
    }
}
