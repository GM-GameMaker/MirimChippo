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

            playerAttack?.UseItem(); // 던진 주체의 UseItem만 호출

            Destroy(gameObject);
        }
        else
        {
            Invoke(nameof(DestroyItem), 2f);
        }
    }

    void DestroyItem()
    {
        Destroy(gameObject);
    }
}
