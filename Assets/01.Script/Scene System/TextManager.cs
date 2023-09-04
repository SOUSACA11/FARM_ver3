using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//by.J:230904 ��ȭ ��ũ��Ʈ ����
public class TextManager : MonoBehaviour
{
    public GameObject dialoguePanel; // ��ȭ �г�
    //public TextMeshProUGUI nameText; //NPC �̸�
    public TextMeshProUGUI dialogueText; // ��縦 ǥ���� Text ������Ʈ

    public DragAndDropCamera dragAndDropCamera;
    private Queue<string> sentences;

    private void Start()
    {
        sentences = new Queue<string>();
        
        // Ʃ�丮�� ���� �� �ڵ����� ��ȭ ����
        StartTutorialDialogue();
        dragAndDropCamera.NoDrag();
    }

    private void Update()
    {
        // ��ȭ �г��� Ȱ��ȭ �����̰� ����ڰ� Ŭ������ ��
        if (dialoguePanel.activeSelf && Input.GetMouseButtonDown(0))
        {
            DisplayNextSentence();
        }
    }

    public void StartTutorialDialogue()
    {
       
        TextLog tutorialDialogue = new TextLog
        {
            //npcName = "Ʃ�丮�� ���̵�",
            sentences = new string[] { "�ȳ�, ���� ���� ������ �ñ���", "����������" }
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

        dialoguePanel.SetActive(true); // �г� Ȱ��ȭ
        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        //������ ���̻� ������
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        // ť���� ���� ������ ������ ǥ��
        string sentence = sentences.Dequeue();
        dialogueText.text = sentence;
    }

    void EndDialogue()
    {
        dialoguePanel.SetActive(false); // �г� ��Ȱ��ȭ
        dragAndDropCamera.OkDrag();
    }


}
