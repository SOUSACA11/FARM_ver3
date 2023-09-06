using UnityEngine;
using System.Collections;

//by.J:230726 ĳ���� ������
//by.J:230901 �ݶ��̴� ������ ���ϱ�
//by.J:230904 ���� �ĸ� �ٸ� �ִϸ����� ������ ����
public class CharaterMovement : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed = 2.0f;
    public Vector2 moveDurationRange = new Vector2(1.0f, 3.0f);
    public Vector2 waitDurationRange = new Vector2(0.5f, 2.0f);

    private float moveDuration;
    private float waitDuration;
    private bool isMoving = false;

    private Vector3 currentMoveDir = Vector3.zero;

    public GameObject frontRightObject; // ù ��° ������Ʈ
    public GameObject backLeftObject; // �� ��° ������Ʈ

    private float animationDelay = 5f;  // �ִϸ��̼� ��� �ð� ���� (0.5��)
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isFront", false); // ���� �� idle ���·�
        PickRandomDirection();
        Vector3 worldPosition = gameObject.transform.position;
        Debug.Log("������ǥ" + worldPosition);

        ResetAnimationParameters();

        // ó�� ������ �� �� ������Ʈ �� �ϳ��� Ȱ��ȭ�մϴ�.
        frontRightObject.SetActive(true);
        backLeftObject.SetActive(false);
    }

    void ResetAnimationParameters()
    {
        animator.SetBool("isFront", false);
        animator.SetBool("isRight", false);
        animator.SetBool("isLeft", false);
        animator.SetBool("isBack", false);
    }

    private void Update()
    {
        if (isMoving)
        {
            moveDuration -= Time.deltaTime;
            transform.position += currentMoveDir * moveSpeed * Time.deltaTime;

            if (moveDuration <= 0)
            {
                isMoving = false;
                animator.SetBool("isFront", false); // �������� ���� �� idle ���·�
                animator.SetBool("isRight", false);
                waitDuration = Random.Range(waitDurationRange.x, waitDurationRange.y);
                ResetAnimationParameters();
            }
        }
        else
        {
            waitDuration -= Time.deltaTime;
            if (waitDuration <= 0)
            {
                PickRandomDirection();
            }
        }
    }

    void PickRandomDirection()
    {

        Vector3 moveDir = Vector3.zero;
        int randomChoice = Random.Range(0, 4);

        switch (randomChoice)
        {
            case 0: // �� (Front)
                ActivateObject(backLeftObject, frontRightObject); //(frontRightObject, backLeftObject);
                moveDir = new Vector3(-1, -1, 0).normalized;
                animator.SetBool("isFront", true);
                break;
            case 1: // ������ (Right)
                ActivateObject(frontRightObject, backLeftObject);
                moveDir = new Vector3(1, -1, 0).normalized;
                animator.SetBool("isRight", true);
                break;
            case 2: // ���� (Left)
                ActivateObject(backLeftObject, frontRightObject);
                moveDir = new Vector3(-1, 1, 0).normalized;
                animator.SetBool("isLeft", true);
                break;
            case 3: // ���� (Back)
                ActivateObject(frontRightObject, backLeftObject);  //(backLeftObject, frontRightObject);
                moveDir = new Vector3(1, 1, 0).normalized;
                animator.SetBool("isBack", true);
                break;
        }

        currentMoveDir = moveDir;
        moveDuration = Random.Range(moveDurationRange.x, moveDurationRange.y);
        isMoving = true;
        StartCoroutine(StartMovementAfterDelay(animationDelay));
    }

    void ActivateObject(GameObject toActivate, GameObject toDeactivate)
    {
        Vector3 previousPosition = toDeactivate.transform.position;

        toDeactivate.SetActive(false);
        // ������Ʈ�� Ȱ��ȭ�ϱ� ���� ��ġ�� ����
        toActivate.transform.position = toDeactivate.transform.position;
        toActivate.SetActive(true);
    }

    IEnumerator StartMovementAfterDelay(float delay)
    {
        // ������ �ð���ŭ ���
        yield return new WaitForSeconds(delay);

        // ������ ����
        isMoving = true;
    }

}