using JinnyCropItem;
using JinnyProcessItem;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//by.J:230814 주문서 표시 UI / 리셋 버튼
//by.J:230816 주문 클릭 후 납부시 재화증가
//by.J:230817 주문서 클릭시 효과 적용
//by.J:230830 주문서 클릭 후 납부시 창고 아이템 차감
//by.J:230901 래퍼클래스 타입으로 통합
public class OrderSlotManagerUI : MonoBehaviour
{
    public OrderPaper orderPaper;
    public GameObject orderItemPrefab;    //주문 아이템을 표시하기 위한 프리팹
    public Transform orderListParent;     //주문 아이템들을 자식으로 가질 부모 위치
    public TextMeshProUGUI totalCostText; //주문서 총 가격
    public GameObject orderSheetPrefab;   //주문서 총 UI 프리팹

    private int currentOrderCount = 0;            //현재 주문서 개수
    private static GameObject selectedOrderPaper; //현재 선택된 주문서 / 같은 변수 공유하도록 static

    private Vector3 originalOrderSize;  //주문서의 원래 크기
    public float enlargedScale = 0.5f;  //크게 할 때의 스케일 값

    private List<Order> currentOrders = new List<Order>();//////////
   

    private void Start()
    {
        // 초기 설정 시 주문서의 원래 크기 저장
        originalOrderSize = orderPaper.transform.localScale;
    }

    private void Update()
    {
        //마우스 클릭, 터치시 UI 오브젝트 아닐경우 원래 크기대로
        if (IsInputDetected() && !IsPointerOverUIObject())
        {
            if (selectedOrderPaper != null)
            {
                ResetOrderSize(selectedOrderPaper);
                selectedOrderPaper = null;
            }
        }
    }

    //주문서 표시
    public void TriggerOrder()
    {
        Debug.Log("트리거");
        MultipleOrder(3);
    }

    //랜덤 아이템 발동
    public void TriggerRandomOrder()
    {
        DisplayOrder();
    }

    //주문서
    public void DisplayOrder()
    {
        //주문서 생성
        currentOrders = orderPaper.RandomOrder(3);
        OrdersTable.instance.orders = new List<Order>(currentOrders);
        Debug.Log("생산직후 리스트 카운트 :" + OrdersTable.instance.orders.Count);

        //기존 주문 아이템 UI 삭제
        foreach (Transform child in orderListParent)
        {
            Destroy(child.gameObject);
            //Debug.Log("기존 주문 삭제");
        }

        //새 주문 아이템 UI 생성
        foreach (var order in OrdersTable.instance.orders)
        {
            GameObject orderItem = Instantiate(orderItemPrefab, orderListParent);
            orderItem.GetComponentInChildren<TextMeshProUGUI>().text = order.ItemName + " x" + order.Quantity;
            orderItem.GetComponentInChildren<Image>().sprite = order.ItemImage;

            Debug.Log($"Order created with ItemId22222: {order.ItemId}");

        }
        //총 가격 표시
        int totalCost = orderPaper.TotalCost(OrdersTable.instance.orders);
        totalCostText.text = totalCost.ToString();
        //Debug.Log("총 가격 표시");
    }

    //주문서 여러개
    public void MultipleOrder(int count)
    {
        Debug.Log(gameObject.name + "멀티플" + OrdersTable.instance.orders.Count);
        //최대 3개까지만 생성
        if (currentOrderCount >= 3)
        {
            Debug.Log("이미 최대 주문서를 생성했습니다.");
            return;
        }

        for (int i = 0; i < count; i++)
        {
            GameObject orderSheet = Instantiate(orderSheetPrefab);
            orderSheet.transform.SetParent(orderListParent);

            OrderSlotManagerUI orderSlotManager = orderSheet.GetComponent<OrderSlotManagerUI>();
            if (orderSlotManager != null)
            {
                orderSlotManager.TriggerRandomOrder();

                if (orderSlotManager.orderPaper == null || orderSlotManager.orderItemPrefab == null)
                {
                    continue;
                }
                orderSlotManager.TriggerRandomOrder();
            }
            //복제된 오브젝트 활성화
            ;
            orderSheet.SetActive(true);
        }

        currentOrderCount += count;  //주문서를 생성한 후 현재 주문서 수 증가

        //현재 주문서 수가 3개 이상이면 3개로 설정
        if (currentOrderCount > 3)
            currentOrderCount = 3;

    }

    //리셋 버튼
    public void ResetOrder()
    {
        //모든 주문서 오브젝트 삭제
        foreach (Transform child in orderListParent)
        {
            Debug.Log("리셋");
            Destroy(child.gameObject);
        }

        //현재 주문서 개수 초기화
        currentOrderCount = 0;
    }

    //주문서 선택
    public void SelectOrder(GameObject orderPaper)
    {
        Debug.Log("선택 주문서");
        //Debug.Log("주문서 선택쓰" + selectedOrderPaper);
        //Debug.Log(gameObject.name +" : "+ OrdersTable.orders.Count);
        //Debug.Log(test.Count);

        //이전 선택 주문서와 클릭 주문서 같을 경우
        if (selectedOrderPaper == orderPaper)
        {
            Debug.Log("에스");
            //주문서 크기 원래대로 후 선택 해제
            ResetOrderSize(orderPaper);
            selectedOrderPaper = null;
        }
        else
        {
            Debug.Log("노");
            //이전 선택 주문서 크기 원래대로
            if (selectedOrderPaper != null)
            {
                ResetOrderSize(selectedOrderPaper);
            }

            //새로운 주문서 선택 및 크기 증가
            selectedOrderPaper = orderPaper;
            EnlargeOrderSize(orderPaper);
        }
    }

    //납부하기 버튼
    public void PayButtonClick()
    {
        Debug.Log("납부하기 버튼 클릭");

        if (selectedOrderPaper != null)
        {
            OrderSlotManagerUI orderUI = selectedOrderPaper.GetComponent<OrderSlotManagerUI>();
            if (orderUI == null)
            {
                Debug.LogError("Order component not found on selectedOrderPaper!");
                return;
            }

            if (orderUI.currentOrders == null || orderUI.currentOrders.Count == 0)
            {
                Debug.LogError("No orders associated with selectedOrderPaper!");
                return;
            }

            //모든 주문 아이템 순회하며 창고 아이템 확인 후 차감
            foreach (var order in orderUI.currentOrders)
            {
                IItem requiredItem = GetItemFromID(order.ItemId);
                if (requiredItem == null)
                {
                    Debug.LogError($"No item found with the given ID: {order.ItemId}!");
                    return;
                }

                int storageAmount = Storage.Instance.GetItemAmount(requiredItem);
                if (storageAmount < order.Quantity)
                {
                    Debug.LogWarning("창고에 필요한 아이템이 부족합니다.");
                    return;  //창고에 필요한 아이템 충분하지 않으면 작업 중지
                }

                //필요한 아이템과 수량 창고에서 제거
                Storage.Instance.RemoveItem(requiredItem, order.Quantity);
            }

            TextMeshProUGUI totalCostText = selectedOrderPaper.transform.Find("gold count").GetComponent<TextMeshProUGUI>();
            if (totalCostText == null)
            {
                Debug.LogError("Failed to find gold count text component!");
                return;
            }

            int cost = int.Parse(totalCostText.text);
            MoneySystem.Instance.AddGold(cost);
            Destroy(selectedOrderPaper);
            selectedOrderPaper = null;
        }
    }

    //아이템 ID를 사용하여 IItem을 가져옴
    private IItem GetItemFromID(string itemId)
    {
        //Crop items 확인
        CropItemDataInfo cropItem = JinnyCropItem.CropItem.Instance.cropItemDataInfoList.FirstOrDefault(item => item.ItemId == itemId);

        if (cropItem.IsInitialized)
        {
            Debug.Log("Found in Crop Items: " + cropItem.ItemId);
            return new CropItemIItem(cropItem); //CropItemIItem 객체 반환
        }

        //Process items 확인
        ProcessItemDataInfo processItem = JinnyProcessItem.ProcessItem.Instance.processItemDataInfoList.FirstOrDefault(item => item.ItemId == itemId);
        if (processItem.IsInitialized)
        {
            return new ProcessItemIItem(processItem); //ProcessItemIItem 객체 반환
        }

        //해당 ID와 일치하는 아이템 없음
        return null;
    }

    //주문서 크기 증가
    private void EnlargeOrderSize(GameObject orderPaper)
    {
        orderPaper.transform.localScale = originalOrderSize * enlargedScale;
    }

    //주문서 크기 원래대로
    private void ResetOrderSize(GameObject orderPaper)
    {
        orderPaper.transform.localScale = originalOrderSize;
    }

    //마우스 클릭, 터치 감지
    private bool IsInputDetected()
    {
        //마우스 클릭
        if (Input.GetMouseButtonDown(0))
            return true;

        //터치 입력
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            return true;

        return false;
    }

    //마우스 포인터, 터치가 UI 오브젝트 위에 있는지 확인
    private bool IsPointerOverUIObject()
    {
        //마우스 포지션
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}