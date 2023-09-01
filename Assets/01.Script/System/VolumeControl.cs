using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//by.J:230830 ���� ��Ʈ��
public class VolumeControl : MonoBehaviour
{
    public AudioSource audioSource;
    private Slider volumeSlider;

    private void Start()
    {
        volumeSlider = GetComponent<Slider>();
        volumeSlider.value = audioSource.volume; //�ʱ� �����̴��� �� ����

        //�����̴� �� ���� �̺�Ʈ�� ������ �߰�
        volumeSlider.onValueChanged.AddListener(ChangeVolume);
    }

    public void ChangeVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
