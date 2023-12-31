using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by.J:230830 엔딩 이미지 회전
public class EpilogueUIManager : MonoBehaviour
{
    public Vector3 rotationAmount = new Vector3(0, 0, 30); //30도 회전
    private float rotationDuration = 3.0f;                 //3초 동안 회전
    private float pauseDuration = 0.1f;                    //회전 후 0.1초 동안 일시정지

    private Quaternion startRotation;
    private Quaternion endRotation;
    private float timeElapsed;

    //이미지 상태 타입
    private enum State
    {
        Rotating, //회전
        Pausing,  //일시정지
        Returning //원래 위치 돌아가기
    }

    private State currentState;

    private void Start()
    {
        startRotation = transform.rotation;
        endRotation = Quaternion.Euler(rotationAmount) * startRotation;
        currentState = State.Rotating;
    }

    private void Update()
    {
        timeElapsed += Time.deltaTime;
        
        //현재 상태에 따른 작업 수행
        switch (currentState)
        {
            case State.Rotating:
                RotateTowards(endRotation);
                if (Quaternion.Angle(transform.rotation, endRotation) < 0.01f)
                {
                    currentState = State.Pausing;
                    timeElapsed = 0;
                }
                break;
            case State.Pausing:
                if (timeElapsed > pauseDuration)
                {
                    currentState = State.Returning;
                    timeElapsed = 0;
                }
                break;
            case State.Returning:
                RotateTowards(startRotation);
                if (Quaternion.Angle(transform.rotation, startRotation) < 0.01f)
                {
                    currentState = State.Rotating;
                    timeElapsed = 0;
                }
                break;
        }
    }

    //주어진 목표 회전 각도로 이미지 회전
    private void RotateTowards(Quaternion targetRotation)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, timeElapsed / rotationDuration);
    }
}
