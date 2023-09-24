using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//by.J:230808 â�� �ý���
//by.J:230829 �̱���
//by.J:230901 ��� ������ ����Ŭ���� Ÿ��
public class Storage : MonoBehaviour
{
    //Storage = Processitem, CropItem �� IItem Ÿ������ ���
    private Dictionary<IItem, int> items; //���� ������ ����
    private int capa;                     //�ִ� �뷮

    public static Storage Instance { get; private set; } //�̱���

    //�̺�Ʈ
    public delegate void StorageChanged();
    public event StorageChanged OnStorageChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Storages(100); //�ʱ� �뷮
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void Storages(int capa)
    {
        this.items = new Dictionary<IItem, int>();
        this.capa = capa;
    }

    public Dictionary<IItem, int> Items { get { return items; } } 

    //������ �߰�
    public bool AddItem(IItem item, int count)
    {
        Debug.Log("â�� ������ �߰�");

        Debug.Log($"[Storage] Adding Item Type: {item.GetType().Name}");

        if (item == null)
        {
            Debug.LogError("Attempting to add a null item to storage.");
            return false;
        }


        if (GetTotalItemCount() + count > capa)
        {
            //â���� �ִ� �뷮�� �ʰ��ϸ� �������� �߰����� ����
            return false;
        }

        if (items.ContainsKey(item))
        {
            items[item] += count;  //�̹� �������� �������� ��� ������ �ø�
        }
        else
        {
            items[item] = count;  //���ο� �������� ��� �������� �߰�
        }

        OnStorageChanged?.Invoke(); //�������� �߰��Ǹ� �̺�Ʈ ȣ��

        Debug.Log($"[Storage] After adding item. Current item list:");
        foreach (var pair in items)
        {
            Debug.Log($"- Item: {pair.Key.ItemName} | Count: {pair.Value}");
        }

        Debug.Log($"[Storage] Added Item: {item.ItemName}");

        Debug.Log("â�� �߰� ����" + item);

        return true;
    }

    //������ ����
    public bool RemoveItem(IItem item, int count)
    {
        Debug.Log("â�� ������ ����");

        if (!items.ContainsKey(item) || items[item] < count)
        {
            //�������� �������� �ƴϰų�, ������ ������� ������ �������� �������� ����
            return false;
        }

        items[item] -= count;  //������ ���� ����
        if (items[item] == 0)
        {
            items.Remove(item);  //������ ������ 0�� �Ǹ� �������� ����
        }

        OnStorageChanged?.Invoke(); //�������� ���ŵǸ� �̺�Ʈ ȣ��
        return true;
    }

    //���� �� ������ �� ���� ��ȯ
    private int GetTotalItemCount()
    {
        if (items == null)
        {
            Debug.LogError("Items dictionary is not initialized!");
            return 0;
        }

        int totalCount = 0;
        foreach (int count in items.Values)
        {
            totalCount += count;
        }
        return totalCount;
    }
    
    //â�� �� Ư�� ������ ���� Ȯ�� ���
    public int GetItemAmount(IItem item)
    {
        //������ ����Ʈ�� �ִ� �ش� �������� ���� ��ȯ
        if (items.TryGetValue(item, out int count))
        {
            Debug.Log($"[Storage] Current item list in storage:");
            Debug.Log($"[Storage] Item: {item.ItemName} | Count: {count}");
            foreach (var pair in items)
            {
                Debug.Log($"- Item: {pair.Key.ItemName} | Count: {pair.Value}");
            }
            return count;
        }
        else
        {
            return 0; //�������� â�� ������ 0 ��ȯ
        }
    }

    //�۹� �����۸� ��������
    public Dictionary<IItem, int> GetCropItems()
    {
        Debug.Log("�۹�");
        return items.Where(pair => pair.Key is CropItemIItem).ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    //����ǰ �����۸� ��������
    public Dictionary<IItem, int> GetProcessItems()
    {
        Debug.Log("����ǰ");
        return items.Where(pair => pair.Key is ProcessItemIItem).ToDictionary(pair => pair.Key, pair => pair.Value);
    }
}
