using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCollect3 : MonoBehaviour
{
    public Image[] clearIcons;        
    private int collectedItemCount = 0;
    private int requiredItemCount = 6;

    void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("충돌한 오브젝트 태그: " + collision.gameObject.tag);

        // 충돌한 상대가 "Item" 태그인지 확인
        if (collision.gameObject.CompareTag("Item"))
        {
            // UI 아이콘 중 아직 불투명하지 않은 아이콘이 있다면 불투명 처리
            if (collectedItemCount < clearIcons.Length)
            {
                Color c = clearIcons[collectedItemCount].color;
                c.a = 1f;  // 완전 불투명
                clearIcons[collectedItemCount].color = c;

                collectedItemCount++;
            }
                    
            // 충돌한 아이템 오브젝트 삭제
            Destroy(collision.gameObject);
        }
    }
}
