using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    public GameObject player; // 플레이어 오브젝트를 지정
    public float backgroundSpeed = 0.1f; // 배경의 움직임 속도 (0.1f는 예시)

    private Vector3 lastPlayerPosition;

    void Start()
    {
        if (player == null)
        {
            Debug.LogWarning("플레이어 오브젝트가 설정되지 않았습니다. BackgroundMovement 스크립트에 플레이어 오브젝트를 연결해주세요.");
        }
        lastPlayerPosition = player.transform.position;
    }

    void Update()
    {
        if (player == null)
        {
            return;
        }

        // 플레이어의 위치 변화 계산
        Vector3 playerMovement = player.transform.position - lastPlayerPosition;
        lastPlayerPosition = player.transform.position;

        // 배경 오브젝트의 위치 조정 (플레이어 이동 방향 반대로)
        transform.position -= playerMovement * backgroundSpeed;
    }
}