using UnityEngine;

//by.J:230725 터치 관련 시스템
public class TouchManager : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            //모든 터치 이벤트 가져오기
            Touch[] touches = Input.touches;

            //터치된 모든 위치 레이캐스트 검사
            foreach (Touch touch in touches)
            {
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position); //터치된 화면 좌표 월드 좌표로 변환
                touchPosition.z = 0f;                                                  //화면 좌표 z값 0 고정

                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                //레이캐스터 충돌 오브젝트
                if (hit.collider != null)
                {
                    // 여기에 오브젝트를 클릭한 경우의 동작을 작성합니다.
                    // hit.collider.gameObject 는 터치한 오브젝트를 나타냅니다.
                    // 예를 들어, hit.collider.gameObject.GetComponent<Building>() 를 사용하여 건물 정보를 가져올 수 있습니다.
                }
            }
        }
    }
}
