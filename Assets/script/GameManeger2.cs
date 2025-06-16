using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManeger2 : MonoBehaviour
{
    public GameObject stageTextUI;
    public TMP_Text stageText;
    public string stageName = "스테이지 2";

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
