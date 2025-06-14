using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using TMPro;  // TextMeshPro 네임스페이스 추가

public class StoryText2 : MonoBehaviour, IPointerClickHandler
{


    public TMP_Text dialogueText;  // 대사 표시할 Text UI
    private int dialogueCnt = 0;  // 대사 인덱스
    private List<string> dialogue = new List<string>();  // 대사 리스트

    // 스크립트 시작 시 대사 리스트 초기화
    void Start()
    {
        // 대사 추가 
        dialogue.Add("다음 주까지 파이썬 프젝 마감인가?");
        dialogue.Add("수행평가 프로젝트 때문에 요즘 진짜 빡세다ㅠ");
        dialogue.Add("어제는 꼬박 밤을 샜어...");
        dialogue.Add("근데 확실히 1학년 때보단 실력이 조금씩 늘고 있는 것 같다.");
        dialogue.Add("그리고 하다 보면 시간 가는 줄 모를 때도 있더라.");
        dialogue.Add("이상하게 힘든데… 또 재미있어. 나, 이 길 잘 가고 있는 걸까?");

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
            SceneManager.LoadScene("stage2");
        }
    }
}
