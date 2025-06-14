using UnityEngine;

public class TextbookBlock : MonoBehaviour
{
    public GameObject item0Prefab;
    public GameObject item100Prefab;
    public Transform spawnPoint;  // �� ���� �� ������Ʈ

    private bool isHit = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isHit && collision.gameObject.CompareTag("Player"))
        {
            if (collision.contacts[0].normal.y > 0.5f) // �Ʒ����� �ε����� ����
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
