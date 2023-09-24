using JinnyCropItem;
using JinnyProcessItem;
using System.Collections.Generic;
using UnityEngine;

//by.J:230811 �ֹ� ���� / ���� �ֹ��� ���� �� �� ��� ���
public class OrderPaper : MonoBehaviour
{
    public CropItem cropItemManager;
    public ProcessItem processItemManager;

    //�������� �ֹ��� ����
    public List<Order> RandomOrder(int numberOfItems)
    {
        List<Order> orders = new List<Order>();
        List<IItem> allItems = new List<IItem>();

        //CropItem �� ProcessItem�� �׸���� IItem ����Ʈ�� �߰�
        foreach (var item in cropItemManager.cropItemDataInfoList)
        {
            allItems.Add(new CropItemIItem(item));
            //Debug.Log("���۹� �߰� ��");
        }

        foreach (var item in processItemManager.processItemDataInfoList)
        {
            allItems.Add(new ProcessItemIItem(item));
            //Debug.Log("����ǰ �߰� ��");
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

            Debug.Log("������ �������� ItemId: " + randomItem.ItemId);

            foreach (var order in orders)
            {
                Debug.Log($"Order created with ItemId: {order.ItemId}");
            }
        }
        return orders;
    }

    //�ֹ��� �� ���
    public int TotalCost(List<Order> orders)
    {
        int totalCost = 0; //�� ��� �ʱ�ȭ

        foreach (var order in orders)
        {
            totalCost += order.TotalCost;
        }

        return totalCost;
    }
}
