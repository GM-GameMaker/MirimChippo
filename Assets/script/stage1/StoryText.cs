using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

using TMPro;  // TextMeshPro 네임스페이스 추가

public class StoryText : MonoBehaviour, IPointerClickHandler
{


    public TMP_Text dialogueText;  // 대사 표시할 Text UI
    private int dialogueCnt = 0;  // 대사 인덱스
    private List<string> dialogue = new List<string>();  // 대사 리스트

    // 스크립트 시작 시 대사 리스트 초기화
    void Start()
    {
        // 대사 추가 
        dialogue.Add("헉 저기가 교문인가… 다들 되게 어른 같아… 나만 긴장했냐");
        dialogue.Add("가방도 무겁고 맘도 무거워… 근데 설렌다! 새로운 시작이니까!");
        dialogue.Add("교복 어색한 거 나뿐인가? 애들 사이로 들어가는 거 너무 떨려…");
        dialogue.Add("…그래, 용기 내보자. 오늘도, 앞으로도. 난 미림인이니까!");
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
            SceneManager.LoadScene("stage1");
        }
    }
}
