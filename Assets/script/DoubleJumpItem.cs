using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJumpItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMove player = other.GetComponent<PlayerMove>();
            if (player != null)
            {
                player.EnableDoubleJumpForLimitedTime();
            }

            Destroy(gameObject); // æ∆¿Ã≈€ ∏‘∞Ì ªÁ∂Û¡¸
        }
    }
}
