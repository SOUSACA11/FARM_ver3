using UnityEngine;
using System.Collections.Generic;
using System;

//by.J:230720 생산품 (가공품 / 동물) 오브젝트 
//by.J:230721 List 변경화
//by.J:230728 이미지 추가 작업
//by.J:230814 IItem 수정에 따른 작업
//by.J:230818 싱글톤 추가 (완성품 이미지 접근 위해)
namespace JinnyProcessItem
{
    //구조체 정의
    [System.Serializable]
    public struct ProcessItemDataInfo : IItem, IEquatable<ProcessItemDataInfo>
    {
        public bool IsInitialized; //초기화 상태를 확인하기 위한 필드

        public string processItemName; //이름
        public int processItemCost;    //가격
        public Sprite processItemImage;//생산품 이미지
        public string processItemId;   //아이템 고유 ID

        public ProcessItemDataInfo(string name, int cost, Sprite image, string itemId)
        {
            this.processItemName = name;
            this.processItemCost = cost;
            this.processItemImage = image;
            this.processItemId = itemId;
            this.IsInitialized = true;
        }

        //IItem 인터페이스 구현 부분
        public string ItemName => processItemName;
        public int ItemCost => processItemCost;
        public Sprite ItemImage => processItemImage;
        public string ItemId => processItemId;

        //주어진 객체가 이 구조체와 같은지 확인하는 메서드
        public override bool Equals(object obj)
        {
            return obj is ProcessItemDataInfo && Equals((ProcessItemDataInfo)obj);
        }
        //다른 ProcessItemDataInfo 객체와 같은지 확인하기 위한 메서드. 아이템ID 기반으로 비교
        public bool Equals(ProcessItemDataInfo other)
        {
            return ItemId == other.ItemId;
        }
        //GetHashCode 메서드 오버라이드. 아이템ID 기반으로 해시 코드 생성
        public override int GetHashCode()
        {
            return ItemId.GetHashCode();
        }
    }

    //IItem 인터페이스 정의
    public class ProcessItem : MonoBehaviour //, IItem
    {
        public static ProcessItem Instance; //싱글톤

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

            InitializeProcessItems();
            Debug.Log("가공 생산품 리스트 크기 : " + processItemDataInfoList.Count);
        }

        //초기화 기능
        private void InitializeProcessItems()
        {
            //이미지 추가
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

            //우유
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "우유",
            //    processItemCost = 10,
            //    processItemImage = milk,
            //    processItemId = "animal_01"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("우유", 10, milk, "animal_01"));
           

            //달걀
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "달걀",
            //    processItemCost = 10,
            //    processItemImage = egg,
            //    processItemId = "animal_02"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("달걀", 10, egg, "animal_02"));

            //돼지고기
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "돼지고기",
            //    processItemCost = 10,
            //    processItemImage = pork,
            //    processItemId = "animal_03"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("돼지고기", 10, pork, "animal_03"));

            //식빵
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "식빵",
            //    processItemCost = 10,
            //    processItemImage = bread,
            //    processItemId = "bread_01"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("식빵", 5, bread, "bread_01"));

            //바게트
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "바게트",
            //    processItemCost = 10,
            //    processItemImage = bagutte,
            //    processItemId = "bread_02"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("바게트", 10, bagutte, "bread_02"));

            //크루와상
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "크루와상",
            //    processItemCost = 10,
            //    processItemImage = croissant,
            //    processItemId = "bread_03"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("크루와상", 25, croissant, "bread_03"));

            //밀가루
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "밀가루",
            //    processItemCost = 10,
            //    processItemImage = flour,
            //    processItemId = "windmill_01"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("밀가루", 3, flour, "windmill_01"));

            //닭 사료
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "닭 사료",
            //    processItemCost = 10,
            //    processItemImage = chickenfeed,
            //    processItemId = "windmill_02"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("닭 사료", 5, chickenfeed, "windmill_02"));

            //돼지 사료
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "돼지 사료",
            //    processItemCost = 10,
            //    processItemImage = pigfeed,
            //    processItemId = "windmill_03"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("돼지 사료", 5, pigfeed, "windmill_03"));

            //소 사료
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "소 사료",
            //    processItemCost = 10,
            //    processItemImage = cowfeed,
            //    processItemId = "windmill_04"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("소 사료", 5, cowfeed, "windmill_04"));

            //계란후라이
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "계란후라이",
            //    processItemCost = 10,
            //    processItemImage = eggflower,
            //    processItemId = "grill_01"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("계란후라이", 10, eggflower, "grill_01"));

            //베이컨
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "베이컨",
            //    processItemCost = 10,
            //    processItemImage = bacon,
            //    processItemId = "grill_02"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("베이컨", 10, bacon, "grill_02"));

            //토마토 쥬스
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "토마토 쥬스",
            //    processItemCost = 10,
            //    processItemImage = tomatojuice,
            //    processItemId = "juice_01"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("토마토 쥬스", 15, tomatojuice, "juice_01"));

            //당근 쥬스
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "당근 쥬스",
            //    processItemCost = 10,
            //    processItemImage = carrotjuice,
            //    processItemId = "juice_02"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("당근 쥬스", 15, carrotjuice, "juice_02"));

            //버터
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "버터",
            //    processItemCost = 10,
            //    processItemImage = butter,
            //    processItemId = "dairy_01"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("버터", 25, butter, "dairy_01"));

            //치즈
            //processItemDataInfoList.Add(new ProcessItemDataInfo()
            //{
            //    //IsInitialized = true,
            //    processItemName = "치즈",
            //    processItemCost = 10,
            //    processItemImage = cheese,
            //    processItemId = "dairy_02"
            //});
            processItemDataInfoList.Add(new ProcessItemDataInfo("치즈", 25, cheese, "dairy_02"));
        }
    }
}

