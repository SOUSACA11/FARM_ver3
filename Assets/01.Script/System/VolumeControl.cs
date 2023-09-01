using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//by.J:230830 볼륨 컨트롤
public class VolumeControl : MonoBehaviour
{
    public AudioSource audioSource;
    private Slider volumeSlider;

    private void Start()
    {
        volumeSlider = GetComponent<Slider>();
        volumeSlider.value = audioSource.volume; //초기 슬라이더의 값 설정

        //슬라이더 값 변경 이벤트에 리스너 추가
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
