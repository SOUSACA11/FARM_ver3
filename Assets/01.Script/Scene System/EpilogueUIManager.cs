using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//by.J:230830 ���� �̹��� ȸ��
public class EpilogueUIManager : MonoBehaviour
{
    public Vector3 rotationAmount = new Vector3(0, 0, 30); // 180�� ȸ��
    private float rotationDuration = 3.0f; // 2�� ���� ȸ��
    private float pauseDuration = 0.1f; // ȸ�� �� 1�� ���� �Ͻ�����

    private Quaternion startRotation;
    private Quaternion endRotation;
    private float timeElapsed;

    private enum State
    {
        Rotating,
        Pausing,
        Returning
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

    private void RotateTowards(Quaternion targetRotation)
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, timeElapsed / rotationDuration);
    }
}
