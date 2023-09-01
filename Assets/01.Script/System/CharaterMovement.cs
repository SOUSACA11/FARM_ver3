using UnityEngine;
using System.Collections;

//by.J:230726 캐릭터 움직임
//by.J:230901 콜라이더 감지시 피하기
public class CharaterMovement : MonoBehaviour
{
    public float speed = 0.5f;     //이동 속도
    public float minWaitTime = 1f; //다음 목적지를 설정하기 전의 최소 대기 시간
    public float maxWaitTime = 3f; //다음 목적지를 설정하기 전의 최대 대기 시간

    private Vector2 bottomLeft; //화면의 왼쪽 하단의 월드 좌표
    private Vector2 topRight;   //화면의 오른쪽 상단의 월드 좌표

    private Vector3 targetPosition; //현재 이동할 목표 위치

    //
    public float avoidDistance = 1.0f;  // 콜라이더를 감지할 거리
    public LayerMask obstacleMask;      // 감지할 장애물 레이어
    //

    private Animator animator; //Animator 컴포넌트

    private void Start()
    {
        //Animator 컴포넌트 가져오기
        animator = GetComponent<Animator>();

        //화면의 왼쪽 하단과 오른쪽 상단의 월드 좌표 구하기
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));

        //Debug.Log("왼쪽 하단: " + bottomLeft);
        //Debug.Log("오른쪽 상단: " + topRight);

        //처음 목적지 설정
        SetNewDestination();
    }

    private void Update()
    {
        //현재 위치에서 목표 위치로 이동
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        //목표 위치에 도착했을 때 새로운 목적지를 설정
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            StartCoroutine(WaitAndSetNewDestination());
        }

        // 장애물 피하기: Ray를 쏘아 앞에 장애물이 있는지 확인
        if (Physics2D.Raycast(transform.position, targetPosition - transform.position, avoidDistance, obstacleMask))
        {
            // 장애물이 감지되면 새로운 목적지 설정
            SetNewDestination();
        }
    }

    private void SetNewDestination()
    {
        //화면 내에서 랜덤한 위치를 목적지로 설정
        float x = Random.Range(bottomLeft.x, topRight.x);
        float y = Random.Range(bottomLeft.y, topRight.y);

        float isoX = (x - y) / 2;
        float isoY = (x + y) / 2;
        //Debug.Log("X: " + isoX);
        //Debug.Log("Y: " + isoY);

        targetPosition = new Vector3(x, y, transform.position.z);
    }

    private IEnumerator WaitAndSetNewDestination()
    {
        //랜덤한 시간만큼 대기한 후 새로운 목적지를 설정
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);
        SetNewDestination();
    }
}
