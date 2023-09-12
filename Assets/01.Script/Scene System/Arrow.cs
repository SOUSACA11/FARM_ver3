using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject storeArrowObject;  // Ȱ��ȭ/��Ȱ��ȭ�� GameObject�� �����մϴ�.

    // �� �Լ��� �ִϸ��̼� �̺�Ʈ���� ȣ��˴ϴ�.
    public void OpenStoreArrow()
    {
        Debug.Log("OpenStoreArrow called!");
        storeArrowObject.SetActive(true);
    }

    public void EndStoreArrow()
    {
        storeArrowObject.SetActive(false);
    }
}
