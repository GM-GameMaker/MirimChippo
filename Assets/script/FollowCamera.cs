using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    public Transform player;  // �÷��̾��� Transform (Inspector�� �־���� ��!)
    public Vector3 offset;    // ī�޶� �÷��̾�κ��� �󸶳� �������� (x, y, z �Ÿ�)

    void LateUpdate()
    {
        // �÷��̾� ��ġ + �����¸�ŭ ī�޶� ��ġ �̵�
        transform.position = player.position + offset;
    }
}
