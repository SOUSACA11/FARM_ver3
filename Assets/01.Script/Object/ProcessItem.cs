using UnityEngine;
using System.Collections.Generic;
using System;

//by.J:230720 ����ǰ (����ǰ / ����) ������Ʈ 
//by.J:230721 List ����ȭ
//by.J:230728 �̹��� �߰� �۾�
//by.J:230814 IItem ������ ���� �۾�
//by.J:230818 �̱��� �߰� (�ϼ�ǰ �̹��� ���� ����)
namespace JinnyProcessItem
{
    //����ü ����
    [System.Serializable]
    public struct ProcessItemDataInfo : IItem, IEquatable<ProcessItemDataInfo>
    {
        public bool IsInitialized; //�ʱ�ȭ ���¸� Ȯ���ϱ� ���� �ʵ�

        public string processItemName; //�̸�
        public int processItemCost;    //����
        public Sprite processItemImage;//����ǰ �̹���
        public string processItemId;   //������ ���� ID

        public ProcessItemDataInfo(string name, int cost, Sprite image, string itemId)
        {
            this.processItemName = name;
            this.processItemCost = cost;
            this.processItemImage = image;
            this.processItemId = itemId;
            this.IsInitialized = true;
        }


        public string ItemName => processItemName;
        public int ItemCost => processItemCost;
        public Sprite ItemImage => processItemImage;
        public string ItemId => processItemId;


        public override bool Equals(object obj)
        {
            return obj is ProcessItemDataInfo && Equals((ProcessItemDataInfo)obj);
        }

        public bool Equals(ProcessItemDataInfo other)
        {
            return ItemId == other.ItemId;
        }

        public override int GetHashCode()
        {
            return ItemId.GetHashCode();
        }
    }

    //IItem �������̽� ����
    public class ProcessItem : MonoBehaviour //, IItem
    {
        public static ProcessItem Instance; //�̱���

        [SerializeField] public List<ProcessItemDataInfo> processItemDataInfoList = new List<ProcessItemDataInfo>();

        //public string[] ItemName
        //{
        //    get
        //    {
        //        string[] names = new string[processItemDataInfoList.Count];
        //        for (int i = 0; i < processItemDataInfoList.Count; i++)
        //        {
        //            names[i] = processItemDataInfoList[i].processItemName;
        //        }
        //        return names;
        //    }
        //}

        //public int[] ItemCost
        //{
        //    get
        //    {
        //        int[] costs = new int[processItemDataInfoList.Count];
        //        for (int i = 0; i < processItemDataInfoList.Count; i++)
        //        {
        //            costs[i] = processItemDataInfoList[i].processItemCost;
        //        }
        //        return costs;
        //    }
        //}

        //public Sprite[] ItemImage
        //{
        //    get
        //    {
        //        Sprite[] images = new Sprite[processItemDataInfoList.Count];
        //        for (int i = 0; i < processItemDataInfoList.Count; i++)
        //        {
        //            images[i] = processItemDataInfoList[i].processItemImage;
        //        }
        //        return images;
        //    }
        //}

        //public string[] ItemId
        //{
        //    get
        //    {
        //        string[] names = new string[processItemDataInfoList.Count];
        //        for (int i = 0; i < processItemDataInfoList.Count; i++)
        //        {
        //            names[i] = processItemDataInfoList[i].processItemId;
        //        }
        //        return names;
        //    }
        //}


        //���۽� �ʱ�ȭ ��� ����
        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
            }

            InitializeProcessItems();
            Debug.Log("���� ����ǰ ����Ʈ ũ�� : " + processItemDataInfoList.Count);
        }

        //�ʱ�ȭ ���
        private void InitializeProcessItems()
        {
            //�̹��� �߰�
            Sprite[] sprites = Resources.LoadAll<Sprite>("Item");
            Sprite milk = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_0"));
            Sprite egg = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_1"));
            Sprite pork = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_2"));
            Sprite bread = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_8"));
            Sprite bagutte = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_9"));
            Sprite croissant = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_10"));
            Sprite flour = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_11"));
            Sprite chickenfeed = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_12"));
            Sprite pigfeed = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_13"));
            Sprite cowfeed = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_14"));
            Sprite eggflower = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_15"));
            Sprite bacon = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_16"));
            Sprite tomatojuice = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_17"));
            Sprite carrotjuice = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_18"));
            Sprite butter = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_19"));
            Sprite cheese = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_20"));

            //����
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "����",
            //    processItemCost = 10,
            //    processItemImage = milk,
            //    processItemId = "animal_01"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("����", 10, milk, "animal_01"));
           

            //�ް�
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "�ް�",
            //    processItemCost = 10,
            //    processItemImage = egg,
            //    processItemId = "animal_02"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("�ް�", 10, egg, "animal_02"));

            //�������
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "�������",
            //    processItemCost = 10,
            //    processItemImage = pork,
            //    processItemId = "animal_03"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("�������", 10, pork, "animal_03"));

            //�Ļ�
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "�Ļ�",
            //    processItemCost = 10,
            //    processItemImage = bread,
            //    processItemId = "bread_01"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("�Ļ�", 5, bread, "bread_01"));

            //�ٰ�Ʈ
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "�ٰ�Ʈ",
            //    processItemCost = 10,
            //    processItemImage = bagutte,
            //    processItemId = "bread_02"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("�ٰ�Ʈ", 10, bagutte, "bread_02"));

            //ũ��ͻ�
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "ũ��ͻ�",
            //    processItemCost = 10,
            //    processItemImage = croissant,
            //    processItemId = "bread_03"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("ũ��ͻ�", 25, croissant, "bread_03"));

            //�а���
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "�а���",
            //    processItemCost = 10,
            //    processItemImage = flour,
            //    processItemId = "windmill_01"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("�а���", 3, flour, "windmill_01"));

            //�� ���
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "�� ���",
            //    processItemCost = 10,
            //    processItemImage = chickenfeed,
            //    processItemId = "windmill_02"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("�� ���", 5, chickenfeed, "windmill_02"));

            //���� ���
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "���� ���",
            //    processItemCost = 10,
            //    processItemImage = pigfeed,
            //    processItemId = "windmill_03"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("���� ���", 5, pigfeed, "windmill_03"));

            //�� ���
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "�� ���",
            //    processItemCost = 10,
            //    processItemImage = cowfeed,
            //    processItemId = "windmill_04"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("�� ���", 5, cowfeed, "windmill_04"));

            //����Ķ���
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "����Ķ���",
            //    processItemCost = 10,
            //    processItemImage = eggflower,
            //    processItemId = "grill_01"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("����Ķ���", 10, eggflower, "grill_01"));

            //������
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "������",
            //    processItemCost = 10,
            //    processItemImage = bacon,
            //    processItemId = "grill_02"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("������", 10, bacon, "grill_02"));

            //�丶�� �꽺
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "�丶�� �꽺",
            //    processItemCost = 10,
            //    processItemImage = tomatojuice,
            //    processItemId = "juice_01"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("�丶�� �꽺", 15, tomatojuice, "juice_01"));

            //��� �꽺
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "��� �꽺",
            //    processItemCost = 10,
            //    processItemImage = carrotjuice,
            //    processItemId = "juice_02"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("��� �꽺", 15, carrotjuice, "juice_02"));

            //����
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "����",
            //    processItemCost = 10,
            //    processItemImage = butter,
            //    processItemId = "dairy_01"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("����", 25, butter, "dairy_01"));

            //ġ��
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "ġ��",
            //    processItemCost = 10,
            //    processItemImage = cheese,
            //    processItemId = "dairy_02"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("ġ��", 25, cheese, "dairy_02"));
        }
    }
}

