using UnityEngine;
using UnityEngine.UI;
using TMPro;
using JinnyProcessItem;
using JinnyCropItem;
using System.Collections.Generic;

//by.J:230808 창고 슬롯 UI 관리
public class StorageSlotUI : MonoBehaviour
{
    public GameObject slotPrefab;  //슬롯 프리팹
    private Storage storage;       //창고 데이터
    private StorageManagerUI.TabType currentTab = StorageManagerUI.TabType.CropItem;
    
    private void Awake()
    {
        storage = Storage.Instance;
       
        storage.OnStorageChanged += UpdateUI; // 이벤트 구독
    }

    //창고 생성
    private void Start() 
    {
        UpdateUI(); //초기 UI 설정

        //창고 아이템 각각에 대한 슬롯 생성
        foreach (var item in storage.Items)
        {
            AddItemSlot(item.Key, item.Value);
        }
    }

    //슬롯 생성 후 창고 추가
    private void AddItemSlot(IItem itemData, int count)
    {
        Debug.Log("창고 슬롯 추가");
        GameObject slot = Instantiate(slotPrefab, transform);  //슬롯 프리팹 생성
        //Debug.Log("슬롯 위치 :"+ transform);
        StorageSlot slotScript = slot.GetComponent<StorageSlot>();  //StarageSlot 스크립트 참조

        //슬롯에 아이템 데이터를 설정
        slotScript.SetItem(itemData, count);
    }

    public void UpdateUI()
    {
        //창고의 모든 아이템에 대해 UI 슬롯 생성
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Dictionary<IItem, int> itemsToDisplay = new Dictionary<IItem, int>();

        //현재 탭에 따라 표시될 아이템들의 목록 설정
        switch (currentTab)
        {
            case StorageManagerUI.TabType.CropItem:
                itemsToDisplay = storage.GetCropItems();
                break;
            case StorageManagerUI.TabType.ProcessItem:
                itemsToDisplay = storage.GetProcessItems();
                break;
        }

        //storage의 아이템 로그로 출력
        foreach (var item in itemsToDisplay)
        {
            Debug.Log($"Item: {item.Key.ItemName}, Count: {item.Value}");
        }

        //새 슬롯 추가
        foreach (var item in itemsToDisplay)
        {
            AddItemSlot(item.Key, item.Value);
        }
    }

    public void SetCurrentTab(StorageManagerUI.TabType tabType)
    {
        currentTab = tabType;
        UpdateUI(); //현재 탭에 따라 UI 업데이트
    }
}

