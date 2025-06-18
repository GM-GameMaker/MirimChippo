using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrownItem : MonoBehaviour
{
    private bool collided = false;
    private PlayerAttack playerAttack;

    public void SetOwner(PlayerAttack attacker)
    {
        playerAttack = attacker;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collided) return;
        collided = true;

        if (collision.gameObject.CompareTag("Boss"))
        {
            collision.gameObject.GetComponent<BossController>()?.TakeHit();

            playerAttack?.UseItem(); // Boss에게 맞았을 때만 다음 아이템 사용
            Destroy(gameObject);
        }
        else
        {
            Invoke(nameof(DestroyItem), 2f);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Laser"))
        {
            Destroy(gameObject);
        }
    }

    void DestroyItem()
    {
        Destroy(gameObject);
    }
}
