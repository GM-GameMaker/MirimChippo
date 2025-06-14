using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : MonoBehaviour
{

    public float fallDistance = 2f;        // 얼마나 떨어질지
    public float moveSpeed = 10f;          // 얼마나 빠르게 움직일지
    public float waitTime = 1f;            // 떨어진 후 기다릴 시간

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

        // 빠르게 떨어짐
        yield return StartCoroutine(MoveToPosition(targetPosition));

        // 1초 대기
        yield return new WaitForSeconds(waitTime);

        // 빠르게 다시 올라옴
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
