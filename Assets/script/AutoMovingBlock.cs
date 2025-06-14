using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AutoMovingBlock : MonoBehaviour
{
    public float moveDistance = 2f;     // ���Ʒ� �̵� �Ÿ�
    public float moveSpeed = 2f;        // �̵� �ӵ�

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
        // ���� �������� �̵�
        Vector3 destination = movingUp ? targetPos : startPos;
        transform.position = Vector3.MoveTowards(transform.position, destination, moveSpeed * Time.deltaTime);

        // �����ϸ� ���� ����
        if (Vector3.Distance(transform.position, destination) < 0.01f)
        {
            movingUp = !movingUp;
        }
    }
}
