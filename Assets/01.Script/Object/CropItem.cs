using UnityEngine;
using System.Collections.Generic;
using System;

//by.J:230720 ����ǰ (�۹�) ������Ʈ
//by.J:230721 List ����ȭ
//by.J:230728 �̹��� �߰� �۾�
//by.J:230814 IItem ������ ���� �۾�
//by.J:230818 �̱��� �߰� (�ϼ�ǰ �̹��� ���� ����)
namespace JinnyCropItem
{
    //����ü ����
    [System.Serializable]
    public struct CropItemDataInfo : IItem, IEquatable<CropItemDataInfo>
    {
        public bool IsInitialized { get; private set; }  //�ʱ�ȭ ���¸� Ȯ���ϱ� ���� �ʵ�

        public string cropItemName;   //�̸�
        public int cropItemCost;      //����
        public Sprite cropItemImage;  //����ǰ �̹���
        public string cropItemId;     //������ ���� ID

        public CropItemDataInfo(string name, int cost, Sprite image, string itemId)
        {
            this.cropItemName = name;
            this.cropItemCost = cost;
            this.cropItemImage = image;
            this.cropItemId = itemId;
            this.IsInitialized = true;
        }



        public string ItemName => cropItemName;
        public int ItemCost => cropItemCost;
        public Sprite ItemImage => cropItemImage;
        public string ItemId => cropItemId;

        public override bool Equals(object obj)
        {
            return obj is CropItemDataInfo && Equals((CropItemDataInfo)obj);
        }

        public bool Equals(CropItemDataInfo other)
        {
            return ItemId == other.ItemId;
        }

        public override int GetHashCode()
        {
            return ItemId.GetHashCode();
        }

    }

    //IItem �������̽� ����
    public class CropItem : MonoBehaviour //IItem
    {
        public static CropItem Instance; //�̱���

        [SerializeField] public List<CropItemDataInfo> cropItemDataInfoList = new List<CropItemDataInfo>();

        //public string[] ItemName
        //{
        //    get
        //    {
        //        string[] names = new string[cropItemDataInfoList.Count];
        //        for (int i = 0; i < cropItemDataInfoList.Count; i++)
        //        {
        //            names[i] = cropItemDataInfoList[i].cropItemName;
        //        }
        //        return names;
        //    }

        //}

        //public int[] ItemCost
        //{
        //    get
        //    {
        //        int[] costs = new int[cropItemDataInfoList.Count];
        //        for (int i = 0; i < cropItemDataInfoList.Count; i++)
        //        {
        //            costs[i] = cropItemDataInfoList[i].cropItemCost;
        //        }
        //        return costs;
        //    }

        //}

        //public Sprite[] ItemImage
        //{
        //    get
        //    {
        //        Sprite[] images = new Sprite[cropItemDataInfoList.Count];
        //        for (int i = 0; i < cropItemDataInfoList.Count; i++)
        //        {
        //            images[i] = cropItemDataInfoList[i].cropItemImage;
        //        }
        //        return images;
        //    }
        //}

        //public string[] ItemId
        //{
        //    get
        //    {
        //        string[] names = new string[cropItemDataInfoList.Count];
        //        for (int i = 0; i < cropItemDataInfoList.Count; i++)
        //        {
        //            names[i] = cropItemDataInfoList[i].cropItemId;
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

            InitializeCropItems();

            Debug.Log("���� ����ǰ ����Ʈ ũ�� : " + cropItemDataInfoList.Count);



            //foreach (var item in cropItemDataInfoList)
            //{
            //    //Debug.Log("Item Name: " + item.cropItemName + ", Image: " + item.cropItemImage);
            //    //Debug.Log("Item ID: " + item.cropItemId);
            //}

            //Sprite[] sprites = Resources.LoadAll<Sprite>("Item");
            //Debug.Log("Loaded " + sprites.Length + " sprites from 'Item' folder.");
            //foreach (Sprite sprite in sprites)
            //{
            //    //Debug.Log("Loaded sprite name: " + sprite.name);
            //}
        }

        //�ʱ�ȭ ���
        private void InitializeCropItems()
        {
            //�̹��� �߰�
            Sprite[] sprites = Resources.LoadAll<Sprite>("Item");
            Sprite wheat = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_3"));
            Sprite corn = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_4"));
            Sprite bean = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_5"));
            Sprite tomato = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_6"));
            Sprite carrot = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_7"));



            //��
            //cropItemDataInfoList.Add(new CropItemDataInfo()
            //{
            //    IsInitialized = true,
            //    cropItemName = "��",
            //    cropItemCost = 10,
            //    cropItemImage = wheat,
            //    cropItemId = "crop_01"

            //});
            cropItemDataInfoList.Add(new CropItemDataInfo("��", 3, wheat, "crop_01"));

            //������
            //cropItemDataInfoList.Add(new CropItemDataInfo()
            //{
            //    IsInitialized = true,
            //    cropItemName = "������",
            //    cropItemCost = 10,
            //    cropItemImage = corn,
            //    cropItemId = "crop_02"
            //});
            cropItemDataInfoList.Add(new CropItemDataInfo("������", 5, corn, "crop_02"));

            //��
            //cropItemDataInfoList.Add(new CropItemDataInfo()
            //{
            //    IsInitialized = true,
            //    cropItemName = "��",
            //    cropItemCost = 10,
            //    cropItemImage = bean,
            //    cropItemId = "crop_03"
            //});
            cropItemDataInfoList.Add(new CropItemDataInfo("��", 10, bean, "crop_03"));

            //�丶��
            //cropItemDataInfoList.Add(new CropItemDataInfo()
            //{
            //    IsInitialized = true,
            //    cropItemName = "�丶��",
            //    cropItemCost = 10,
            //    cropItemImage = tomato,
            //    cropItemId = "crop_04"
            //});
            cropItemDataInfoList.Add(new CropItemDataInfo("�丶��", 15, tomato, "crop_04"));

            //���
            //cropItemDataInfoList.Add(new CropItemDataInfo()
            //{
            //    IsInitialized = true,
            //    cropItemName = "���",
            //    cropItemCost = 10,
            //    cropItemImage = carrot,
            //    cropItemId = "crop_05"
            //});
            cropItemDataInfoList.Add(new CropItemDataInfo("���", 20, carrot, "crop_05"));

        }
    }
}
