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
        Debug.Log($"ThrownItem 충돌 감지: {collision.gameObject.name}, 태그: {collision.gameObject.tag}");

        if (collision.gameObject.CompareTag("Boss"))
        {
            Debug.Log("보스와 충돌함!");
            collision.gameObject.GetComponent<BossController>()?.TakeHit();
            playerAttack?.UseItem();
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
