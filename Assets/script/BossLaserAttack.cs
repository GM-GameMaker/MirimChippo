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

    public float paperAttackInterval = 15f;
    public float laserAttackInterval = 10f;
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
        foreach (var laser in lasers)
            laser.SetActive(false);
    }

    void LaserAttack()
    {
        HideAllLasers();

        int index = Random.Range(0, lasers.Length);
        GameObject selectedLaser = lasers[index];

        selectedLaser.transform.position = laserPositions[index].position;
        selectedLaser.SetActive(true);

        StartCoroutine(DisableAfterSeconds(selectedLaser, 0.5f));
    }

    IEnumerator DisableAfterSeconds(GameObject obj, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        obj.SetActive(false);
    }

    void PaperAttack()
    {
        int spawnIndex = Random.Range(0, paperSpawnPoints.Length);
        GameObject paperPrefab = papers[Random.Range(0, papers.Length)];
        Transform spawnPoint = paperSpawnPoints[spawnIndex];

        Instantiate(paperPrefab, spawnPoint.position, Quaternion.identity);
    }
}
