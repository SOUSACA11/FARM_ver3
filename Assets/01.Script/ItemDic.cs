using UnityEngine;
using System.Collections.Generic;
using JinnyBuilding;
using JinnyFarm;
using JinnyCropItem;
using JinnyProcessItem;
using JinnyAnimal;

//by.J:230721 ������ ����
public class ItemDic : MonoBehaviour
{
    public static ItemDic Instance { get; private set; }

    Building building;
    public Farm farm;
    CropItem cropItem;
    ProcessItem processItem;
    Animal animal;
   
    //������ ���� ����
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

        Item.Add("�ǹ�", building.buildingDataList);
        Item.Add("�����", farm.farmDataList);
        Item.Add("���� ����ǰ", cropItem.cropItemDataInfoList);
        Item.Add("���� ����ǰ", processItem.processItemDataInfoList);
        Item.Add("����", animal.animalDataList);

        Debug.Log("������ ����Ʈ ����: " + Item.Count);

    }
}