using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by.J:230912 ClickPrompt ����
public class ClickPromptBehaviour : StateMachineBehaviour
{ 
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) //���� ����
    {
        var tutoManager = animator.GetComponent<TutoManager>();
        tutoManager.clickPromptPanel.SetActive(true); //�ؽ�Ʈ�г� Ȱ��ȭ
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) //���� Ż��
    {
        var tutoManager = animator.GetComponent<TutoManager>();
        tutoManager.clickPromptPanel.SetActive(false); //�ؽ�Ʈ�г� ��Ȱ��ȭ
    }
}
