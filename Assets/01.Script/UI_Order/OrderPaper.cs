using JinnyCropItem;
using JinnyProcessItem;
using System.Collections.Generic;
using UnityEngine;

//by.J:230811 주문 관리 / 랜덤 주문서 생성 및 총 비용 계산
public class OrderPaper : MonoBehaviour
{
    public CropItem cropItemManager;
    public ProcessItem processItemManager;

    //랜덤으로 주문서 생성
    public List<Order> RandomOrder(int numberOfItems)
    {
        List<Order> orders = new List<Order>();
        List<IItem> allItems = new List<IItem>();

        //CropItem 및 ProcessItem의 항목들을 IItem 리스트로 추가
        foreach (var item in cropItemManager.cropItemDataInfoList)
        {
            allItems.Add(new CropItemIItem(item));
            //Debug.Log("농작물 추가 완");
        }

        foreach (var item in processItemManager.processItemDataInfoList)
        {
            allItems.Add(new ProcessItemIItem(item));
            //Debug.Log("가공품 추가 완");
        }

        for (int i = 0; i < numberOfItems; i++)
        {
            int randomIndex = Random.Range(0, allItems.Count);
            IItem randomItem = allItems[randomIndex];
            int randomQuantity = Random.Range(1, 6);

            Order newOrder = new Order
            {
                ItemId = randomItem.ItemId,
                ItemImage = randomItem.ItemImage,
                Quantity = randomQuantity,
                TotalCost = randomItem.ItemCost * randomQuantity
            };

            orders.Add(newOrder);

            Debug.Log("생성된 아이템의 ItemId: " + randomItem.ItemId);

            foreach (var order in orders)
            {
                Debug.Log($"Order created with ItemId: {order.ItemId}");
            }
        }
        return orders;
    }

    //주문서 총 비용
    public int TotalCost(List<Order> orders)
    {
        int totalCost = 0; //총 비용 초기화

        foreach (var order in orders)
        {
            totalCost += order.TotalCost;
        }

        return totalCost;
    }
}
