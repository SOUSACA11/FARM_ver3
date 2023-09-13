using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//by.J:230904 ��ȭ ��ũ��Ʈ ����
public class TutoManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; //�ؽ�Ʈ
    public GameObject clickPromptPanel; //�ؽ�Ʈ �г�
    
    private Animator animator;
    private Queue<string> dialogueQueue = new Queue<string>();

    private TouchManager touchManager;

    private bool hasShownFarmPlayDialogue = false;  //FarmPlay ��ȭâ
    private bool hasShownFarmPlay4Dialogue = false; //FarmPlay4 ��ȭâ
    private bool hasShownFarmPlay5Dialogue = false; //FarmPlay5 ��ȭâ
    private bool hasShownStoragePlayDialogue = false; //StoragePlay ��ȭâ
    private bool hasShownStoragePlay2Dialogue = false; //StoragePlay2 ��ȭâ
    private bool hasShownStoragePlay3Dialogue = false; //StoragePlay3 ��ȭâ
    private bool hasShownBuildingPlay2Dialogue = false; //BuildingPlay2 ��ȭâ

    public GameObject storeArrow; //���� ȭ��ǥ
    public GameObject farmArrow;  //����� ȭ��ǥ
    public GameObject farmArrow2; //�й� ȭ��ǥ
    public GameObject farmArrow3; //�ݱ� ȭ��ǥ
    public GameObject storageArrow; //â�� ȭ��ǥ
    public GameObject tArrow; //â���� ȭ��ǥ
    public GameObject tArrow2; //â����2 ȭ��ǥ
    public GameObject tArrow3; //�ݱ� ȭ��ǥ

    //�� ȭ��ǥ�� ����Ű�� ����� ����
    //public GameObject targetForStoreArrow; //���� ��ư
    //public GameObject targetForFarmArrow;  //�������
    //public GameObject targetForFarmArrow2; //�й�
    //public GameObject targetForFarmArrow3; //�ݱ� ��ư

    void Start()
    {
        //touchManager = FindObjectOfType<TouchManager>();
        //if (touchManager != null)
        //{
        //    touchManager.enabled = false; // Ʃ�丮�� ���� �� TouchManager ��Ȱ��ȭ
        //}

        animator = GetComponent<Animator>();
        clickPromptPanel.SetActive(true);

        //�ʱ� ��ȭ ����
        dialogueQueue.Enqueue("�ȳ�!");
        dialogueQueue.Enqueue("�츮 ������ ~~~");
        dialogueQueue.Enqueue("�׷� ���� â�� ��� ���� ��ġ�غ���");
        ShowDialogue();
    }

    void Update()
    {
       
        if (!hasShownFarmPlayDialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay"))
        {
            clickPromptPanel.SetActive(true);
            hasShownFarmPlayDialogue = true; //��ȭ�� �� ���� ǥ���ϵ��� �÷��� ����
            dialogueQueue.Enqueue("������� �����ؼ� ���� �ɾ��");
            ShowDialogue();
        }

        if (!hasShownFarmPlay4Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay4"))
        {
            clickPromptPanel.SetActive(true);
            hasShownFarmPlay4Dialogue = true; //��ȭ�� �� ���� ǥ���ϵ��� �÷��� ����
            dialogueQueue.Enqueue("��ġ�� ���� �ð��� ������ ������");
            dialogueQueue.Enqueue("���� â�� �ݰ� ��ٷ�����?");
            ShowDialogue();
        }

        if (!hasShownFarmPlay5Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay5"))
        {
            StartCoroutine(ShowFarmPlay5DialogueAfterDelay(5f)); //5�� �ڿ� ��ȭ ǥ��
        }

        if (!hasShownStoragePlayDialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay"))
        {
            StartCoroutine(ShownStoragePlayDialogueAfterDelay(0.5f)); //0.5�� �ڿ� ��ȭ ǥ��
        }

        if (!hasShownStoragePlay2Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay2"))
        {
            StartCoroutine(ShownStoragePlay2DialogueAfterDelay(1f)); //1�� �ڿ� ��ȭ ǥ��
        }

        if (!hasShownStoragePlay3Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay3"))
        {
            StartCoroutine(ShownStoragePlay3DialogueAfterDelay(0.5f)); //0.5�� �ڿ� ��ȭ ǥ��
        }

        if (!hasShownBuildingPlay2Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay2"))
        {
            clickPromptPanel.SetActive(true);
            hasShownBuildingPlay2Dialogue = true; //��ȭ�� �� ���� ǥ���ϵ��� �÷��� ����
            dialogueQueue.Enqueue("������ ��ġ�غ���");
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
        yield return new WaitForSeconds(delay); //������ �ð�(��) ���� ���

        if (!hasShownFarmPlay5Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay5"))
        {
            clickPromptPanel.SetActive(true);
            hasShownFarmPlay5Dialogue = true;
            dialogueQueue.Enqueue("���� �� �ڶ��� ����");
            dialogueQueue.Enqueue("���� Ŭ���ؼ� ��Ȯ�غ���");
            ShowDialogue();
        }
    }
 
    IEnumerator ShownStoragePlayDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); //������ �ð�(��) ���� ���

        if (!hasShownStoragePlayDialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay"))
        {
            clickPromptPanel.SetActive(true);
            hasShownStoragePlayDialogue = true; //��ȭ�� �� ���� ǥ���ϵ��� �÷��� ����
            dialogueQueue.Enqueue("��Ȯ�� ���� â�� �ڵ����� ����");
            dialogueQueue.Enqueue("â�� Ŭ���ؼ� Ȯ���غ���");
            ShowDialogue();
        }
    }

    IEnumerator ShownStoragePlay2DialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); //������ �ð�(��) ���� ���

        if (!hasShownStoragePlay2Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay2"))
        {
            storageArrow.SetActive(false); //â��
            tArrow.SetActive(true); //â����1
            tArrow2.SetActive(true); //â����2
            clickPromptPanel.SetActive(true);
            hasShownStoragePlay2Dialogue = true; //��ȭ�� �� ���� ǥ���ϵ��� �÷��� ����
            dialogueQueue.Enqueue("���忡�� ��Ȯ�ϴ� �۹��� �۹�â���� �� �� �ְ�");
            dialogueQueue.Enqueue("�����ؼ� ��� ����ǰ�� ����ǰâ���� �� �� �־�");
            ShowDialogue();
        }
    }

    IEnumerator ShownStoragePlay3DialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); //������ �ð�(��) ���� ���

        if (!hasShownStoragePlay3Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay3"))
        {
            tArrow.SetActive(false); //â����1
            tArrow2.SetActive(false); //â����2
            clickPromptPanel.SetActive(true);
            hasShownStoragePlay3Dialogue = true; //��ȭ�� �� ���� ǥ���ϵ��� �÷��� ����
            dialogueQueue.Enqueue("��Ȯ�� �۹���� �پ��� ����ǰ�� ���� �� �־�");
            dialogueQueue.Enqueue("���� Ȱ���ؼ� �Ļ��� ������");
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
            animator.SetTrigger("ToClick"); // ��� ��ȭ�� ������ ClickPrompt ���·� ��ȯ
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
                storeArrow.SetActive(true); //����
                animator.SetTrigger("ToStoreArrow");       
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("StoreArrow"))
            {
                    storeArrow.SetActive(false); //����
                    animator.SetTrigger("ToFarm");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay")) //FarmPlay : ��ȭâ ���� - �� ��ġ ����
            {
                clickPromptPanel.SetActive(false); //��ȭâ
                animator.SetTrigger("ToFarm2");
                farmArrow.SetActive(true); //�����

            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay2")) //FarmPlay2 : ��ȭâ �������� ����� ����Ű��
            {
                farmArrow.SetActive(false); //�����
                animator.SetTrigger("ToFarm3");
                farmArrow2.SetActive(true); //�й�
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay3")) //FarmPlay3 : �й� ����Ű��
            {
                animator.SetTrigger("ToFarm4");
                farmArrow2.SetActive(false); //�й�
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay4")) //FarmPlay4 : ��ȭâ ���� - �� ���� ���� / �ݱ� ��ư����Ű��
            {
                if (dialogueQueue.Count > 0)
                {
                    ShowDialogue();
                }
                else if (dialogueQueue.Count == 0 && clickPromptPanel.activeSelf) //��ȭ ���� ��ȭâ Ȱ��ȭ
                {
                    clickPromptPanel.SetActive(false); //��ȭâ�� ��Ȱ��ȭ
                    farmArrow3.SetActive(true); //���� �ݱ�
                }
                else if (dialogueQueue.Count == 0 && !clickPromptPanel.activeSelf) //��ȭ ���� ��ȭâ ��Ȱ��ȭ
                { 
                    animator.SetTrigger("ToFarm5");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay5")) //FarmPlay5 : 5�ʵ� ��ȭâ ���� / ���� �Ϸ�� �� Ŭ�� ����
            {
                if (dialogueQueue.Count > 0) 
                {
                    ShowDialogue();
                }
     
                else
                {
                    clickPromptPanel.SetActive(false); //��ȭ
                    animator.SetTrigger("ToRest");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rest")) //Rest : �����
            {
                animator.SetTrigger("ToStorage");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay")) //StoragePlay : 1�ʵ� ��ȭâ ���� / â�� Ŭ�� ����
            {
                if (dialogueQueue.Count > 0)
                {
                    ShowDialogue();
                }
                else
                {
                    clickPromptPanel.SetActive(false); //��ȭ
                    storageArrow.SetActive(true); //â��
                    animator.SetTrigger("ToStorage2");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay2")) //StoragePlay2 : â�� ����
            {
                if (dialogueQueue.Count > 0)
                {
                    ShowDialogue();
                }
                else if (dialogueQueue.Count == 0 && clickPromptPanel.activeSelf) //��ȭ ���� ��ȭâ Ȱ��ȭ
                {
                    clickPromptPanel.SetActive(false); //��ȭâ�� ��Ȱ��ȭ
                }
                else if (dialogueQueue.Count == 0 && !clickPromptPanel.activeSelf) //��ȭ ���� ��ȭâ ��Ȱ��ȭ
                {
                    animator.SetTrigger("ToRest2");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rest2")) //Rest2 : �����
            {
                if (dialogueQueue.Count > 0)
                {
                    ShowDialogue();
                }
                else if (dialogueQueue.Count == 0 && clickPromptPanel.activeSelf) //��ȭ ���� ��ȭâ Ȱ��ȭ
                {
                    clickPromptPanel.SetActive(false); //��ȭâ�� ��Ȱ��ȭ
                }
                else if (dialogueQueue.Count == 0 && !clickPromptPanel.activeSelf) //��ȭ ���� ��ȭâ ��Ȱ��ȭ
                {
                    animator.SetTrigger("ToStorage3");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay3")) //StoragePlay3 : â�� �ݱ�
            {
                if (dialogueQueue.Count > 0) //��ȭ ť�� ���� ������ �ִٸ�
                {
                    ShowDialogue(); //���� ��ȭ�� �����ش�
                }
                else
                {
                    clickPromptPanel.SetActive(false); //��ȭ
                    tArrow3.SetActive(true); //â��ݱ�
                    animator.SetTrigger("ToBuilding");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay")) //BuildingPlay : ���� ��ư �����ֱ�
            {
                storeArrow.SetActive(true); //����
                animator.SetTrigger("ToBuilding2");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay2")) //BuildingPlay2 : ���� ��ư���� ���� �Ұ�
            {
                if (dialogueQueue.Count > 0) //��ȭ ť�� ���� ������ �ִٸ�
                {
                    ShowDialogue(); //���� ��ȭ�� �����ش�
                }
                else
                {
                    clickPromptPanel.SetActive(false); //��ȭ
                    //animator.SetTrigger("ToBuilding");
                }
            }
        }
    }
}