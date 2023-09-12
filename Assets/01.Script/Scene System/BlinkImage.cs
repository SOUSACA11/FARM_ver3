using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkImage : MonoBehaviour
{
    private Image image;
    public float blinkDuration = 0.3f; //�����̴� ���ӽð� ����

    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true) //���� �ݺ�
        {
            image.enabled = !image.enabled; //�̹����� Ȱ��ȭ ���¸� ���
            yield return new WaitForSeconds(blinkDuration); //������ ���ӽð���ŭ ���
        }
    }
}
