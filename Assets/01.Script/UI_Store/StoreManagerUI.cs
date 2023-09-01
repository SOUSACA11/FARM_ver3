using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using JinnyBuilding;
using JinnyFarm;
using JinnyAnimal;

//by.J:230724 상점 창 클릭시 활성화 / 메뉴 버튼 비활성화 / 닫기 버튼 / 상점 아이템 읽기
//by.J:230724 상점 탭 UI 설정 추가
//by.J:230809 상점 아이템 살 경우 상점 창 아래로 내리고 배치 완료 할 경우 원위치
//by.J:230810 상점 아이템 리스트 조회해서 순차적으로 슬롯에 배치 / 이전, 다음 버튼 클릭시 맞는 정보 제공

public class StoreManagerUI : MonoBehaviour
{
    public Image image;            //움직일 상점 창 이미지 
    public Vector3 endPosition;    //마지막 이동 위치
    public float speed = 120f;     //이동 속도
    private Vector3 startPosition; //시작위치

    public Button closeButton;   //닫기 버튼
    public Button inviButton1;   //비활성화 할 버튼 1번
    public Button inviButton2;   //비활성화 할 버튼 2번
    public Button inviButton3;   //비활성화 할 버튼 3번
    public Button nextButton;    //다음 페이지 버튼

    public StoreSlot[] slots;           //슬롯 개수
    private int currentPage = 0;        //현재 페이지 번호
    private const int itemsPerPage = 3; //페이지당 아이템 수

    public ItemDic itemDic;                    //아이템 정보를 가진 ItemDic 클래스
    private Dictionary<string, object> items;  //아이템 정보를 담은 사전

    List<StoreItemUI> storeItems = new List<StoreItemUI>();  //상점 아이템 객체들을 저장할 리스트

    //public GameObject itemPrefab;
    //public Transform itemParan;

    // *아이템 정보 관련* 
    private TabType currentTab = TabType.Building; //처음 빌딩 탭 보여주기

    public enum TabType
    {
        Building,
        Farm,
        Animal
    }

    public void TabBuilding() //빌딩 탭
    {
        currentTab = TabType.Building;
        currentPage = 0; 
        DisplayItems();
    }

    public void TabFarm() //농장밭 탭
    {
        currentTab = TabType.Farm;
        currentPage = 0; 
        DisplayItems();
    }

    public void TabAnimal() //동물 탭
    {
        currentTab = TabType.Animal;
        currentPage = 0; 
        DisplayItems();
    }

    public void NextPage() //다음 버튼
    {
        currentPage++;
        DisplayItems();
    }

    public void PreviousPage() //이전 버튼
    {
        currentPage = Mathf.Max(0, currentPage - 1); //0보다 작지 않게
        DisplayItems();
    }

    //다음 페이지 버튼 활성화, 비활성화 / 리스트 마지막 아이템 일 경우 다음 페이지 비활성화
    void NextButtonDecision<T>(List<T> itemList)
    {
        if ((currentPage +1) * itemsPerPage < itemList.Count)
        {
            nextButton.interactable = true;
        }
        else
        {
            nextButton.interactable = false;
        }
    }
  
    //빌딩 페이지
    void DisplayBuilding(List<BuildingDataInfo> buildingList) 
    {
        int start = currentPage * itemsPerPage;
        int end = Mathf.Min(start + itemsPerPage, buildingList.Count); //Mathf.Min = a,b 중 작은 값 반환

        foreach (StoreSlot slot in slots)
        {
            slot.gameObject.SetActive(false);
        }

        for (int i = start; i < end; i++)
        {
            int slotIndex = i - start;
            slots[slotIndex].SetSlotBuilding(buildingList[i]);
            slots[slotIndex].gameObject.SetActive(true);
        }

        NextButtonDecision(buildingList);
    }

    //농장밭 페이지
    void DisplayFarm(List<FarmDataInfo> farmList)
    {
        int start = currentPage * itemsPerPage;
        int end = Mathf.Min(start + itemsPerPage, farmList.Count); //Mathf.Min = a,b 중 작은 값 반환

        foreach (StoreSlot slot in slots)
        {
            slot.gameObject.SetActive(false);
        }

        for (int i = start; i < end; i++)
        {
            int slotIndex = i - start;
            slots[slotIndex].SetSlotFarm(farmList[i]);
            slots[slotIndex].gameObject.SetActive(true);
        }

        NextButtonDecision(farmList);
    }

    //동물 페이지
    void DisplayAnimal(List<AnimalDataInfo> animalList)
    {
        int start = currentPage * itemsPerPage;
        int end = Mathf.Min(start + itemsPerPage, animalList.Count); //Mathf.Min = a,b 중 작은 값 반환

        foreach (StoreSlot slot in slots)
        {
            slot.gameObject.SetActive(false);
        }

        for (int i = start; i < end; i++)
        {
            int slotIndex = i - start;
            slots[slotIndex].SetSlotAnimal(animalList[i]);
            slots[slotIndex].gameObject.SetActive(true);
        }

        NextButtonDecision(animalList);
    }

    //아이템 정보 보여주기
    void DisplayItems()
    {
        if (currentTab == TabType.Building) //건물 탭 처리
        {
            Building buildingComponent = FindObjectOfType<Building>();
            if (buildingComponent != null)
            {
                List<BuildingDataInfo> buildingList = buildingComponent.buildingDataList;
                DisplayBuilding(buildingList);
            }
        }
        else if (currentTab == TabType.Farm) //농장밭 탭 처리
        {
            Farm farmComponent = FindObjectOfType<Farm>();
            if (farmComponent != null)
            {
                List<FarmDataInfo> farmList = farmComponent.farmDataList; 
                DisplayFarm(farmList);
            }
        }
        else if (currentTab == TabType.Animal) //동물 탭 처리
        {
            Animal animalComponent = FindObjectOfType<Animal>();
            if (animalComponent != null)
            {
                List<AnimalDataInfo> animalList = animalComponent.animalDataList;
                DisplayAnimal(animalList);
            }
        }
    }



    private void Start() 
    {
        //Debug.Log(image.rectTransform.position.x);
        //Debug.Log(image.rectTransform.position.y);

        closeButton.onClick.AddListener(CloseButtonOnClick);    //닫기 버튼 클릭
        startPosition = image.transform.position;               //시작 위치 설정

        itemDic = FindObjectOfType<ItemDic>(); //ItemDic 클래스를 찾아서 itemDic에 저장
        items = itemDic.Item;                  //ItemDic 클래스에 있는 Item 사전을 items에 저장

        DisplayItems(); //아이템 정보 보여주기 실행

    }



    // *UI 이미지 움직임 처리*
    public void CloseButtonOnClick()
    {
        //메뉴 버튼 비활성화, 닫기 버튼 활성화
        image.transform.position = startPosition;
        inviButton1.gameObject.SetActive(true);
        inviButton2.gameObject.SetActive(true);
        inviButton3.gameObject.SetActive(true);
    }

    public void StoreButton_Click()
    {
        //메뉴 버튼 비활성화
        inviButton1.gameObject.SetActive(false);
        inviButton2.gameObject.SetActive(false);
        inviButton3.gameObject.SetActive(false);

        foreach (var slot in slots)
        {
            slot.ResetSlot();
        }

        //상점 창 기능 활성화
        StartCoroutine(MoveImageUp());
    }
    //밖에 있는 상점 창 화면상 배치
    IEnumerator MoveImageUp()
    {
        //처음 y값    : -846
        //마지막 y값  : 318

        float t = 0f; //시간 변수

        Vector3 startPosition = image.transform.position;  //시작 위치 저장

        endPosition = new Vector3(948+400, image.rectTransform.position.y + 1150, 0); //마지막 위치 저장

        while (t < 1f) //t가 1이 될 때까지
        {
            if (image.rectTransform.position.y >= 318) //y값이 318 이상이면 멈춤
            {
                yield break;
            }

            t += Time.deltaTime * speed; //시간 누적

            //Lerp를 이용해 현재 위치에서 endPosition까지 부드럽게 이동
            image.transform.position = Vector3.Lerp(startPosition, endPosition, t);

            yield return null; //프레임 간격대로 실행
        }
    }

    //화면상 배치된 상점 창 아래로 내리기
    public void BottomStore_Click()
    {
        StartCoroutine(MoveImageBottom());
    }
    IEnumerator MoveImageBottom()
    {
        //y값 : -191
        //Debug.Log("상점 창 내리기 시그널");
        float t = 0f; // 시간 변수

        Vector3 startPosition = image.transform.position;  //시작 위치 저장

        endPosition = new Vector3(948+400, image.rectTransform.position.y - 480, 0); //마지막 위치 저장

        while (t < 1f) //t가 1이 될 때까지
        {
            if (image.rectTransform.position.y >= 2000) //y값 2000 이상이면 멈춤
            {
                yield break;
            }

            t += Time.deltaTime * speed; //시간 누적

            //Lerp를 이용해 현재 위치에서 endPosition까지 부드럽게 이동
            image.transform.position = Vector3.Lerp(startPosition, endPosition, t);

            yield return null; //프레임 간격대로 실행

        }
    }

    //화면상 배치된 상점 창 위로 올리기
    public void TopStore_Click()
    {
        StartCoroutine(MoveImageTop());
    }
    IEnumerator MoveImageTop()
    {
        //Debug.Log("상점 창 올리기 시그널");
        float t = 0f; // 시간 변수

        Vector3 startPosition = image.transform.position;  //시작 위치 저장

        endPosition = new Vector3(948+400, image.rectTransform.position.y + 480, 0); //마지막 위치 저장

        while (t < 1f) //t가 1이 될 때까지
        {
            if (image.rectTransform.position.y >= 2000) //y값 2000 이상이면 멈춤
            {
                yield break;
            }

            t += Time.deltaTime * speed; //시간 누적

            //Lerp를 이용해 현재 위치에서 endPosition까지 부드럽게 이동
            image.transform.position = Vector3.Lerp(startPosition, endPosition, t);

            yield return null; //프레임 간격대로 실행

        }
    }

}

