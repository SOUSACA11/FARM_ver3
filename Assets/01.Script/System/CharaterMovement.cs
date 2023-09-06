using UnityEngine;
using System.Collections;

//by.J:230726 캐릭터 움직임
//by.J:230901 콜라이더 감지시 피하기
//by.J:230904 정면 후면 다른 애니메이터 움직임 적용
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

    public GameObject frontRightObject; // 첫 번째 오브젝트
    public GameObject backLeftObject; // 두 번째 오브젝트

    private float animationDelay = 5f;  // 애니메이션 대기 시간 설정 (0.5초)
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isFront", false); // 시작 시 idle 상태로
        PickRandomDirection();
        Vector3 worldPosition = gameObject.transform.position;
        Debug.Log("월드좌표" + worldPosition);

        ResetAnimationParameters();

        // 처음 시작할 때 두 오브젝트 중 하나만 활성화합니다.
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
                animator.SetBool("isFront", false); // 움직임이 멈출 때 idle 상태로
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
            case 0: // 앞 (Front)
                ActivateObject(backLeftObject, frontRightObject); //(frontRightObject, backLeftObject);
                moveDir = new Vector3(-1, -1, 0).normalized;
                animator.SetBool("isFront", true);
                break;
            case 1: // 오른쪽 (Right)
                ActivateObject(frontRightObject, backLeftObject);
                moveDir = new Vector3(1, -1, 0).normalized;
                animator.SetBool("isRight", true);
                break;
            case 2: // 왼쪽 (Left)
                ActivateObject(backLeftObject, frontRightObject);
                moveDir = new Vector3(-1, 1, 0).normalized;
                animator.SetBool("isLeft", true);
                break;
            case 3: // 뒤쪽 (Back)
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
        // 오브젝트를 활성화하기 전에 위치를 변경
        toActivate.transform.position = toDeactivate.transform.position;
        toActivate.SetActive(true);
    }

    IEnumerator StartMovementAfterDelay(float delay)
    {
        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(delay);

        // 움직임 시작
        isMoving = true;
    }

}