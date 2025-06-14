using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;  // 플레이어의 Transform (Inspector에 넣어줘야 해!)
    public Vector3 offset;    // 카메라가 플레이어로부터 얼마나 떨어질지 (x, y, z 거리)

    void LateUpdate()
    {
        // 플레이어 위치 + 오프셋만큼 카메라 위치 이동
        transform.position = player.position + offset;
    }
}
