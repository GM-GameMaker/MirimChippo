using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBlockOnly : MonoBehaviour
{
    public float rotateSpeed = 180f; // �ʴ� 180�� ȸ��

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
