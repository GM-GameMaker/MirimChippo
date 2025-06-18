using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLaserAttack : MonoBehaviour
{
    [Header("서류 공격 설정")]
    public GameObject[] papers;
    public Transform[] paperSpawnPoints;

    [Header("레이저 공격 설정")]
    public GameObject[] lasers;
    public Transform[] laserPositions;

    [Header("회복 아이템 설정")]
    public GameObject[] batteries;
    public Transform[] batterySpawnPoints;

    public float paperAttackInterval = 8f;
    public float laserAttackInterval = 7f;
    public float bossTotalTime = 40f;
    private float bossTimeLeft;

    private float paperAttackTimer = 0f;
    private float laserAttackTimer = 0f;

    void Start()
    {
        bossTimeLeft = bossTotalTime;
        HideAllLasers();
    }

    void Update()
    {
        bossTimeLeft -= Time.deltaTime;

        paperAttackTimer += Time.deltaTime;
        if (paperAttackTimer >= paperAttackInterval)
        {
            PaperAttack();
            paperAttackTimer = 0f;
        }

        laserAttackTimer += Time.deltaTime;
        if (laserAttackTimer >= laserAttackInterval)
        {
            LaserAttack();
            laserAttackTimer = 0f;
        }
    }

    void HideAllLasers()
    {
        foreach (GameObject laser in lasers)
        {
            if (laser && laser.activeInHierarchy)
            {
                laser.SetActive(false);
            }
        }
    }

    void LaserAttack()
    {
        HideAllLasers();

        int index = Random.Range(0, lasers.Length);
        GameObject selectedLaser = lasers[index];

        if (selectedLaser != null)
        {
            selectedLaser.transform.position = laserPositions[index].position;
            selectedLaser.SetActive(true);

            StartCoroutine(DisableAfterSeconds(selectedLaser, 0.5f));
        }
    }

    IEnumerator DisableAfterSeconds(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (obj != null)
            obj.SetActive(false);
    }

    void PaperAttack()
    {
        // 서류
    if (papers == null || papers.Length == 0 || paperSpawnPoints == null || paperSpawnPoints.Length == 0)
        return;

    int spawnIndex = Random.Range(0, paperSpawnPoints.Length);
    GameObject paperPrefab = papers[Random.Range(0, papers.Length)];
    Transform spawnPoint = paperSpawnPoints[spawnIndex];

    Instantiate(paperPrefab, spawnPoint.position, Quaternion.identity);

    // 건전지 
    if (batteries != null && batteries.Length > 0 &&
        batterySpawnPoints != null && batterySpawnPoints.Length > 0 &&
        Random.value < 0.4f)
    {
        int batterySpawnIndex = Random.Range(0, batterySpawnPoints.Length);
        GameObject batteryPrefab = batteries[Random.Range(0, batteries.Length)];
        Transform batterySpawnPoint = batterySpawnPoints[batterySpawnIndex];

        Instantiate(batteryPrefab, batterySpawnPoint.position, Quaternion.identity);
    }
    }
}
