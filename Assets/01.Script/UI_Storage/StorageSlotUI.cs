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
    private Storage storage;  //창고 데이터
    private StorageManagerUI.TabType currentTab = StorageManagerUI.TabType.CropItem;
    private void Awake()
    {
        storage = Storage.Instance;
        //storage = gameObject.AddComponent<Storage>();


        storage.OnStorageChanged += UpdateUI; // 이벤트 구독
    }


    //창고 생성
    private void Start() //아이템 획득시 추가 및 사용시 제거 기능 추가 필요
    {
        //Storage storage = gameObject.AddComponent<Storage>();
        //storage.Storages(100);  //용량이 100인 창고를 생성

       
        UpdateUI(); // 초기 UI 설정



        //창고의 아이템 각각에 대해 슬롯을 생성
        foreach (var item in storage.Items)
        {
            AddItemSlot(item.Key, item.Value);
        }
    }

    //새로운 슬롯을 생성하고 창고 UI에 추가
    private void AddItemSlot(IItem itemData, int count)
    {
        Debug.Log("창고 슬롯 추가");
        GameObject slot = Instantiate(slotPrefab, transform);  //슬롯 프리팹을 생성
        //Debug.Log("슬롯 위치 :"+ transform);
        StorageSlot slotScript = slot.GetComponent<StorageSlot>();  //StarageSlot 스크립트를 참조

        //슬롯에 아이템 데이터를 설정
        slotScript.SetItem(itemData, count);
    }

    public void UpdateUI()
    {
        //    //기존 슬롯 삭제
        //    foreach (Transform child in transform)
        //    {
        //        Destroy(child.gameObject);
        //    }

        //    // storage의 아이템 로그로 출력
        //    foreach (var item in storage.Items)
        //    {
        //        Debug.Log($"Item: {item.Key.ItemName}, Count: {item.Value}");  //ItemName이 IItem에 있는 속성이라고 가정
        //    }

        //    //새 슬롯 추가
        //    foreach (var item in storage.Items)
        //    {
        //        AddItemSlot(item.Key, item.Value);
        //    }
        //}//기존 슬롯 삭제
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        Dictionary<IItem, int> itemsToDisplay = new Dictionary<IItem, int>();

        switch (currentTab)
        {
            case StorageManagerUI.TabType.CropItem:
                itemsToDisplay = storage.GetCropItems();
                break;
            case StorageManagerUI.TabType.ProcessItem:
                itemsToDisplay = storage.GetProcessItems();
                break;
        }

        // storage의 아이템 로그로 출력
        foreach (var item in itemsToDisplay)
        {
            Debug.Log($"Item: {item.Key.ItemName}, Count: {item.Value}");
        }

        // 새 슬롯 추가
        foreach (var item in itemsToDisplay)
        {
            AddItemSlot(item.Key, item.Value);
        }
    }

    public void SetCurrentTab(StorageManagerUI.TabType tabType)
    {
        currentTab = tabType;
        UpdateUI(); // 현재 탭에 따라 UI를 업데이트합니다.
    }
}

