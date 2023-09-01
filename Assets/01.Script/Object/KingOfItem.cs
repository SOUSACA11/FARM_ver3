using System.Collections.Generic;
using UnityEngine;
using System.Linq; //(Language INtegrated Query) �÷��� ������ ������ ������ ���� �޼��� ����
using JinnyProcessItem;
using JinnyCropItem;

//by.J:230808 ProcessItem, CropItem �Ѱ���
public class KingOfItem : MonoBehaviour
{
    //�����۵��� �����ϴ� ����Ʈ
    public List<IItem> items = new List<IItem>();

    //������ ��ȯ
    public List<IItem> GetAllItems()
    {
        return items;
    }

    private void Start()
    {
        //Scene�� �ִ� ��� IItem�� ã�Ƽ� ����Ʈ�� �߰�
        //LINQ�� OfType() Ư�� Ÿ���� �����͸� ����
        foreach (IItem item in FindObjectsOfType<MonoBehaviour>().OfType<IItem>())
        {
            items.Add(item);
        }

        //��� �������� ������ ���
        foreach (IItem item in items)
        {
            Debug.Log("Item names: " + string.Join(", ", item.ItemName));
            Debug.Log("Item costs: " + string.Join(", ", item.ItemCost));
        }
    }
}
