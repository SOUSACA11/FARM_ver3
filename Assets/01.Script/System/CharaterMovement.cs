using UnityEngine;
using System.Collections;

//by.J:230726 캐릭터 움직임
//by.J:230904 정면 후면 다른 애니메이터 움직임 적용
//by.J:230914 방향에 따른 애니메이션 클리 적용 성공
public class CharaterMovement : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed = 2.0f;
    public Vector2 moveDurationRange = new Vector2(1.0f, 3.0f);
    public Vector2 waitDurationRange = new Vector2(0.5f, 2.0f);

    public float moveDuration;
    private float waitDuration;
    private bool isMoving = false;

    private Vector3 currentMoveDir = Vector3.zero;

    public GameObject frontRightObject; // 첫 번째 오브젝트
    public GameObject backLeftObject; // 두 번째 오브젝트

    private float animationDelay = 5f;  // 애니메이션 대기 시간 설정 (0.5초)
    private void Start()
    {
        animator = GetComponent<Animator>();
        PickRandomDirection();
        Vector3 worldPosition = gameObject.transform.position;
        Debug.Log("월드좌표" + worldPosition);

        //처음 시작할 때 두 오브젝트 중 하나만 활성화
        frontRightObject.gameObject.SetActive(true);
        backLeftObject.gameObject.SetActive(false);
    }

 
    private void Update()
    {
        GameObject activeObject = frontRightObject.activeSelf ? frontRightObject : backLeftObject;

        if (isMoving)
        {
            moveDuration -= Time.deltaTime;
            activeObject.transform.position += currentMoveDir * moveSpeed * Time.deltaTime;  // 변경된 부분

            if (moveDuration <= 0)
            {
                moveDuration = 0;
                isMoving = false;
                waitDuration = Random.Range(waitDurationRange.x, waitDurationRange.y);
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
        StopCoroutine(StartMovementAfterDelay(animationDelay));

        Vector3 moveDir = Vector3.zero;
        int randomChoice = Random.Range(0, 4);

        switch (randomChoice)
        {
            case 0: // 앞 (Front)
                ActivateObject(frontRightObject, backLeftObject); 
                moveDir = new Vector3(-1, -1, 0).normalized;
                frontRightObject.GetComponent<Animator>().SetTrigger("isFront");
                break;
            case 1: // 오른쪽 (Right)
                ActivateObject(frontRightObject, backLeftObject);
                moveDir = new Vector3(1, -1, 0).normalized;
                frontRightObject.GetComponent<Animator>().SetTrigger("isRight");
                break;
            case 2: // 왼쪽 (Left)
                ActivateObject(backLeftObject, frontRightObject);
                moveDir = new Vector3(-1, 1, 0).normalized;
                backLeftObject.GetComponent<Animator>().SetTrigger("isLeft");
                break;
            case 3: // 뒤쪽 (Back)
                ActivateObject(backLeftObject, frontRightObject);
                moveDir = new Vector3(1, 1, 0).normalized;
                backLeftObject.GetComponent<Animator>().SetTrigger("isBack");
                break;
        }

        currentMoveDir = moveDir;
        moveDuration = Random.Range(moveDurationRange.x, moveDurationRange.y);
        isMoving = true;

        // 오브젝트 활성화 확인 및 활성화
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
        }

        StartCoroutine(StartMovementAfterDelay(animationDelay));
    }

    void ActivateObject(GameObject toActivate, GameObject toDeactivate)
    {
        Debug.Log("흐쿠쿠ㅜ");

        if (toDeactivate.activeSelf)
        {
            toDeactivate.SetActive(false);
        }

        if (!toActivate.activeSelf)
        {
            toActivate.transform.position = toDeactivate.transform.position;
            toActivate.SetActive(true);
        }

    }

    IEnumerator StartMovementAfterDelay(float delay)
    {
        // 지정된 시간만큼 대기
        yield return new WaitForSeconds(delay);

        // 움직임 시작
        isMoving = true;
    }

}