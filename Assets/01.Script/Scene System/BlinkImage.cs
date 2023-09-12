using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlinkImage : MonoBehaviour
{
    private Image image;
    public float blinkDuration = 0.3f; //깜박이는 지속시간 설정

    void Start()
    {
        image = GetComponent<Image>();
        StartCoroutine(Blink());
    }

    IEnumerator Blink()
    {
        while (true) //무한 반복
        {
            image.enabled = !image.enabled; //이미지의 활성화 상태를 토글
            yield return new WaitForSeconds(blinkDuration); //설정한 지속시간만큼 대기
        }
    }
}
