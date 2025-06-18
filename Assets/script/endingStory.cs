using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using TMPro;  // TextMeshPro 네임스페이스 추가

public class EndingStory: MonoBehaviour, IPointerClickHandler
{


    public TMP_Text dialogueText;  // 대사 표시할 Text UI
    private int dialogueCnt = 0;  // 대사 인덱스
    private List<string> dialogue = new List<string>();  // 대사 리스트

    // 스크립트 시작 시 대사 리스트 초기화
    void Start()
    {
        // 대사 추가 
        dialogue.Add("며칠 후");
        dialogue.Add("-띠링-");
        dialogue.Add("(메일함을 확인한다.)");
        dialogue.Add("`축하합니다. 미림소프트에 최종 합격하셨습니다.`");
        dialogue.Add("오… 오 이건 꿈 아니지!?");
        dialogue.Add("나 드디어 취뽀했어~~!!");
        dialogue.Add("당신은 모든 스테이지를 클리어하고\r\n미림 최고의 개발자로 취뽀했습니다.");

    }

    // 대사창 클릭 시 호출되는 함수
    public void OnPointerClick(PointerEventData eventData)
    {
        if (dialogueCnt < dialogue.Count)
        {
            dialogueText.text = dialogue[dialogueCnt++];  // 현재 대사 출력 후 인덱스 증가
        }
        else
        {
            SceneManager.LoadScene("startScene");
        }
    }
}
