using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//by.J:230830 메뉴 패널 활성화
public class MenuUIManager : MonoBehaviour
{
    public GameObject menuPanel; //메뉴 패널

    //열기
    public void OpenPanel()
    {
        if (menuPanel != null)
        {
            menuPanel.SetActive(!menuPanel.activeSelf); //패널의 현재 활성화 상태를 반대로 설정
        }
    }

    //닫기
    public void ClosePanel()
    {
        if (menuPanel != null)
        {
            menuPanel.SetActive(!menuPanel.activeSelf); //패널의 현재 활성화 상태를 반대로 설정
        }
    }
}
