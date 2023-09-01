using UnityEngine;
using System.Collections.Generic;
using System;

//by.J:230720 생산품 (작물) 오브젝트
//by.J:230721 List 변경화
//by.J:230728 이미지 추가 작업
//by.J:230814 IItem 수정에 따른 작업
//by.J:230818 싱글톤 추가 (완성품 이미지 접근 위해)
namespace JinnyCropItem
{
    //구조체 정의
    [System.Serializable]
    public struct CropItemDataInfo : IItem, IEquatable<CropItemDataInfo>
    {
        public bool IsInitialized { get; private set; }  //초기화 상태를 확인하기 위한 필드

        public string cropItemName;   //이름
        public int cropItemCost;      //가격
        public Sprite cropItemImage;  //생산품 이미지
        public string cropItemId;     //아이템 고유 ID

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

    //IItem 인터페이스 정의
    public class CropItem : MonoBehaviour //IItem
    {
        public static CropItem Instance; //싱글톤

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

        //시작시 초기화 기능 시작
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

            Debug.Log("농장 생산품 리스트 크기 : " + cropItemDataInfoList.Count);



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

        //초기화 기능
        private void InitializeCropItems()
        {
            //이미지 추가
            Sprite[] sprites = Resources.LoadAll<Sprite>("Item");
            Sprite wheat = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_3"));
            Sprite corn = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_4"));
            Sprite bean = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_5"));
            Sprite tomato = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_6"));
            Sprite carrot = System.Array.Find(sprites, sprite => sprite.name.Equals("Item_7"));



            //밀
            //cropItemDataInfoList.Add(new CropItemDataInfo()
            //{
            //    IsInitialized = true,
            //    cropItemName = "밀",
            //    cropItemCost = 10,
            //    cropItemImage = wheat,
            //    cropItemId = "crop_01"

            //});
            cropItemDataInfoList.Add(new CropItemDataInfo("밀", 3, wheat, "crop_01"));

            //옥수수
            //cropItemDataInfoList.Add(new CropItemDataInfo()
            //{
            //    IsInitialized = true,
            //    cropItemName = "옥수수",
            //    cropItemCost = 10,
            //    cropItemImage = corn,
            //    cropItemId = "crop_02"
            //});
            cropItemDataInfoList.Add(new CropItemDataInfo("옥수수", 5, corn, "crop_02"));

            //콩
            //cropItemDataInfoList.Add(new CropItemDataInfo()
            //{
            //    IsInitialized = true,
            //    cropItemName = "콩",
            //    cropItemCost = 10,
            //    cropItemImage = bean,
            //    cropItemId = "crop_03"
            //});
            cropItemDataInfoList.Add(new CropItemDataInfo("콩", 10, bean, "crop_03"));

            //토마토
            //cropItemDataInfoList.Add(new CropItemDataInfo()
            //{
            //    IsInitialized = true,
            //    cropItemName = "토마토",
            //    cropItemCost = 10,
            //    cropItemImage = tomato,
            //    cropItemId = "crop_04"
            //});
            cropItemDataInfoList.Add(new CropItemDataInfo("토마토", 15, tomato, "crop_04"));

            //당근
            //cropItemDataInfoList.Add(new CropItemDataInfo()
            //{
            //    IsInitialized = true,
            //    cropItemName = "당근",
            //    cropItemCost = 10,
            //    cropItemImage = carrot,
            //    cropItemId = "crop_05"
            //});
            cropItemDataInfoList.Add(new CropItemDataInfo("당근", 20, carrot, "crop_05"));

        }
    }
}
