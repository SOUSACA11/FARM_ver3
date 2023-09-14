using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

//by.J:230904 ��ȭ ��ũ��Ʈ ����
//by.J:230912 �ִϸ����� ��Ʈ�ѷ� ����
public class TutoManager : MonoBehaviour
{
    public TextMeshProUGUI dialogueText; //�ؽ�Ʈ
    public GameObject clickPromptPanel; //�ؽ�Ʈ �г�
    
    private Animator animator;
    private Queue<string> dialogueQueue = new Queue<string>();

    private bool hasShownFarmPlayDialogue = false;  //FarmPlay ��ȭâ
    private bool hasShownFarmPlay4Dialogue = false; //FarmPlay4 ��ȭâ
    private bool hasShownFarmPlay5Dialogue = false; //FarmPlay5 ��ȭâ
    private bool hasShownStoragePlayDialogue = false; //StoragePlay ��ȭâ
    private bool hasShownStoragePlay2Dialogue = false; //StoragePlay2 ��ȭâ
    private bool hasShownStoragePlay3Dialogue = false; //StoragePlay3 ��ȭâ
    private bool hasShownBuildingPlay2Dialogue = false; //BuildingPlay2 ��ȭâ
    private bool hasShownBuildingPlay5Dialogue = false; //BuildingPlay5 ��ȭâ
    private bool hasShownBuildingPlay6Dialogue = false; //BuildingPlay6 ��ȭâ
    private bool hasShownOrderPlayDialogue = false; //OrderPlay ��ȭâ
    private bool hasShownOrderPlay2Dialogue = false; //OrderPlay2 ��ȭâ

    public GameObject storeArrow;   //���� ȭ��ǥ
    public GameObject farmArrow;    //������� ȭ��ǥ
    public GameObject farmArrow2;   //�й� ȭ��ǥ
    public GameObject farmArrow3;   //���� �ݱ� ȭ��ǥ
    public GameObject storageArrow; //â�� ȭ��ǥ
    public GameObject tArrow;       //â���� ȭ��ǥ
    public GameObject tArrow2;      //â����2 ȭ��ǥ
    public GameObject tArrow3;      //â�� �ݱ� ȭ��ǥ
    public GameObject storeArrow2;  //����2 ȭ��ǥ
    public GameObject farmArrow4;   //������ ȭ��ǥ
    public GameObject farmArrow5;   //���� ȭ��ǥ
    public GameObject farmArrow6;   //���� �ݱ�2 ȭ��ǥ
    public GameObject orderArrow;   //�ֹ� ȭ��ǥ

    void Start()
    {
        animator = GetComponent<Animator>();
        clickPromptPanel.SetActive(true);

        //�ʱ� ��ȭ ����
        dialogueQueue.Enqueue("�ȳ�! ���� ���� ��� ��");
        dialogueQueue.Enqueue("���� ����� ���� �̻�����?");
        dialogueQueue.Enqueue("�������� Ǫ���� �ֹε鵵 ���Ҵµ�");
        dialogueQueue.Enqueue("���÷� �ֹε��� �����鼭 �������� �Ǿ���");
        dialogueQueue.Enqueue("...");
        dialogueQueue.Enqueue("�����Ҹ��� ������ �� ����� ����;�...");
        dialogueQueue.Enqueue("�� ȥ�ڼ� �ܿ� �� �ϳ� �ϼ��ߴµ�...");
        dialogueQueue.Enqueue("...?!");
        dialogueQueue.Enqueue("�ʰ� Ǫ�� ������ �ǵ��� �����ְڴٰ�?");
        dialogueQueue.Enqueue("����! ����� ���� �˰��־�!");
        dialogueQueue.Enqueue("���� ���� â�� ��� ���� ��ġ�ϸ��");
        ShowDialogue();
    }

    void Update()
    {
       
        if (!hasShownFarmPlayDialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay"))
        {
            clickPromptPanel.SetActive(true);
            hasShownFarmPlayDialogue = true; //��ȭ�� �� ���� ǥ���ϵ��� �÷��� ����
            dialogueQueue.Enqueue("�׸��� ����� ���� �����ؼ� ���� �ɾ��");
            ShowDialogue();
        }

        if (!hasShownFarmPlay4Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay4"))
        {
            clickPromptPanel.SetActive(true);
            hasShownFarmPlay4Dialogue = true; //��ȭ�� �� ���� ǥ���ϵ��� �÷��� ����
            dialogueQueue.Enqueue("��ġ�� ���� �ð��� ������ ������");
            dialogueQueue.Enqueue("���� â�� �ݰ� ��ٷ�����");
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
            tArrow.SetActive(true); //â����1
            tArrow2.SetActive(true); //â����2
            clickPromptPanel.SetActive(true);
            hasShownStoragePlay2Dialogue = true; //��ȭ�� �� ���� ǥ���ϵ��� �÷��� ����
            dialogueQueue.Enqueue("���忡�� ��Ȯ�ϴ� �۹��� �۹�â���� �� �� �ְ�");
            dialogueQueue.Enqueue("�����ؼ� ��� ����ǰ�� ����ǰâ���� �� �� �־�");
            ShowDialogue();
        }

        if (!hasShownStoragePlay3Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("StoragePlay3"))
        {
            StartCoroutine(ShownStoragePlay3DialogueAfterDelay(0.3f)); //0.3�� �ڿ� ��ȭ ǥ��
        }

        if (!hasShownBuildingPlay2Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay2"))
        {
            storeArrow2.SetActive(false); //����2
            clickPromptPanel.SetActive(true);
            hasShownBuildingPlay2Dialogue = true; //��ȭ�� �� ���� ǥ���ϵ��� �÷��� ����
            dialogueQueue.Enqueue("������ ��ġ�غ���");
            ShowDialogue();
        }

        if (!hasShownBuildingPlay5Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay5"))
        {
            clickPromptPanel.SetActive(true);
            hasShownBuildingPlay5Dialogue = true; //��ȭ�� �� ���� ǥ���ϵ��� �÷��� ����
            dialogueQueue.Enqueue("������ Ŭ���ϸ� ���� �� �ִ� ����ǰ ����� ������");
            dialogueQueue.Enqueue("����ǰ�� Ŭ���ϸ� �ʿ��� ��Ḧ �� �� �־�");
            dialogueQueue.Enqueue("�Ļ��� Ŭ���ؼ� �������� ���� ���Ƽ� ����� ����");
            ShowDialogue();
        }

        if (!hasShownBuildingPlay6Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay6"))
        {
            clickPromptPanel.SetActive(true);
            hasShownBuildingPlay6Dialogue = true; //��ȭ�� �� ���� ǥ���ϵ��� �÷��� ����
            dialogueQueue.Enqueue("������ �������̸� �����̰� �ְ�");
            dialogueQueue.Enqueue("������ �Ϸ�Ǹ� �����̸鼭 �������� ����");
            dialogueQueue.Enqueue("�׸��� ����ǰ�� �ڵ����� â�� ����");
            ShowDialogue();
        }

        if(!hasShownOrderPlayDialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("OrderPlay"))
        {
            StartCoroutine(ShownOrderPlayDialogueAfterDelay(0.5f)); //0.5�� �ڿ� ��ȭ ǥ��
        }

        if (!hasShownOrderPlay2Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("OrderPlay2"))
        {
            StartCoroutine(ShownOrderPlay2DialogueAfterDelay(0.5f)); //0.5�� �ڿ� ��ȭ ǥ��
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

    IEnumerator ShownOrderPlayDialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); //������ �ð�(��) ���� ���

        if (!hasShownOrderPlayDialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("OrderPlay"))
        {
            clickPromptPanel.SetActive(true);
            hasShownOrderPlayDialogue = true; //��ȭ�� �� ���� ǥ���ϵ��� �÷��� ����
            dialogueQueue.Enqueue("ȹ���� ����ǰ���� ��ǰ�� �ؼ� ���� �� �� �־�");
            ShowDialogue();
        }
    }
    IEnumerator ShownOrderPlay2DialogueAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay); //������ �ð�(��) ���� ���

        if (!hasShownOrderPlay2Dialogue && animator.GetCurrentAnimatorStateInfo(0).IsName("OrderPlay2"))
        {
            clickPromptPanel.SetActive(true);
            hasShownOrderPlay2Dialogue = true; //��ȭ�� �� ���� ǥ���ϵ��� �÷��� ����
            dialogueQueue.Enqueue("������ �۹��� �ɰ� ��Ȯ�ؼ�");
            dialogueQueue.Enqueue("�پ��� ����ǰ�� ����");
            dialogueQueue.Enqueue("������ ������");
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

            if (animator.GetCurrentAnimatorStateInfo(0).IsName("ClickPrompt"))
            {
                clickPromptPanel.SetActive(false); //��ȭâ
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
                farmArrow3.SetActive(false); //���� �ݱ�

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
                    
                    animator.SetTrigger("ToRest3");
                    storageArrow.SetActive(true); //â��
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rest3")) //Rest3 : â��ݱ� / �����
            {
                storageArrow.SetActive(false); //â��
                animator.SetTrigger("ToStorage2");
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
                else if (dialogueQueue.Count == 0 && clickPromptPanel.activeSelf) //��ȭ ���� ��ȭâ Ȱ��ȭ
                {
                    clickPromptPanel.SetActive(false); //��ȭâ�� ��Ȱ��ȭ
                    tArrow3.SetActive(true); //â��ݱ�
                }
                else if (dialogueQueue.Count == 0 && !clickPromptPanel.activeSelf) //��ȭ ���� ��ȭâ ��Ȱ��ȭ
                {
                    storeArrow2.SetActive(true); //����2
                    animator.SetTrigger("ToBuilding");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay")) //BuildingPlay : ���� ��ư �����ֱ�
            {
                animator.SetTrigger("ToBuilding2");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay2")) //BuildingPlay2 : �������� ������ ����Ű��
            {
                if (dialogueQueue.Count > 0) //��ȭ ť�� ���� ������ �ִٸ�
                {
                    ShowDialogue(); //���� ��ȭ�� �����ش�
                }
                else
                {
                    clickPromptPanel.SetActive(false); //��ȭ
                    farmArrow4.SetActive(true); //������
                    animator.SetTrigger("ToBuilding3");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay3")) //BuildingPlay3 : ���� ����Ű��
            {
                farmArrow4.SetActive(false); //������
                farmArrow5.SetActive(true); //����
                animator.SetTrigger("ToBuilding4");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay4")) //BuildingPlay4 : ���� �ݱ�2
            {
                farmArrow5.SetActive(false); //����
                farmArrow6.SetActive(true); //���� �ݱ�2
                animator.SetTrigger("ToRest4");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rest4")) //Rest4 : �����ݱ� / �����
            {
                farmArrow6.SetActive(false); //���� �ݱ�2
                animator.SetTrigger("ToBuilding5");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay5")) //BuildingPlay5 : ��ȭâ - ���� ���� ����
            {
                if (dialogueQueue.Count > 0) //��ȭ ť�� ���� ������ �ִٸ�
                {
                    ShowDialogue(); //���� ��ȭ�� �����ش�
                }
                else
                {
                    clickPromptPanel.SetActive(false); //��ȭ
                    animator.SetTrigger("ToRest5");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rest5")) //Rest5 : �����
            {
                animator.SetTrigger("ToRest6");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rest6")) //Rest6 : �����
            {
                animator.SetTrigger("ToRest7");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rest7")) //Rest7 : �����
            {
                animator.SetTrigger("ToBuilding6");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("BuildingPlay6")) //BuildingPlay6 : ��ȭâ - ���� �ִϸ��̼� ����
            {
                if (dialogueQueue.Count > 0) //��ȭ ť�� ���� ������ �ִٸ�
                {
                    ShowDialogue(); //���� ��ȭ�� �����ش�
                }
                else
                {
                    clickPromptPanel.SetActive(false); //��ȭ
                    animator.SetTrigger("ToOrder");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("OrderPlay")) //OrderPlay : ��ȭâ - �� ���� ���
            {
                if (dialogueQueue.Count > 0) //��ȭ ť�� ���� ������ �ִٸ�
                {
                    ShowDialogue(); //���� ��ȭ�� �����ش�
                }
                else
                {
                    clickPromptPanel.SetActive(false); //��ȭ
                    orderArrow.SetActive(true); //�ֹ�
                    animator.SetTrigger("ToRest8");
                }
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rest8")) //Rest8 : �����
            {
                orderArrow.SetActive(false); //�ֹ�
                animator.SetTrigger("ToOrder2");
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("OrderPlay2")) //OrderPlay2 : ��ȭâ - ������
            {
                
                if (dialogueQueue.Count > 0) //��ȭ ť�� ���� ������ �ִٸ�
                {
                    ShowDialogue(); //���� ��ȭ�� �����ش�
                }
                else
                {
                    clickPromptPanel.SetActive(false); //��ȭ
                    animator.SetTrigger("ToRest9");
                }
            }


            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("Rest9")) //Rest9 : �����
            {
                SceneManager.LoadScene("Main");
            }
        }
    }
}