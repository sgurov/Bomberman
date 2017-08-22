using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkPlayerController : PlayerBase
{
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    protected override void Start ()
    {
        base.Start();
	}
	
	void Update ()
    {
        if (!isLocalPlayer)
        {
            return;
        }
        Move();
	}

    void Move()
    {
        Vector3 direction = PhysicsHelper.GetDirectionByKeyboard();

        if (direction != Vector3.zero)
        {
            animator.SetFloat("Speed", GetSpeed());
            transform.rotation = Quaternion.LookRotation(direction);
            transform.Translate(direction * GetSpeed() * Time.deltaTime, Space.World);
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
    }

    protected override void PlayStepSound()
    {
        base.PlayStepSound();
    }

    protected override void PlayDeathSound()
    {
        base.PlayDeathSound();
    }
}