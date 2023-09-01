using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//by.J:230823 씬 전환
public class SceneUIManager : MonoBehaviour
{
    public void ClickStart()
    {
        Debug.Log("게임 시작");
        SceneManager.LoadScene("Main");
    }

    public void ClickExit()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
