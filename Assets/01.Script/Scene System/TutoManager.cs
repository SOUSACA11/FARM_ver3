using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//by.J:230904 대화 스크립트 관리
public class TutoManager : MonoBehaviour
{
    public TutorialState currentState = TutorialState.None;
    public TextMeshProUGUI dialogueText; //텍스트
    public GameObject clickPromptPanel; //텍스트 패널
    
    private Animator animator;
    private Queue<string> dialogueQueue = new Queue<string>();

    private bool hasShownFarmPlayDialogue = false; //FarmPlay 대화창
    private bool hasShownFarmPlay4Dialogue = false; //FarmPlay4 대화창

    public GameObject storePanel; //스토어 패널
    public GameObject farmArrow; //농장밭 화살표
    public GameObject farmArrow2; //밀밭 화살표

    public enum TutorialState
    {
        None,
        Dialogue,
        ClickPrompt,
        StoreArrow,
        FarmPlay,
        FarmPlay2,
        FarmPlay3
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        clickPromptPanel.SetActive(true);

        //초기 대화 설정
        dialogueQueue.Enqueue("안녕!");
        dialogueQueue.Enqueue("우리 마을을 ~~~");
        dialogueQueue.Enqueue("그럼 상점 창을 열어서 밭을 설치해보자");
        ShowDialogue();
    }

    void Update()
    {
        if (!hasShownFarmPlayDialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay"))
        {
            clickPromptPanel.SetActive(true);
            hasShownFarmPlayDialogue = true; //대화를 한 번만 표시하도록 플래그 설정
            dialogueQueue.Enqueue("그럼 농장밭을 선택해서 밀을 심어보자");
            ShowDialogue();
        }
        if (!hasShownFarmPlay4Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay4"))
        {
            clickPromptPanel.SetActive(true);
            hasShownFarmPlay4Dialogue = true; //대화를 한 번만 표시하도록 플래그 설정
            dialogueQueue.Enqueue("설치한 밭은 시간이 지나면 성장해");
            dialogueQueue.Enqueue("상점 창을 닫고 기다려볼까?");
            ShowDialogue();
        }
        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dialogue") && Input.GetMouseButtonDown(0))
        {
            ShowDialogue();
        }

        HandleUserInput();
    }

    void ShowDialogue()
    {
        if (dialogueQueue.Count > 0)
        {
            dialogueText.text = dialogueQueue.Dequeue();
        }
        else
        {
            animator.SetTrigger("ToClick"); // 모든 대화가 끝나면 ClickPrompt 상태로 전환
        }
    }
    void HandleUserInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("ClickPrompt"))
            {
                storePanel.SetActive(true);
                animator.SetTrigger("ToStoreArrow");
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("StoreArrow"))
            {
                storePanel.SetActive(false);
                animator.SetTrigger("ToFarm");
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay")) //FarmPlay : 대화창 오픈 - 밭 설치 유도
            {
                clickPromptPanel.SetActive(false);
                animator.SetTrigger("ToFarm2");
                farmArrow.SetActive(true); //농장밭

            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay2")) //FarmPlay2 : 대화창 없어지고 농장밭 가르키기
            {
                animator.SetTrigger("ToFarm3");
                farmArrow.SetActive(false); //농장밭
                farmArrow2.SetActive(true); //밀밭
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay3")) //FarmPlay3 : 밀밭 가르키기
            {
                animator.SetTrigger("ToFarm4");
                farmArrow2.SetActive(false); //밀밭
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay4")) //FarmPlay4 : 대화창 오픈 - 밭 성장 설명
            {
                clickPromptPanel.SetActive(false);
                animator.SetTrigger("ToFarm5");
            }
        }
    }
}