using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//by.J:230823 �� ��ȯ
public class SceneUIManager : MonoBehaviour
{
    public void ClickStart()
    {
        Debug.Log("���� ����");
        SceneManager.LoadScene("Tutorial");
    }

    public void ClickExit()
    {
        Debug.Log("���� ����");
        Application.Quit();
    }
}
