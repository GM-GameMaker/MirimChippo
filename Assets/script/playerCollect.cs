using UnityEngine;
using UnityEngine.UI;

public class PlayerCollect : MonoBehaviour
{
    public Image[] clearIcons; // Inspector에 아이콘 이미지 3개 넣기!
    private int collectedItemCount = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            if (collectedItemCount < clearIcons.Length)
            {
                Color c = clearIcons[collectedItemCount].color;
                c.a = 1f; // 알파값 1로 변경 (완전 불투명)
                clearIcons[collectedItemCount].color = c;

                collectedItemCount++;
            }

            Destroy(other.gameObject);
        }
    }
}
