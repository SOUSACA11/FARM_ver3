using JinnyCropItem;
using JinnyProcessItem;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//by.J:230814 �ֹ��� ǥ�� UI / ���� ��ư
//by.J:230816 �ֹ� Ŭ�� �� ���ν� ��ȭ����
//by.J:230817 �ֹ��� Ŭ���� ȿ�� ����
//by.J:230830 �ֹ��� Ŭ�� �� ���ν� â�� ������ ����
//by.J:230901 ����Ŭ���� Ÿ������ ����
public class OrderSlotManagerUI : MonoBehaviour
{
    public OrderPaper orderPaper;
    public GameObject orderItemPrefab;    //�ֹ� �������� ǥ���ϱ� ���� ������
    public Transform orderListParent;     //�ֹ� �����۵��� �ڽ����� ���� �θ� ��ġ
    public TextMeshProUGUI totalCostText; //�ֹ��� �� ����
    public GameObject orderSheetPrefab;   //�ֹ��� �� UI ������

    private int currentOrderCount = 0;            //���� �ֹ��� ����
    private static GameObject selectedOrderPaper; //���� ���õ� �ֹ��� / ���� ���� �����ϵ��� static

    private Vector3 originalOrderSize;  //�ֹ����� ���� ũ��
    public float enlargedScale = 0.5f;  //ũ�� �� ���� ������ ��

    private List<Order> currentOrders = new List<Order>();//////////
   

    private void Start()
    {
        // �ʱ� ���� �� �ֹ����� ���� ũ�� ����
        originalOrderSize = orderPaper.transform.localScale;
    }

    private void Update()
    {
        //���콺 Ŭ��, ��ġ�� UI ������Ʈ �ƴҰ�� ���� ũ����
        if (IsInputDetected() && !IsPointerOverUIObject())
        {
            if (selectedOrderPaper != null)
            {
                ResetOrderSize(selectedOrderPaper);
                selectedOrderPaper = null;
            }
        }
    }

    //�ֹ��� ǥ��
    public void TriggerOrder()
    {
        Debug.Log("Ʈ����");
        MultipleOrder(3);
    }

    //���� ������ �ߵ�
    public void TriggerRandomOrder()
    {
        DisplayOrder();
    }

    //�ֹ���
    public void DisplayOrder()
    {
        //�ֹ��� ����
        currentOrders = orderPaper.RandomOrder(3);
        OrdersTable.instance.orders = new List<Order>(currentOrders);
        Debug.Log("�������� ����Ʈ ī��Ʈ :" + OrdersTable.instance.orders.Count);

        //���� �ֹ� ������ UI ����
        foreach (Transform child in orderListParent)
        {
            Destroy(child.gameObject);
            //Debug.Log("���� �ֹ� ����");
        }

        //�� �ֹ� ������ UI ����
        foreach (var order in OrdersTable.instance.orders)
        {
            GameObject orderItem = Instantiate(orderItemPrefab, orderListParent);
            orderItem.GetComponentInChildren<TextMeshProUGUI>().text = order.ItemName + " x" + order.Quantity;
            orderItem.GetComponentInChildren<Image>().sprite = order.ItemImage;

            Debug.Log($"Order created with ItemId22222: {order.ItemId}");

        }
        //�� ���� ǥ��
        int totalCost = orderPaper.TotalCost(OrdersTable.instance.orders);
        totalCostText.text = totalCost.ToString();
        //Debug.Log("�� ���� ǥ��");
    }

    //�ֹ��� ������
    public void MultipleOrder(int count)
    {
        Debug.Log(gameObject.name + "��Ƽ��" + OrdersTable.instance.orders.Count);
        //�ִ� 3�������� ����
        if (currentOrderCount >= 3)
        {
            Debug.Log("�̹� �ִ� �ֹ����� �����߽��ϴ�.");
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
            //������ ������Ʈ Ȱ��ȭ
            ;
            orderSheet.SetActive(true);
        }

        currentOrderCount += count;  //�ֹ����� ������ �� ���� �ֹ��� �� ����

        //���� �ֹ��� ���� 3�� �̻��̸� 3���� ����
        if (currentOrderCount > 3)
            currentOrderCount = 3;

    }

    //���� ��ư
    public void ResetOrder()
    {
        //��� �ֹ��� ������Ʈ ����
        foreach (Transform child in orderListParent)
        {
            Debug.Log("����");
            Destroy(child.gameObject);
        }

        //���� �ֹ��� ���� �ʱ�ȭ
        currentOrderCount = 0;
    }

    //�ֹ��� ����
    public void SelectOrder(GameObject orderPaper)
    {
        Debug.Log("���� �ֹ���");
        //Debug.Log("�ֹ��� ���þ�" + selectedOrderPaper);
        //Debug.Log(gameObject.name +" : "+ OrdersTable.orders.Count);
        //Debug.Log(test.Count);

        //���� ���� �ֹ����� Ŭ�� �ֹ��� ���� ���
        if (selectedOrderPaper == orderPaper)
        {
            Debug.Log("����");
            //�ֹ��� ũ�� ������� �� ���� ����
            ResetOrderSize(orderPaper);
            selectedOrderPaper = null;
        }
        else
        {
            Debug.Log("��");
            //���� ���� �ֹ��� ũ�� �������
            if (selectedOrderPaper != null)
            {
                ResetOrderSize(selectedOrderPaper);
            }

            //���ο� �ֹ��� ���� �� ũ�� ����
            selectedOrderPaper = orderPaper;
            EnlargeOrderSize(orderPaper);
        }
    }

    //�����ϱ� ��ư
    public void PayButtonClick()
    {
        Debug.Log("�����ϱ� ��ư Ŭ��");

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

            //��� �ֹ� ������ ��ȸ�ϸ� â�� ������ Ȯ�� �� ����
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
                    Debug.LogWarning("â�� �ʿ��� �������� �����մϴ�.");
                    return;  //â�� �ʿ��� ������ ������� ������ �۾� ����
                }

                //�ʿ��� �����۰� ���� â���� ����
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

    //������ ID�� ����Ͽ� IItem�� ������
    private IItem GetItemFromID(string itemId)
    {
        //Crop items Ȯ��
        CropItemDataInfo cropItem = JinnyCropItem.CropItem.Instance.cropItemDataInfoList.FirstOrDefault(item => item.ItemId == itemId);

        if (cropItem.IsInitialized)
        {
            Debug.Log("Found in Crop Items: " + cropItem.ItemId);
            return new CropItemIItem(cropItem); //CropItemIItem ��ü ��ȯ
        }

        //Process items Ȯ��
        ProcessItemDataInfo processItem = JinnyProcessItem.ProcessItem.Instance.processItemDataInfoList.FirstOrDefault(item => item.ItemId == itemId);
        if (processItem.IsInitialized)
        {
            return new ProcessItemIItem(processItem); //ProcessItemIItem ��ü ��ȯ
        }

        //�ش� ID�� ��ġ�ϴ� ������ ����
        return null;
    }

    //�ֹ��� ũ�� ����
    private void EnlargeOrderSize(GameObject orderPaper)
    {
        orderPaper.transform.localScale = originalOrderSize * enlargedScale;
    }

    //�ֹ��� ũ�� �������
    private void ResetOrderSize(GameObject orderPaper)
    {
        orderPaper.transform.localScale = originalOrderSize;
    }

    //���콺 Ŭ��, ��ġ ����
    private bool IsInputDetected()
    {
        //���콺 Ŭ��
        if (Input.GetMouseButtonDown(0))
            return true;

        //��ġ �Է�
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            return true;

        return false;
    }

    //���콺 ������, ��ġ�� UI ������Ʈ ���� �ִ��� Ȯ��
    private bool IsPointerOverUIObject()
    {
        //���콺 ������
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}