using UnityEngine;
using UnityEngine.UI;

//by.J:230829 ��ư �����̴� ������
public class ButtonBlink : MonoBehaviour
{
    public float blinkSpeed = 3.0f;  //�����̴� �ӵ�
    public Image btnImage;          //��ư�� �̹��� ������Ʈ
    private Color originalColor;     //���� ��ư ����
    private bool isBlinking = true;

    private void Start()
    {
        //btnImage = GetComponent<Image>();
        originalColor = btnImage.color;
    }

    private void Update()
    {
        if (isBlinking)
        {
            float alphaValue = (Mathf.Sin(Time.time * blinkSpeed) + 1) / 2;
            btnImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alphaValue);
        }
    }

    //public void StartBlinking()
    //{
    //    isBlinking = true;
    //}

    //public void StopBlinking()
    //{
    //    isBlinking = false;
    //    btnImage.color = originalColor;  //���� �������� �ǵ���
    //}

}
