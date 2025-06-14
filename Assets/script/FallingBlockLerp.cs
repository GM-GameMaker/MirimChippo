using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatingBlockOnly : MonoBehaviour
{
    public float rotateSpeed = 180f; // 초당 180도 회전

    void Update()
    {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
    }
}
