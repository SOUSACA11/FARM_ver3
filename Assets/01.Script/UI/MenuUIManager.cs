using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//by.J:230830 �޴� �г� Ȱ��ȭ
public class MenuUIManager : MonoBehaviour
{
    public GameObject menuPanel; //�޴� �г�

    //����
    public void OpenPanel()
    {
        if (menuPanel != null)
        {
            menuPanel.SetActive(!menuPanel.activeSelf); //�г��� ���� Ȱ��ȭ ���¸� �ݴ�� ����
        }
    }

    //�ݱ�
    public void ClosePanel()
    {
        if (menuPanel != null)
        {
            menuPanel.SetActive(!menuPanel.activeSelf); //�г��� ���� Ȱ��ȭ ���¸� �ݴ�� ����
        }
    }
}
