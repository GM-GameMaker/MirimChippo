using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FlagTrigger : MonoBehaviour
{
    public string nextSceneName = "stage3-2";

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("충돌 발생 with: " + other.name);

        if (other.CompareTag("Player"))
        {
            Debug.Log("Player 감지 - 씬 전환");
            SceneManager.LoadScene(nextSceneName);
        }
    }
}
