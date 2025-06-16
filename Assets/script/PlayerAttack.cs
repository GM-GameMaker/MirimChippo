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
        if (currentIndex >= itemPrefabs.Length) return;

        GameObject item = Instantiate(itemPrefabs[currentIndex], firePoint.position, Quaternion.identity);
        Rigidbody2D rb = item.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(direction * throwSpeed, 0);

        // 던진 아이템에게 자기 자신을 전달
        item.GetComponent<ThrownItem>()?.SetOwner(this);
    }

    public void UseItem()
    {
        currentIndex++;
    }
}
