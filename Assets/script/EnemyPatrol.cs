using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float moveSpeed = 2f;

    private float leftLimit;
    private float rightLimit;
    private bool movingRight = true;

    void Start()
    {
        // 부모 오브젝트(발판)의 크기를 기준으로 이동 범위 설정
        Transform parent = transform.parent;
        float width = parent.GetComponent<SpriteRenderer>().bounds.size.x;

        Vector3 center = parent.position;
        leftLimit = center.x - width / 2f;
        rightLimit = center.x + width / 2f;
    }

    void Update()
    {
        if (movingRight)
        {
            transform.Translate(Vector2.right * moveSpeed * Time.deltaTime);

            if (transform.position.x >= rightLimit)
                movingRight = false;
        }
        else
        {
            transform.Translate(Vector2.left * moveSpeed * Time.deltaTime);

            if (transform.position.x <= leftLimit)
                movingRight = true;
        }
    }
}
