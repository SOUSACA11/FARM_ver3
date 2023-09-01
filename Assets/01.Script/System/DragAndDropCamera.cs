using UnityEngine;

//by.J:230725 드래그 시 화면 및 카메라 이동
public class DragAndDropCamera : MonoBehaviour
{
    private Vector3 touchStart;
    public Camera cam;

    private Vector2 minPosition = new Vector2(-10, -10); //카메라 이동 최소 위치
    private Vector2 maxPosition = new Vector2(20, 20);   //카메라 이동 최대 위치

    public bool dragOk = true; //이동 가능 여부

    void Update()
    {
        if (!dragOk)
        {
            return;
        }
           

        //모바일 버전
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began: //Began = 화면에 터치 시작 상태
                    touchStart = cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, cam.transform.position.z));
                    break;
                case TouchPhase.Moved: //Moved = 손가락 터치가 화면에서 움직인 경우
                    Vector3 direction = touchStart - cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, cam.transform.position.z));
                    Vector3 newPosition = cam.transform.position + direction;

                    //새로운 위치가 제한 범위 내에 있는지 확인
                    newPosition.x = Mathf.Clamp(newPosition.x, minPosition.x, maxPosition.x);
                    newPosition.y = Mathf.Clamp(newPosition.y, minPosition.y, maxPosition.y);

                    //카메라 위치를 새로운 위치로 업데이트
                    cam.transform.position = newPosition;
                    break;
            }
        }

        //PC 버전
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z));
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z));
            Vector3 newPosition = cam.transform.position + direction;

            //새로운 위치가 제한 범위 내에 있는지 확인
            newPosition.x = Mathf.Clamp(newPosition.x, minPosition.x, maxPosition.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minPosition.y, maxPosition.y);

            //카메라 위치를 새로운 위치로 업데이트
            cam.transform.position = newPosition;
        }
    }

    public void OkDrag()
    {
        dragOk = true;
        Debug.Log("움직임");
    }

    public void NoDrag()
    {
        dragOk = false;
        Debug.Log("안움직임");
    }
}

