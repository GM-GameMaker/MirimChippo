using UnityEngine;
using UnityEngine.UI;

public class PlayerCollect : MonoBehaviour
{
    public Image[] clearIcons; // Inspector�� ������ �̹��� 3�� �ֱ�!
    private int collectedItemCount = 0;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Collectible"))
        {
            if (collectedItemCount < clearIcons.Length)
            {
                Color c = clearIcons[collectedItemCount].color;
                c.a = 1f; // ���İ� 1�� ���� (���� ������)
                clearIcons[collectedItemCount].color = c;

                collectedItemCount++;
            }

            Destroy(other.gameObject);
        }
    }
}
