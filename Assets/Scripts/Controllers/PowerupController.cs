﻿using Assets.Scripts;
using Assets.Scripts.BaseClasses;
using Assets.Scripts.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerupController : MonoBehaviour
{
    private StaticObjectsGeneratorBase staticObjects;
    private Canvas bombsPowerupInfo, flamesPowerupInfo, speedPowerupInfo, wallpassPowerupInfo;
    private float delaySeconds = 2.0f;
    // Use this for initialization
    void Start ()
    {
        Initialize();
	}
	
	// Update is called once per frame
	void Update ()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
	}

    private void OnTriggerEnter(Collider other)
    {
        string powerup = gameObject.name;

        if (name.Contains("Speed"))
        {
            ShowPowerupInfo(speedPowerupInfo, delaySeconds);
        }
        else if (name.Contains("Bombs"))
        {
            ShowPowerupInfo(bombsPowerupInfo, delaySeconds);
        }
        else if (name.Contains("Flames"))
        {
            ShowPowerupInfo(flamesPowerupInfo, delaySeconds);
        }
        else if (name.Contains("WallPass"))
        {
            ShowPowerupInfo(wallpassPowerupInfo, delaySeconds);
        }
    }

    private void ShowPowerupInfo(Canvas powerup, float seconds)
    {
        Canvas canvas = Instantiate(powerup);

        foreach (Transform child in canvas.transform)
        {
            if (!child.name.Contains("Small"))
                Destroy(child.gameObject, seconds);
        }
    }

    private void Initialize()
    {
        staticObjects = ObjectsCreator.GetStaticObjects();

        bombsPowerupInfo = staticObjects.GetBombsImage();
        flamesPowerupInfo = staticObjects.GetFlamesImage();
        speedPowerupInfo = staticObjects.GetSpeedImage();
        wallpassPowerupInfo = staticObjects.GetWallpassImage();
    }
}