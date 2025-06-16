using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManeger4 : MonoBehaviour
{
    public GameObject stageTextUI;
    public TMP_Text stageText;
    public string stageName = "º¸½ºÀü";

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
