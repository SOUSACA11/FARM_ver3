using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinnyProcessItem;
using JinnyCropItem;

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

            orders.Add(new Order
            {
                ItemId = randomItem.ItemId,
                ItemImage = randomItem.ItemImage,
                Quantity = randomQuantity,
                TotalCost = randomItem.ItemCost * randomQuantity
            });

            // Debug.Log("���ο� �ֹ�");
            Debug.Log("������ �������� ItemId: " + randomItem.ItemId);


            foreach (var order in orders)
            {
                Debug.Log($"Order created with ItemId: {order.ItemId}");

            }
        }

        //Debug.Log("�ֹ� ���� �ǳ�?");
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