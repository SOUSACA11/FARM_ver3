using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//by.J:230904 대화 스크립트 관리
public class TextManager : MonoBehaviour
{
    public GameObject dialoguePanel; // 대화 패널
    //public TextMeshProUGUI nameText; //NPC 이름
    public TextMeshProUGUI dialogueText; // 대사를 표시할 Text 컴포넌트

    public DragAndDropCamera dragAndDropCamera;
    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
        
        // 튜토리얼 시작 시 자동으로 대화 시작
        StartTutorialDialogue();
        dragAndDropCamera.NoDrag();
    }

    private void Update()
    {
        // 대화 패널이 활성화 상태이고 사용자가 클릭했을 때
        if (dialoguePanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            DisplayNextSentence();
        }
    }

    public void StartTutorialDialogue()
    {
       
        TextLog tutorialDialogue = new TextLog
        {
            //npcName = "튜토리얼 가이드",
            sentences = new string[] { "안녕, 내가 보낸 편지를 봤구나", "ㅇㅇㅇㅇㅇ" }
        };
        StartDialogue(tutorialDialogue);
    }


    public void StartDialogue(TextLog dialogue)
    {
        sentences.Clear();
        //nameText.text = dialogue.npcName;

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        dialoguePanel.SetActive(true); // 패널 활성화
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //문장이 더이상 없으면
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // 큐에서 다음 문장을 가져와 표시
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false); // 패널 비활성화
        dragAndDropCamera.OkDrag();
    }


}
