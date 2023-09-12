using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

//by.J:230904 ��ȭ ��ũ��Ʈ ����
public class TutoManager : MonoBehaviour
{
    public TutorialState currentState = TutorialState.None;
    public TextMeshProUGUI dialogueText; //�ؽ�Ʈ
    public GameObject clickPromptPanel; //�ؽ�Ʈ �г�
    
    private Animator animator;
    private Queue<string> dialogueQueue = new Queue<string>();

    private bool hasShownFarmPlayDialogue = false; //FarmPlay ��ȭâ
    private bool hasShownFarmPlay4Dialogue = false; //FarmPlay4 ��ȭâ

    public GameObject storePanel; //����� �г�
    public GameObject farmArrow; //����� ȭ��ǥ
    public GameObject farmArrow2; //�й� ȭ��ǥ

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
            dialogueQueue.Enqueue("�׷� ������� �����ؼ� ���� �ɾ��");
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
            animator.SetTrigger("ToClick"); // ��� ��ȭ�� ������ ClickPrompt ���·� ��ȯ
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
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay")) //FarmPlay : ��ȭâ ���� - �� ��ġ ����
            {
                clickPromptPanel.SetActive(false);
                animator.SetTrigger("ToFarm2");
                farmArrow.SetActive(true); //�����

            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay2")) //FarmPlay2 : ��ȭâ �������� ����� ����Ű��
            {
                animator.SetTrigger("ToFarm3");
                farmArrow.SetActive(false); //�����
                farmArrow2.SetActive(true); //�й�
            }
            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay3")) //FarmPlay3 : �й� ����Ű��
            {
                animator.SetTrigger("ToFarm4");
                farmArrow2.SetActive(false); //�й�
            }

            else if (animator.GetCurrentAnimatorStateInfo(0).IsName("FarmPlay4")) //FarmPlay4 : ��ȭâ ���� - �� ���� ����
            {
                clickPromptPanel.SetActive(false);
                animator.SetTrigger("ToFarm5");
            }
        }
    }
}