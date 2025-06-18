using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public Transform firePoint;
    public float throwSpeed = 10f;

    private int facingDirection = 1;
    private int currentIndex = 0;

    void Update()
    {
        if (Input.GetKey(KeyCode.D))
            facingDirection = 1;
        else if (Input.GetKey(KeyCode.A))
            facingDirection = -1;

        firePoint.position = transform.position + new Vector3(0.5f * facingDirection, 0.2f, 0);

        if (Input.GetKeyDown(KeyCode.X))
        {
            ThrowItem(facingDirection);
        }
    }

    void ThrowItem(int direction)
    {
        if (currentIndex >= itemPrefabs.Length)
        {
            Debug.LogWarning("모든 아이템 사용");
            return;
        }

        if (itemPrefabs[currentIndex] == null)
        {
            Debug.LogWarning($"itemPrefabs[{currentIndex}]이 null입니다.");
            return;
        }

        GameObject itemClone = Instantiate(itemPrefabs[currentIndex], firePoint.position, Quaternion.identity);
        Rigidbody2D rb = itemClone.GetComponent<Rigidbody2D>();

        if (rb == null)
        {
            Debug.LogWarning("프리팹에 Rigidbody2D가 없습니다.");
            Destroy(itemClone);
            return;
        }

        rb.velocity = new Vector2(direction * throwSpeed, 0);

        ThrownItem thrown = itemClone.GetComponent<ThrownItem>();
        if (thrown != null)
        {
            thrown.SetOwner(this);
        }
        else
        {
            Debug.LogWarning("프리팹에 ThrownItem 스크립트가 없습니다.");
        }
    }

    public void UseItem()
    {
        currentIndex++;
    }
}
