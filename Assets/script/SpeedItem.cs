using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class SpeedItem : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMove player = other.GetComponent<PlayerMove>();
            if (player != null)
            {
                player.BoostSpeed();
            }

            Destroy(gameObject); // æ∆¿Ã≈€ ∏‘∞Ì ªÁ∂Û¡¸
        }
    }
}
