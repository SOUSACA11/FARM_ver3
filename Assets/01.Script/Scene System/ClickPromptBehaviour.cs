using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by.J:230912 ClickPrompt 상태
public class ClickPromptBehaviour : StateMachineBehaviour
{ 
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) //상태 진입
    {
        var tutoManager = animator.GetComponent<TutoManager>();
        tutoManager.clickPromptPanel.SetActive(true); //텍스트패널 활성화
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex) //상태 탈출
    {
        var tutoManager = animator.GetComponent<TutoManager>();
        tutoManager.clickPromptPanel.SetActive(false); //텍스트패널 비활성화
    }
}
