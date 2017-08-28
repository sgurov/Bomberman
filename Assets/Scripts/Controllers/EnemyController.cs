using Assets.Scripts;
using Assets.Scripts.Algorithms;
using Assets.Scripts.BaseClasses;
using Assets.Scripts.Behaviors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : EnemyBase
{
    private ICharacterBehavior characterBehavior;
    
    protected override void Start ()
    {
        base.Start();
        characterBehavior = new RandomMove();
	}

    void FixedUpdate ()
    {
        if (!isCollision)
        {
            characterBehavior.Move(this);
        }
    }
}
