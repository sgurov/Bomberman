using Assets.Scripts;
using Assets.Scripts.BaseClasses;
using Assets.Scripts.Behaviors;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyController : Enemy
{
    private ICharacterBehavior characterBehavior;
    
    void Start ()
    {
        characterBehavior = new RandomMove();
	}

    void FixedUpdate ()
    {
        characterBehavior.Move(this);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(other.gameObject);
        }
    }
}
