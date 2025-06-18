using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechBubble : MonoBehaviour
{
    public float speed = 5f;

    void Update()
    {
        // 왼쪽으로 이동
        transform.Translate(Vector2.left * speed * Time.deltaTime);

        // 카메라 왼쪽 밖으로 나가면 삭제
        if (transform.position.x < Camera.main.transform.position.x - 10f)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // 플레이어와 충돌 시 말풍선만 제거
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        // 아이템과 충돌 시 아이템만 제거
        else if (collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
        }
    }
}
