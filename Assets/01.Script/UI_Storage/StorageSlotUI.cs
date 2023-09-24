using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JinnyProcessItem;
using JinnyCropItem;
using System.Collections.Generic;

//by.J:230808 â�� ���� UI ����
public class StorageSlotUI : MonoBehaviour
{
    public GameObject slotPrefab;  //���� ������
    private Storage storage;       //â�� ������
    private StorageManagerUI.TabType currentTab = StorageManagerUI.TabType.CropItem;
    
    private void Awake()
    {
        storage = Storage.Instance;
       
        storage.OnStorageChanged += UpdateUI; // �̺�Ʈ ����
    }

    //â�� ����
    private void Start() 
    {
        UpdateUI(); //�ʱ� UI ����

        //â�� ������ ������ ���� ���� ����
        foreach (var item in storage.Items)
        {
            AddItemSlot(item.Key, item.Value);
        }
    }

    //���� ���� �� â�� �߰�
    private void AddItemSlot(IItem itemData, int count)
    {
        Debug.Log("â�� ���� �߰�");
        GameObject slot = Instantiate(slotPrefab, transform);  //���� ������ ����
        //Debug.Log("���� ��ġ :"+ transform);
        StorageSlot slotScript = slot.GetComponent<StorageSlot>();  //StarageSlot ��ũ��Ʈ ����

        //���Կ� ������ �����͸� ����
        slotScript.SetItem(itemData, count);
    }

    public void UpdateUI()
    {
        //â���� ��� �����ۿ� ���� UI ���� ����
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Dictionary<IItem, int> itemsToDisplay = new Dictionary<IItem, int>();

        //���� �ǿ� ���� ǥ�õ� �����۵��� ��� ����
        switch (currentTab)
        {
            case StorageManagerUI.TabType.CropItem:
                itemsToDisplay = storage.GetCropItems();
                break;
            case StorageManagerUI.TabType.ProcessItem:
                itemsToDisplay = storage.GetProcessItems();
                break;
        }

        //storage�� ������ �α׷� ���
        foreach (var item in itemsToDisplay)
        {
            Debug.Log($"Item: {item.Key.ItemName}, Count: {item.Value}");
        }

        //�� ���� �߰�
        foreach (var item in itemsToDisplay)
        {
            AddItemSlot(item.Key, item.Value);
        }
    }

    public void SetCurrentTab(StorageManagerUI.TabType tabType)
    {
        currentTab = tabType;
        UpdateUI(); //���� �ǿ� ���� UI ������Ʈ
    }
}

