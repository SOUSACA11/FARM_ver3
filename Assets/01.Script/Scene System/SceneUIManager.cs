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
        SceneManager.LoadScene("Main");
    }

    public void ClickExit()
    {
        Debug.Log("���� ����");
        Application.Quit();
    }
}
