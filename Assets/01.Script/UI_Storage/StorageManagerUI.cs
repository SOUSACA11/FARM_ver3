using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

//by.J:230808 창고 창 클릭시 활성화 / 메뉴 버튼 비활성화 / 닫기 버튼 
public class StorageManagerUI : MonoBehaviour
{
    public Image image;         //움직일 이미지
    public Vector3 endPosition; //마지막 이동 위치
    public float speed;         //이동 속도

    public Button closeButton;  //닫기 버튼

    public Button inviButton1;      //비활성화 할 버튼 1번
    public Button inviButton2;      //비활성화 할 버튼 2번
    public Button inviButton3;      //비활성화 할 버튼 3번

    private Vector3 startPosition; //시작 위치
    public StorageSlotUI storageSlotUI;

    private TabType currentTab = TabType.CropItem; //처음 빌딩 탭 보여주기

    public enum TabType
    {
        CropItem,
        ProcessItem
    }

    public void TabCropItem()
    {
        Debug.Log("탭전환 작물");
        currentTab = TabType.CropItem;
        storageSlotUI.SetCurrentTab(currentTab);
        Dictionary<IItem, int> cropItems = Storage.Instance.GetCropItems();
        DisplayItems(cropItems);
    }

    public void TabProcessItem()
    {
        Debug.Log("탭전환 생산품");
        currentTab = TabType.ProcessItem;
        storageSlotUI.SetCurrentTab(currentTab);
        Dictionary<IItem, int> processItems = Storage.Instance.GetProcessItems();
        DisplayItems(processItems);
    }

    private void Start()
    {
        //Debug.Log("x값 :" + image.rectTransform.position.x); //977
        //Debug.Log("y값 :"+ image.rectTransform.position.y);  //-1824

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

    public void StorageButton_Click()
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

        //처음 y값    : -1824
        //마지막 y값  : 526

        float t = 0f; //시간 변수

        Vector3 startPosition = image.transform.position;  //시작 위치 저장

        endPosition = new Vector3(977 + 400, image.rectTransform.position.y + 2350, 0); //마지막 위치 저장

        while (t < 1f) //t가 1이 될 때까지
        {
            if (image.rectTransform.position.y >= 480) //y값 480 이상이면 멈춤
            {
                yield break;
            }

            t += Time.deltaTime * speed; //시간 누적

            //Lerp를 이용해 현재 위치에서 endPosition까지 부드럽게 이동
            image.transform.position = Vector3.Lerp(startPosition, endPosition, t);

            yield return null; //프레임 간격대로 실행

        }
    }

    public void DisplayItems(Dictionary<IItem, int> itemsToDisplay)
    {
        storageSlotUI.UpdateUI(); // UpdateUI 함수 호출
    }
}

