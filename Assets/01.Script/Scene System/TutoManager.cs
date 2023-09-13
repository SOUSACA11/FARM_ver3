using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//by.J:230904 대화 스크립트 관리
public class TutoManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; //텍스트
    public GameObject clickPromptPanel; //텍스트 패널
    
    private Animator animator;
    private Queue<string> dialogueQueue = new Queue<string>();

    private TouchManager touchManager;

    private bool hasShownFarmPlayDialogue = false;  //FarmPlay 대화창
    private bool hasShownFarmPlay4Dialogue = false; //FarmPlay4 대화창
    private bool hasShownFarmPlay5Dialogue = false; //FarmPlay5 대화창
    private bool hasShownStoragePlayDialogue = false; //StoragePlay 대화창
    private bool hasShownStoragePlay2Dialogue = false; //StoragePlay2 대화창
    private bool hasShownStoragePlay3Dialogue = false; //StoragePlay3 대화창
    private bool hasShownBuildingPlay2Dialogue = false; //BuildingPlay2 대화창

    public GameObject storeArrow; //상점 화살표
    public GameObject farmArrow;  //농장밭 화살표
    public GameObject farmArrow2; //밀밭 화살표
    public GameObject farmArrow3; //닫기 화살표
    public GameObject storageArrow; //창고 화살표
    public GameObject tArrow; //창고탭 화살표
    public GameObject tArrow2; //창고탭2 화살표
    public GameObject tArrow3; //닫기 화살표

    //각 화살표가 가리키는 대상의 참조
    //public GameObject targetForStoreArrow; //상점 버튼
    //public GameObject targetForFarmArrow;  //농장밭탭
    //public GameObject targetForFarmArrow2; //밀밭
    //public GameObject targetForFarmArrow3; //닫기 버튼

    void Start()
    {
        //touchManager = FindObjectOfType<TouchManager>();
        //if (touchManager != null)
        //{
        //    touchManager.enabled = false; // 튜토리얼 시작 시 TouchManager 비활성화
        //}

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
            dialogueQueue.Enqueue("농장밭을 선택해서 밀을 심어보자");
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

        if (!hasShownFarmPlay5Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay5"))
        {
            StartCoroutine(ShowFarmPlay5DialogueAfterDelay(5f)); //5초 뒤에 대화 표시
        }

        if (!hasShownStoragePlayDialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay"))
        {
            StartCoroutine(ShownStoragePlayDialogueAfterDelay(0.5f)); //0.5초 뒤에 대화 표시
        }

        if (!hasShownStoragePlay2Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay2"))
        {
            StartCoroutine(ShownStoragePlay2DialogueAfterDelay(1f)); //1초 뒤에 대화 표시
        }

        if (!hasShownStoragePlay3Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay3"))
        {
            StartCoroutine(ShownStoragePlay3DialogueAfterDelay(0.5f)); //0.5초 뒤에 대화 표시
        }

        if (!hasShownBuildingPlay2Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay2"))
        {
            clickPromptPanel.SetActive(true);
            hasShownBuildingPlay2Dialogue = true; //대화를 한 번만 표시하도록 플래그 설정
            dialogueQueue.Enqueue("빵집을 설치해보자");
            ShowDialogue();
        }

        else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Dialogue") && Input.GetMouseButtonDown(0))
        {
            ShowDialogue();
        }

        HandleUserInput();
    }

    IEnumerator ShowFarmPlay5DialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); //지정된 시간(초) 동안 대기

        if (!hasShownFarmPlay5Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay5"))
        {
            clickPromptPanel.SetActive(true);
            hasShownFarmPlay5Dialogue = true;
            dialogueQueue.Enqueue("밀이 다 자란것 같아");
            dialogueQueue.Enqueue("밀을 클릭해서 수확해보자");
            ShowDialogue();
        }
    }
 
    IEnumerator ShownStoragePlayDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); //지정된 시간(초) 동안 대기

        if (!hasShownStoragePlayDialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay"))
        {
            clickPromptPanel.SetActive(true);
            hasShownStoragePlayDialogue = true; //대화를 한 번만 표시하도록 플래그 설정
            dialogueQueue.Enqueue("수확한 밀은 창고에 자동으로 들어가져");
            dialogueQueue.Enqueue("창고를 클릭해서 확인해보자");
            ShowDialogue();
        }
    }

    IEnumerator ShownStoragePlay2DialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); //지정된 시간(초) 동안 대기

        if (!hasShownStoragePlay2Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay2"))
        {
            storageArrow.SetActive(false); //창고
            tArrow.SetActive(true); //창고탭1
            tArrow2.SetActive(true); //창고탭2
            clickPromptPanel.SetActive(true);
            hasShownStoragePlay2Dialogue = true; //대화를 한 번만 표시하도록 플래그 설정
            dialogueQueue.Enqueue("농장에서 수확하는 작물은 작물창에서 볼 수 있고");
            dialogueQueue.Enqueue("가공해서 얻는 가공품은 생산품창에서 볼 수 있어");
            ShowDialogue();
        }
    }

    IEnumerator ShownStoragePlay3DialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); //지정된 시간(초) 동안 대기

        if (!hasShownStoragePlay3Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay3"))
        {
            tArrow.SetActive(false); //창고탭1
            tArrow2.SetActive(false); //창고탭2
            clickPromptPanel.SetActive(true);
            hasShownStoragePlay3Dialogue = true; //대화를 한 번만 표시하도록 플래그 설정
            dialogueQueue.Enqueue("수확한 작물들로 다양한 생산품을 만들 수 있어");
            dialogueQueue.Enqueue("밀을 활용해서 식빵을 만들어보자");
            ShowDialogue();
        }
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
            //Vector2 rayPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            //RaycastHit2D hit = Physics2D.Raycast(rayPos, Vector2.zero);


            if (animator.GetCurrentAnimatorStateInfo(0).IsName("ClickPrompt"))
            {
                storeArrow.SetActive(true); //상점
                animator.SetTrigger("ToStoreArrow");       
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("StoreArrow"))
            {
                    storeArrow.SetActive(false); //상점
                    animator.SetTrigger("ToFarm");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay")) //FarmPlay : 대화창 오픈 - 밭 설치 유도
            {
                clickPromptPanel.SetActive(false); //대화창
                animator.SetTrigger("ToFarm2");
                farmArrow.SetActive(true); //농장밭

            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay2")) //FarmPlay2 : 대화창 없어지고 농장밭 가르키기
            {
                farmArrow.SetActive(false); //농장밭
                animator.SetTrigger("ToFarm3");
                farmArrow2.SetActive(true); //밀밭
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay3")) //FarmPlay3 : 밀밭 가르키기
            {
                animator.SetTrigger("ToFarm4");
                farmArrow2.SetActive(false); //밀밭
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay4")) //FarmPlay4 : 대화창 오픈 - 밭 성장 설명 / 닫기 버튼가르키기
            {
                if (dialogueQueue.Count > 0)
                {
                    ShowDialogue();
                }
                else if (dialogueQueue.Count == 0 && clickPromptPanel.activeSelf) //대화 종료 대화창 활성화
                {
                    clickPromptPanel.SetActive(false); //대화창을 비활성화
                    farmArrow3.SetActive(true); //상점 닫기
                }
                else if (dialogueQueue.Count == 0 && !clickPromptPanel.activeSelf) //대화 종료 대화창 비활성화
                { 
                    animator.SetTrigger("ToFarm5");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay5")) //FarmPlay5 : 5초뒤 대화창 오픈 / 성장 완료된 밭 클릭 유도
            {
                if (dialogueQueue.Count > 0) 
                {
                    ShowDialogue();
                }
     
                else
                {
                    clickPromptPanel.SetActive(false); //대화
                    animator.SetTrigger("ToRest");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rest")) //Rest : 쉬어가기
            {
                animator.SetTrigger("ToStorage");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay")) //StoragePlay : 1초뒤 대화창 오픈 / 창고 클릭 유도
            {
                if (dialogueQueue.Count > 0)
                {
                    ShowDialogue();
                }
                else
                {
                    clickPromptPanel.SetActive(false); //대화
                    storageArrow.SetActive(true); //창고
                    animator.SetTrigger("ToStorage2");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay2")) //StoragePlay2 : 창고 설명
            {
                if (dialogueQueue.Count > 0)
                {
                    ShowDialogue();
                }
                else if (dialogueQueue.Count == 0 && clickPromptPanel.activeSelf) //대화 종료 대화창 활성화
                {
                    clickPromptPanel.SetActive(false); //대화창을 비활성화
                }
                else if (dialogueQueue.Count == 0 && !clickPromptPanel.activeSelf) //대화 종료 대화창 비활성화
                {
                    animator.SetTrigger("ToRest2");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rest2")) //Rest2 : 쉬어가기
            {
                if (dialogueQueue.Count > 0)
                {
                    ShowDialogue();
                }
                else if (dialogueQueue.Count == 0 && clickPromptPanel.activeSelf) //대화 종료 대화창 활성화
                {
                    clickPromptPanel.SetActive(false); //대화창을 비활성화
                }
                else if (dialogueQueue.Count == 0 && !clickPromptPanel.activeSelf) //대화 종료 대화창 비활성화
                {
                    animator.SetTrigger("ToStorage3");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay3")) //StoragePlay3 : 창고 닫기
            {
                if (dialogueQueue.Count > 0) //대화 큐에 아직 내용이 있다면
                {
                    ShowDialogue(); //다음 대화를 보여준다
                }
                else
                {
                    clickPromptPanel.SetActive(false); //대화
                    tArrow3.SetActive(true); //창고닫기
                    animator.SetTrigger("ToBuilding");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay")) //BuildingPlay : 상점 버튼 보여주기
            {
                storeArrow.SetActive(true); //상점
                animator.SetTrigger("ToBuilding2");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay2")) //BuildingPlay2 : 상점 버튼에서 빌딩 소개
            {
                if (dialogueQueue.Count > 0) //대화 큐에 아직 내용이 있다면
                {
                    ShowDialogue(); //다음 대화를 보여준다
                }
                else
                {
                    clickPromptPanel.SetActive(false); //대화
                    //animator.SetTrigger("ToBuilding");
                }
            }
        }
    }
}