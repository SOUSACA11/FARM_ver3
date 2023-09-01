using UnityEngine;
using UnityEngine.UI;

//by.J:230829 버튼 깜박이는 움직임
public class ButtonBlink : MonoBehaviour
{
    public float blinkSpeed = 3.0f;  //깜박이는 속도
    public Image btnImage;          //버튼의 이미지 컴포넌트
    private Color originalColor;     //원래 버튼 색상
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
    //    btnImage.color = originalColor;  //원래 색상으로 되돌림
    //}

}
