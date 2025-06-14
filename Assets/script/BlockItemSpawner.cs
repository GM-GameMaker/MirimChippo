using UnityEngine;

public class TextbookBlock : MonoBehaviour
{
    public GameObject item0Prefab;
    public GameObject item100Prefab;
    public Transform spawnPoint;  // 블럭 위에 빈 오브젝트

    private bool isHit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isHit && collision.gameObject.CompareTag("Player"))
        {
            if (collision.contacts[0].normal.y > 0.5f) // 아래에서 부딪혔을 때만
            {
                isHit = true;
                SpawnRandomItem();
            }
        }
    }

    void SpawnRandomItem()
    {
        GameObject itemToSpawn = Random.value < 0.5f ? item0Prefab : item100Prefab;
        Instantiate(itemToSpawn, spawnPoint.position, Quaternion.identity);
    }
}
