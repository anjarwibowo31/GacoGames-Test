using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float chaseRadius;
    [SerializeField] private LayerMask playerLayerMask;
    [SerializeField] private float moveSpeed;

    private Vector3 playerPos;

    private void Start()
    {
        playerPos = PlayerData.Instance.transform.position;
    }

    private void Update()
    {
        float playerDistance = Vector3.Distance(transform.position, playerPos);

        Debug.Log(playerDistance);
        if (playerDistance < chaseRadius)
        {
            ChasePlayer();
            print("AAA");
        }
    }

    private void ChasePlayer()
    {
        {
            Vector3 direction = (playerPos - transform.position).normalized;
            transform.forward = direction;
            transform.Translate(direction * moveSpeed * Time.deltaTime);
        }
    }
}
