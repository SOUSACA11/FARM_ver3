using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//by.J:230726 캐릭터 움직임
//by.J:230901 콜라이더 감지시 피하기
//by.J:230904 정면 후면 다른 애니메이터 움직임 적용
public class CharaterMovement : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();
    Rigidbody2D rigidbody2D;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UPdateState();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        rigidbody2D.velocity = movement * movementSpeed;
    }

    void UPdateState()
    {
        if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
        {
            animator.SetBool("isWalk", false);
        }
        else
        {
            animator.SetBool("isWalk", true);
        }
        animator.SetFloat("DirectionX", movement.x);
        animator.SetFloat("DirectionY", movement.y);
    }
}
//    public float speed = 0.5f;     //이동 속도
//    public float minWaitTime = 1f; //다음 목적지를 설정하기 전의 최소 대기 시간
//    public float maxWaitTime = 3f; //다음 목적지를 설정하기 전의 최대 대기 시간

//    private Vector2 bottomLeft; //화면의 왼쪽 하단의 월드 좌표
//    private Vector2 topRight;   //화면의 오른쪽 상단의 월드 좌표

//    private Vector3 targetPosition; //현재 이동할 목표 위치

//    public float avoidDistance = 1.0f;  // 콜라이더를 감지할 거리
//    public LayerMask obstacleMask;      // 감지할 장애물 레이어
 
//    private Animator animator; //Animator 컴포넌트

//    private void Start()
//    {
//        //Animator 컴포넌트 가져오기
//        animator = GetComponent<Animator>();

//        //화면의 왼쪽 하단과 오른쪽 상단의 월드 좌표 구하기
//        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
//        topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));

//        //Debug.Log("왼쪽 하단: " + bottomLeft);
//        //Debug.Log("오른쪽 상단: " + topRight);

//        //처음 목적지 설정
//        SetNewDestination();
//    }

//    private void Update()
//    {
//        //현재 위치에서 목표 위치로 이동
//        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

//        Vector2 movementDirection = (targetPosition - transform.position).normalized;
//        SetAnimatorDirection(movementDirection);


//        //목표 위치에 도착했을 때 새로운 목적지를 설정
//        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
//        {
//            StartCoroutine(WaitAndSetNewDestination());
//        }

//        //장애물 피하기: Ray를 쏘아 앞에 장애물이 있는지 확인
//        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPosition - transform.position, avoidDistance, obstacleMask);
//        if (hit.collider != null && hit.distance < 0.5f) // 장애물이 특정 거리 이내에 있을 경우
//        {
//            SetNewDestination();
//        }
//    }

//    private void SetNewDestination()
//    {
//        Vector3 oldTargetPosition = targetPosition;

//        float x;
//        float y;

//        if (Random.value < 0.5f)
//        {
//            x = transform.position.x - Random.Range(0f, Mathf.Abs(topRight.x - transform.position.x));
//            y = transform.position.y;
//        }
//        else
//        {
//            x = transform.position.x;
//            y = transform.position.y + Random.Range(0f, Mathf.Abs(topRight.y - transform.position.y));
//        }

//        targetPosition = new Vector3(x, y, transform.position.z);

//        StartCoroutine(MoveToTargetPosition());
//    }

  

//    private void SetAnimatorDirection(Vector2 movementDirection)
//    {
//        animator.SetFloat("DirectionX", movementDirection.x);
//        animator.SetFloat("DirectionY", movementDirection.y);
//    }
    
//    private IEnumerator MoveToTargetPosition()
//    {
//        // 목적지까지 이동하기 전에 애니메이터 상태를 설정
//        Vector2 movementDirection = (targetPosition - transform.position).normalized;
//        SetAnimatorDirection(movementDirection);

//        yield return new WaitForSeconds(0.1f); // 필요한 경우 대기 시간 조절

//        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
//        {
//            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
//            yield return null;
//        }
//    }

//    private IEnumerator WaitAndSetNewDestination()
//    {
//        //랜덤한 시간만큼 대기한 후 새로운 목적지를 설정
//        float waitTime = Random.Range(minWaitTime, maxWaitTime);
//        yield return new WaitForSeconds(waitTime);
//        SetNewDestination();
//    }
//}
