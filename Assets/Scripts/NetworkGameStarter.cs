using Assets.Scripts;
using Assets.Scripts.BaseClasses;
using Assets.Scripts.Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkGameStarter : NetworkBehaviour
{
    public override void OnStartServer()
    {
        gameObject.AddComponent<GameField>();
    }

    //private void generateplayerpositions()
    //{
    //    transform transform = gameobject.transform;
    //    transform.position = generateplayerposition();
    //    networkmanager.registerstartposition(transform);
    //    transform.position = generateplayerposition();
    //    networkmanager.registerstartposition(transform);
    //}
}
