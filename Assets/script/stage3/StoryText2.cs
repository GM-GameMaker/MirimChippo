using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using TMPro;  // TextMeshPro 네임스페이스 추가

public class StoryText3 : MonoBehaviour, IPointerClickHandler
{


    public TMP_Text dialogueText;  // 대사 표시할 Text UI
    private int dialogueCnt = 0;  // 대사 인덱스
    private List<string> dialogue = new List<string>();  // 대사 리스트

    // 스크립트 시작 시 대사 리스트 초기화
    void Start()
    {
        // 대사 추가 
        dialogue.Add("여기까지 오는데 진짜 쉽지 않았어.  \r\n이제 면접만 보면.....");
        dialogue.Add("하… 심장이 왜 이렇게 뛰지…  \r\n기술 질문만 아니면 괜찮을 텐데.");
        dialogue.Add(" -띠링—  \r\n“3번 지원자, 입장 준비해 주세요.”.");
        dialogue.Add("드디어 내 차례야.   ");
        dialogue.Add("3년 동안 열심히 했으니까 잘할 수 있겠지?");
        dialogue.Add("괜찮아, 나는 준비됐어.  ");

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
            SceneManager.LoadScene("stage3-1");
        }
    }
}
