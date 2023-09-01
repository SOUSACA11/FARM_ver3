using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//by.J:230809 주문 창 클릭시 활성화 / 메뉴 버튼 비활성화 / 닫기 버튼
public class OrderManagerUI : MonoBehaviour
{
    public Image image;         //움직일 이미지
    public Vector3 endPosition; //마지막 이동 위치
    public float speed;         //이동 속도

    public Button closeButton;      //닫기 버튼
    public Button inviButton1;      //비활성화 할 버튼 1번
    public Button inviButton2;      //비활성화 할 버튼 2번
    public Button inviButton3;      //비활성화 할 버튼 3번

    private Vector3 startPosition; //시작 위치

    private void Start()
    {
        //Debug.Log("x값 :" + image.rectTransform.position.x); //955
        //Debug.Log("y값 :"+ image.rectTransform.position.y);  //-3030

        closeButton.onClick.AddListener(CloseButtonOnClick);    //닫기 버튼 클릭
        startPosition = image.transform.position;               //시작 위치 설정
    }

    public void CloseButtonOnClick()
    {
        //메뉴 버튼 비활성화, 닫기 버튼 활성화
        image.transform.position = startPosition;
        inviButton1.gameObject.SetActive(true);
        inviButton2.gameObject.SetActive(true);
        inviButton3.gameObject.SetActive(true);
    }

    public void OrderButton_Click()
    {
        //상점 창 기능 활성화
        StartCoroutine(MoveImage());

        //메뉴 버튼 비활성화
        inviButton1.gameObject.SetActive(false);
        inviButton2.gameObject.SetActive(false);
        inviButton3.gameObject.SetActive(false);
    }

    IEnumerator MoveImage()
    {

        //처음 y값    : -3030
        //마지막 y값  : -680

        float t = 0f; // 시간 변수

        Vector3 startPosition = image.transform.position;  // 시작 위치 저장

        endPosition = new Vector3(977+400, image.rectTransform.position.y + 3550, 0); //마지막 위치 저장

        while (t < 1f) // t가 1이 될 때까지
        {
            if (image.rectTransform.position.y >= 480) //y갑시 480 이상이면 멈춤
            {
                yield break;
            }

            t += Time.deltaTime * speed; // 시간 누적

            // Lerp를 이용해 현재 위치에서 endPosition까지 부드럽게 이동
            image.transform.position = Vector3.Lerp(startPosition, endPosition, t);

            yield return null; // 프레임 간격대로 실행

        }
    }
}
