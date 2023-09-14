using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//by.J:230904 대화 스크립트 관리
//by.J:230912 애니메이터 컨트롤러 적용
public class TutoManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; //텍스트
    public GameObject clickPromptPanel; //텍스트 패널
    
    private Animator animator;
    private Queue<string> dialogueQueue = new Queue<string>();

    private bool hasShownFarmPlayDialogue = false;  //FarmPlay 대화창
    private bool hasShownFarmPlay4Dialogue = false; //FarmPlay4 대화창
    private bool hasShownFarmPlay5Dialogue = false; //FarmPlay5 대화창
    private bool hasShownStoragePlayDialogue = false; //StoragePlay 대화창
    private bool hasShownStoragePlay2Dialogue = false; //StoragePlay2 대화창
    private bool hasShownStoragePlay3Dialogue = false; //StoragePlay3 대화창
    private bool hasShownBuildingPlay2Dialogue = false; //BuildingPlay2 대화창
    private bool hasShownBuildingPlay5Dialogue = false; //BuildingPlay5 대화창
    private bool hasShownBuildingPlay6Dialogue = false; //BuildingPlay6 대화창
    private bool hasShownOrderPlayDialogue = false; //OrderPlay 대화창
    private bool hasShownOrderPlay2Dialogue = false; //OrderPlay2 대화창

    public GameObject storeArrow;   //상점 화살표
    public GameObject farmArrow;    //농장밭탭 화살표
    public GameObject farmArrow2;   //밀밭 화살표
    public GameObject farmArrow3;   //상점 닫기 화살표
    public GameObject storageArrow; //창고 화살표
    public GameObject tArrow;       //창고탭 화살표
    public GameObject tArrow2;      //창고탭2 화살표
    public GameObject tArrow3;      //창고 닫기 화살표
    public GameObject storeArrow2;  //상점2 화살표
    public GameObject farmArrow4;   //빌딩탭 화살표
    public GameObject farmArrow5;   //빵집 화살표
    public GameObject farmArrow6;   //상점 닫기2 화살표
    public GameObject orderArrow;   //주문 화살표

    void Start()
    {
        animator = GetComponent<Animator>();
        clickPromptPanel.SetActive(true);

        //초기 대화 설정
        dialogueQueue.Enqueue("안녕! 나는 피피 라고 해");
        dialogueQueue.Enqueue("마을 모습이 조금 이상하지?");
        dialogueQueue.Enqueue("옛날에는 푸르고 주민들도 많았는데");
        dialogueQueue.Enqueue("도시로 주민들이 떠나면서 버려지게 되었어");
        dialogueQueue.Enqueue("...");
        dialogueQueue.Enqueue("웃음소리가 가득한 그 모습을 보고싶어...");
        dialogueQueue.Enqueue("나 혼자서 겨우 집 하나 완성했는데...");
        dialogueQueue.Enqueue("...?!");
        dialogueQueue.Enqueue("너가 푸른 마을이 되도록 도와주겠다고?");
        dialogueQueue.Enqueue("고마워! 방법은 내가 알고있어!");
        dialogueQueue.Enqueue("먼저 상점 창을 열어서 밭을 설치하면돼");
        ShowDialogue();
    }

    void Update()
    {
       
        if (!hasShownFarmPlayDialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay"))
        {
            clickPromptPanel.SetActive(true);
            hasShownFarmPlayDialogue = true; //대화를 한 번만 표시하도록 플래그 설정
            dialogueQueue.Enqueue("그리고 농장밭 탭을 선택해서 밀을 심어봐");
            ShowDialogue();
        }

        if (!hasShownFarmPlay4Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay4"))
        {
            clickPromptPanel.SetActive(true);
            hasShownFarmPlay4Dialogue = true; //대화를 한 번만 표시하도록 플래그 설정
            dialogueQueue.Enqueue("설치한 밭은 시간이 지나면 성장해");
            dialogueQueue.Enqueue("상점 창을 닫고 기다려보자");
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
            tArrow.SetActive(true); //창고탭1
            tArrow2.SetActive(true); //창고탭2
            clickPromptPanel.SetActive(true);
            hasShownStoragePlay2Dialogue = true; //대화를 한 번만 표시하도록 플래그 설정
            dialogueQueue.Enqueue("농장에서 수확하는 작물은 작물창에서 볼 수 있고");
            dialogueQueue.Enqueue("가공해서 얻는 가공품은 생산품창에서 볼 수 있어");
            ShowDialogue();
        }

        if (!hasShownStoragePlay3Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay3"))
        {
            StartCoroutine(ShownStoragePlay3DialogueAfterDelay(0.3f)); //0.3초 뒤에 대화 표시
        }

        if (!hasShownBuildingPlay2Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay2"))
        {
            storeArrow2.SetActive(false); //상점2
            clickPromptPanel.SetActive(true);
            hasShownBuildingPlay2Dialogue = true; //대화를 한 번만 표시하도록 플래그 설정
            dialogueQueue.Enqueue("빵집을 설치해보자");
            ShowDialogue();
        }

        if (!hasShownBuildingPlay5Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay5"))
        {
            clickPromptPanel.SetActive(true);
            hasShownBuildingPlay5Dialogue = true; //대화를 한 번만 표시하도록 플래그 설정
            dialogueQueue.Enqueue("빵집을 클릭하면 만들 수 있는 생산품 목록을 보여줘");
            dialogueQueue.Enqueue("생산품을 클릭하면 필요한 재료를 볼 수 있어");
            dialogueQueue.Enqueue("식빵을 클릭해서 빵집으로 끌어 놓아서 만들어 보자");
            ShowDialogue();
        }

        if (!hasShownBuildingPlay6Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay6"))
        {
            clickPromptPanel.SetActive(true);
            hasShownBuildingPlay6Dialogue = true; //대화를 한 번만 표시하도록 플래그 설정
            dialogueQueue.Enqueue("빵집이 생산중이면 움직이고 있고");
            dialogueQueue.Enqueue("생산이 완료되면 깜박이면서 움직임이 멈춰");
            dialogueQueue.Enqueue("그리고 생산품은 자동으로 창고에 들어가져");
            ShowDialogue();
        }

        if(!hasShownOrderPlayDialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("OrderPlay"))
        {
            StartCoroutine(ShownOrderPlayDialogueAfterDelay(0.5f)); //0.5초 뒤에 대화 표시
        }

        if (!hasShownOrderPlay2Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("OrderPlay2"))
        {
            StartCoroutine(ShownOrderPlay2DialogueAfterDelay(0.5f)); //0.5초 뒤에 대화 표시
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

    IEnumerator ShownOrderPlayDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); //지정된 시간(초) 동안 대기

        if (!hasShownOrderPlayDialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("OrderPlay"))
        {
            clickPromptPanel.SetActive(true);
            hasShownOrderPlayDialogue = true; //대화를 한 번만 표시하도록 플래그 설정
            dialogueQueue.Enqueue("획득한 생산품으로 납품을 해서 돈을 벌 수 있어");
            ShowDialogue();
        }
    }
    IEnumerator ShownOrderPlay2DialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); //지정된 시간(초) 동안 대기

        if (!hasShownOrderPlay2Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("OrderPlay2"))
        {
            clickPromptPanel.SetActive(true);
            hasShownOrderPlay2Dialogue = true; //대화를 한 번만 표시하도록 플래그 설정
            dialogueQueue.Enqueue("열심히 작물을 심고 수확해서");
            dialogueQueue.Enqueue("다양한 생산품을 만들어서");
            dialogueQueue.Enqueue("마을을 가꿔줘");
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

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("ClickPrompt"))
            {
                clickPromptPanel.SetActive(false); //대화창
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
                farmArrow3.SetActive(false); //상점 닫기

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
                    
                    animator.SetTrigger("ToRest3");
                    storageArrow.SetActive(true); //창고
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rest3")) //Rest3 : 창고닫기 / 쉬어가기
            {
                storageArrow.SetActive(false); //창고
                animator.SetTrigger("ToStorage2");
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
                else if (dialogueQueue.Count == 0 && clickPromptPanel.activeSelf) //대화 종료 대화창 활성화
                {
                    clickPromptPanel.SetActive(false); //대화창을 비활성화
                    tArrow3.SetActive(true); //창고닫기
                }
                else if (dialogueQueue.Count == 0 && !clickPromptPanel.activeSelf) //대화 종료 대화창 비활성화
                {
                    storeArrow2.SetActive(true); //상점2
                    animator.SetTrigger("ToBuilding");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay")) //BuildingPlay : 상점 버튼 보여주기
            {
                animator.SetTrigger("ToBuilding2");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay2")) //BuildingPlay2 : 상점에서 빌딩탭 가르키기
            {
                if (dialogueQueue.Count > 0) //대화 큐에 아직 내용이 있다면
                {
                    ShowDialogue(); //다음 대화를 보여준다
                }
                else
                {
                    clickPromptPanel.SetActive(false); //대화
                    farmArrow4.SetActive(true); //빌딩탭
                    animator.SetTrigger("ToBuilding3");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay3")) //BuildingPlay3 : 빵집 가르키기
            {
                farmArrow4.SetActive(false); //빌딩탭
                farmArrow5.SetActive(true); //빵집
                animator.SetTrigger("ToBuilding4");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay4")) //BuildingPlay4 : 상점 닫기2
            {
                farmArrow5.SetActive(false); //빵집
                farmArrow6.SetActive(true); //상점 닫기2
                animator.SetTrigger("ToRest4");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rest4")) //Rest4 : 상점닫기 / 쉬어가기
            {
                farmArrow6.SetActive(false); //상점 닫기2
                animator.SetTrigger("ToBuilding5");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay5")) //BuildingPlay5 : 대화창 - 빵집 생산 유도
            {
                if (dialogueQueue.Count > 0) //대화 큐에 아직 내용이 있다면
                {
                    ShowDialogue(); //다음 대화를 보여준다
                }
                else
                {
                    clickPromptPanel.SetActive(false); //대화
                    animator.SetTrigger("ToRest5");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rest5")) //Rest5 : 쉬어가기
            {
                animator.SetTrigger("ToRest6");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rest6")) //Rest6 : 쉬어가기
            {
                animator.SetTrigger("ToRest7");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rest7")) //Rest7 : 쉬어가기
            {
                animator.SetTrigger("ToBuilding6");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay6")) //BuildingPlay6 : 대화창 - 생산 애니메이션 설명
            {
                if (dialogueQueue.Count > 0) //대화 큐에 아직 내용이 있다면
                {
                    ShowDialogue(); //다음 대화를 보여준다
                }
                else
                {
                    clickPromptPanel.SetActive(false); //대화
                    animator.SetTrigger("ToOrder");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("OrderPlay")) //OrderPlay : 대화창 - 돈 버는 방법
            {
                if (dialogueQueue.Count > 0) //대화 큐에 아직 내용이 있다면
                {
                    ShowDialogue(); //다음 대화를 보여준다
                }
                else
                {
                    clickPromptPanel.SetActive(false); //대화
                    orderArrow.SetActive(true); //주문
                    animator.SetTrigger("ToRest8");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rest8")) //Rest8 : 쉬어가기
            {
                orderArrow.SetActive(false); //주문
                animator.SetTrigger("ToOrder2");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("OrderPlay2")) //OrderPlay2 : 대화창 - 마무리
            {
                
                if (dialogueQueue.Count > 0) //대화 큐에 아직 내용이 있다면
                {
                    ShowDialogue(); //다음 대화를 보여준다
                }
                else
                {
                    clickPromptPanel.SetActive(false); //대화
                    animator.SetTrigger("ToRest9");
                }
            }


            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rest9")) //Rest9 : 쉬어가기
            {
                SceneManager.LoadScene("Main");
            }
        }
    }
}