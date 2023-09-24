using UnityEngine;
using System.Collections.Generic;
using JinnyBuilding;
using JinnyFarm;
using JinnyCropItem;
using JinnyProcessItem;
using JinnyAnimal;

//by.J:230721 아이템 사전
public class ItemDic : MonoBehaviour
{
    public static ItemDic Instance { get; private set; }

    Building building;
    public Farm farm;
    CropItem cropItem;
    ProcessItem processItem;
    Animal animal;
   
    //아이템 사전 선언
    public Dictionary<string, object> Item = new Dictionary<string, object>();

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        building = gameObject.AddComponent<Building>();
        farm = gameObject.AddComponent<Farm>();
        cropItem = gameObject.AddComponent<CropItem>();
        processItem = gameObject.AddComponent<ProcessItem>();
        animal = gameObject.AddComponent<Animal>();

    }
    public void Start()
    {
        Item.Add("건물", building.buildingDataList);
        Item.Add("농장밭", farm.farmDataList);
        Item.Add("농장 생산품", cropItem.cropItemDataInfoList);
        Item.Add("가공 생산품", processItem.processItemDataInfoList);
        Item.Add("동물", animal.animalDataList);

        Debug.Log("아이템 리스트 갯수: " + Item.Count);
    }
}