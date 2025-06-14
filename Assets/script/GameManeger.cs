using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject stageTextUI;
    public TMP_Text stageText;
    public string stageName = "스테이지 1";

    void Start()
    {
        stageText.text = stageName;
        StartCoroutine(ShowStageText());
    }

    IEnumerator ShowStageText()
    {
        stageTextUI.SetActive(true);
        yield return new WaitForSeconds(1f);
        stageTextUI.SetActive(false);
    }
}
