using UnityEngine;

public class BossController : MonoBehaviour
{
    public int maxHits = 6;
    private int currentHits = 0;

    public GameObject gameClearUI;

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Item"))
        {
            Destroy(collision.gameObject); // 맞았을 때 아이템 제거
            // 데미지 처리 추가 가능
        }
    }

    void Start()
    {
        if (gameClearUI != null)
            gameClearUI.SetActive(false);
    }

    public void TakeHit()
    {
        currentHits++;

        if (currentHits >= maxHits)
        {
            Die();
        }
    }

    void Die()
    {
        if (gameClearUI != null)
            gameClearUI.SetActive(true); // 클리어 UI 출력

        Time.timeScale = 0f; // 게임 일시정지 (선택)
        gameObject.SetActive(false); // 보스 비활성화
    }
}
