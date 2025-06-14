using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowItem : MonoBehaviour
{
    public float slowMultiplier = 0.5f;
    public float slowDuration = 3f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMove player = other.GetComponent<PlayerMove>();
            if (player != null)
            {
                player.ApplySlow(slowMultiplier, slowDuration);
                Destroy(gameObject);
            }
        }
    }
}
