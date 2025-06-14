using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{

    public float fallDistance = 2f;        // �󸶳� ��������
    public float moveSpeed = 10f;          // �󸶳� ������ ��������
    public float waitTime = 1f;            // ������ �� ��ٸ� �ð�

    private Vector3 originalPosition;
    private Vector3 targetPosition;
    private bool isMoving = false;

    private void Start()
    {
        originalPosition = transform.position;
        targetPosition = originalPosition + Vector3.down * fallDistance;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isMoving && collision.collider.CompareTag("Player"))
        {
            StartCoroutine(FallAndReturn());
        }
    }

    private IEnumerator FallAndReturn()
    {
        isMoving = true;

        // ������ ������
        yield return StartCoroutine(MoveToPosition(targetPosition));

        // 1�� ���
        yield return new WaitForSeconds(waitTime);

        // ������ �ٽ� �ö��
        yield return StartCoroutine(MoveToPosition(originalPosition));

        isMoving = false;
    }

    private IEnumerator MoveToPosition(Vector3 target)
    {
        while (Vector3.Distance(transform.position, target) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, moveSpeed * Time.deltaTime);
            yield return null;
        }

        transform.position = target;
    }
}
