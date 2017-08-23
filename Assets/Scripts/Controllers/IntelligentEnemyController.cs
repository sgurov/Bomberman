using Assets.Scripts;
using Assets.Scripts.Algorithms;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntelligentEnemyController : EnemyBase
{
    private Vector3 playerPosition, enemyPosition;
    private Vector3 nextPosition = new Vector3(-1, 0, -1);
    private bool newPosition;
    private List<Node> path;
    private Node startNode, targetNode;

	protected override void Start()
    {
        base.Start();
        GameObject player = GetPlayer();
        if (player != null)
        {
            playerPosition = GetRoundPosition(player.transform.position);
            enemyPosition = GetRoundPosition(transform.position);
            startNode = new Node(GetRoundPosition(transform.position));
            targetNode = new Node(playerPosition);
            path = new List<Node>();

            PathFinding.FindPath(path, startNode, targetNode);

            SetNextPosition();
            
        }
        else
        {
            Debug.LogError("Player wasn't found!");
        }
	}

	void FixedUpdate()
    {
        if (isCollision)
        {
            return;
        }
        
		if (newPosition)
        {
            if (path == null || path.Count == 0)
            {
                PathFinding.FindPath(path, startNode, targetNode);
            }
            else
            {
                SetNextPosition();
            }
        }

        if (path != null && path.Count > 0 && (nextPosition.x >= 0 && nextPosition.z >= 0))
        {
            animator.SetFloat("Speed", GetSpeed());
            RotateAndMove();
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }

        if (IsSamePositions(transform.position, nextPosition))
        {
            newPosition = true;
            startNode = new Node(GetRoundPosition(transform.position));
            //CheckPlayerPosition();
        }

        Vector3 currentEnemyPosition = GetRoundPosition(transform.position);

        if (currentEnemyPosition == enemyPosition)
        {
            newPosition = true;
        }

        CheckPlayerPosition();
    }

    private void RotateAndMove()
    {
        transform.LookAt(nextPosition);
        Vector3 direction = nextPosition - transform.position;
        direction = direction.normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);
    }

    private bool IsSamePositions(Vector3 current, Vector3 next)
    {
        float error = 0.1f;

        if (Math.Round(current.x, 2) == Math.Round(next.x, 2))
        {
            if (Math.Abs(current.z - next.z) < error)
            {
                return true;
            }
        }
        else if (Math.Round(current.z, 2) == Math.Round(next.z, 2))
        {
            if (Math.Abs(current.x - next.x) < error)
            {
                return true;
            }
        }

        return false;
    }

    private Vector3 GetRoundPosition(Vector3 position)
    {
        Vector3 result = new Vector3();
        result.x = (int)Math.Round(position.x);
        result.y = position.y;
        result.z = (int)Math.Round(position.z);
        return result;
    }

    private void CheckPlayerPosition()
    {
        GameObject player = GetPlayer();
        Vector3 currentPlayerPosition;

        if (player != null)
        {
            currentPlayerPosition = GetRoundPosition(player.transform.position);

            if (currentPlayerPosition != playerPosition)
            {
                playerPosition = player.transform.position;
                targetNode = new Node(GetRoundPosition(playerPosition));
                path = new List<Node>();
                PathFinding.FindPath(path, startNode, targetNode);
            }
        }
        else
        {
            Debug.LogError("Player wasn't found!");
        }
    }

    private GameObject GetPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var item in players)
        {
            if (item.name.Contains("Player"))
            {
                return item;
            }
        }

        return null;
    }

    private void SetNextPosition()
    {
        if (path.Count > 0)
        {
            nextPosition = path[0].position;
            path.RemoveAt(0);
            newPosition = false;
        }
    }

    protected override void PlayStepSound()
    {
        base.PlayStepSound();
    }

    protected override void PlayAttackSound()
    {
        base.PlayAttackSound();
    }

    protected override void PlayTauntSound()
    {
        base.PlayTauntSound();
    }
}
