using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoMovingBlock : MonoBehaviour
{
    public float moveDistance = 2f;     // 위아래 이동 거리
    public float moveSpeed = 2f;        // 이동 속도

    private Vector3 startPos;
    private Vector3 targetPos;
    private bool movingUp = true;


    void Start()
    {
        startPos = transform.position;
        targetPos = startPos + Vector3.up * moveDistance;
    }

    void Update()
    {
        // 현재 방향으로 이동
        Vector3 destination = movingUp ? targetPos : startPos;
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        // 도착하면 방향 반전
        if (Vector3.Distance(transform.position, destination) < 0.01f)
        {
            movingUp = !movingUp;
        }
    }
}
