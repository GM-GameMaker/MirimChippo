using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundFollow : MonoBehaviour
{
    public Transform player;
    public GameObject[] backgrounds;
    public float switchDistance = 20f;
    private int leftIndex = 0;

    void Update()
    {
        if (player.position.x > backgrounds[leftIndex].transform.position.x + switchDistance)
        {
            // 현재 왼쪽에 있던 배경을 맨 뒤로 보냄
            int nextIndex = (leftIndex + backgrounds.Length - 1) % backgrounds.Length;
            Vector3 nextPos = backgrounds[nextIndex].transform.position + new Vector3(switchDistance, 0, 0);
            backgrounds[leftIndex].transform.position = nextPos;

            leftIndex = (leftIndex + 1) % backgrounds.Length;
        }
    }
}
