using Assets.Scripts;
using Assets.Scripts.Algorithms;
using Assets.Scripts.BaseClasses;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class IntelligentEnemyController : EnemyBase
{
    [SyncVar]
    private Vector3 playerPosition, enemyPosition;
    [SyncVar]
    private Vector3 nextPosition = new Vector3(-1, 0, -1);
    //private bool newPosition;
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
            startNode = new Node(enemyPosition);
            targetNode = new Node(playerPosition);
            path = new List<Node>();

            PathFinding.FindPath(path, startNode, targetNode);

            SetNextPosition();

            if (nextPosition.x < 0 && nextPosition.z < 0)
            {
                nextPosition = transform.position;
            }
        }
        else
        {
            Debug.LogError("Player wasn't found!");
        }
	}

	void FixedUpdate()
    {
        if (!isServer)
        {
            return;
        }

        if (isCollision)
        {
            //StartCoroutine(SearchOtherPlayers());
            SearchOtherPlayers();
            return;
        }

        if (path == null || path.Count == 0)
        {
            PathFinding.FindPath(path, startNode, targetNode);
        }
        else
        {
            //SetNextPosition();
        }

        if (path != null && path.Count > 0 && (nextPosition.x >= 0 && nextPosition.z >= 0))
        {
            networkAnimator.animator.SetFloat("Speed", GetSpeed());

            RpcRotateAndMove();
        }
        else
        {
            networkAnimator.animator.SetFloat("Speed", 0);
        }

        if (transform.position == nextPosition)
        {
            startNode = new Node(nextPosition);
            CheckPlayerPosition();
            SetNextPosition();
        }
    }

    [ClientRpc]
    private void RpcRotateAndMove()
    {
        transform.LookAt(nextPosition);
        StartCoroutine(Move(transform.position, nextPosition));
    }

    private IEnumerator Move(Vector3 start, Vector3 finish)
    {
        while (transform.position != finish)
        {
            transform.position = Vector3.MoveTowards(start, finish, Time.deltaTime * speed);
            //transform.position = Vector3.Lerp(start, finish, Time.deltaTime * speed);
            yield return new WaitForEndOfFrame();
        }
        
    }

    private Vector3 GetDirection(Vector3 start, Vector3 target)
    {
            if (start.z > target.z)
            {
                return Vector3.back;
            }
            else if (start.z < target.z)
            {
                return Vector3.forward;
            }
        
            if (start.x > target.x)
            {
                return Vector3.left;
            }
            else if (start.x < target.x)
            {
                return Vector3.right;
            }

        return Vector3.zero;
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
        //yield return new WaitForSeconds((float)GetDistance(transform.position, playerPosition) / GetSpeed());
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
            //Debug.LogError("Player wasn't found!");
            networkAnimator.animator.SetFloat("Speed", 0);
        }
    }

    private void SetNextPosition()
    {
        if (path.Count > 0)
        {
            nextPosition = path[0].position;
            path.RemoveAt(0);
            //newPosition = false;
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

    private GameObject GetPlayer()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        double minDistance = double.MaxValue;
        GameObject result = null;

        foreach (var item in players)
        {
            if (item.transform.name.Contains("Player"))
            {
                if (item.transform.GetComponent<PlayerBase>().dead)
                {
                    continue;
                }
                if (GetDistance(transform.position, item.transform.position) < minDistance)
                {
                    minDistance = GetDistance(transform.position, item.transform.position);
                    result = item;
                }
            }
        }

        return result;
    }

    private double GetDistance(Vector3 start, Vector3 target)
    {
        return Math.Abs(start.x - target.x) + Math.Abs(start.z - target.z);
    }

    private void SearchOtherPlayers()
    {
        //yield return new WaitForFixedUpdate();

        GameObject player = GetPlayer();

        if (player != null)
        {
            startNode = new Node(GetRoundPosition(transform.position));
            targetNode = new Node(GetRoundPosition(player.transform.position));
            path = new List<Node>();
            isCollision = false;
        }
        else
        {
            networkAnimator.SetTrigger("Taunt");
        }
    }
}
