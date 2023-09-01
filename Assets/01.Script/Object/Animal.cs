using UnityEngine;
using System.Collections.Generic;

//by.J:230725 동물 축사
//by.J:230814 IItem 수정에 따른 작업
namespace JinnyAnimal
{
    //타입 정의
    public enum AnimalType
    {
        None,
        CageChichen,
        CageCow,
        CagePig
    }

    //축사 성장 타입 정의
    public enum GrowthAnimalType
    {
        Baby,  //생성
        Child, //자람
        Adult, //자람2
        Man    //완성
    }

    //구조체 정의
    [System.Serializable]
    public struct AnimalDataInfo
    {
        public string animalName;          //이름
        public int animalCost;             //가격
        public float animalGrowTime;         //축사 성장 시간
        public Sprite[] animalImage;       //동물 축사 이미지
        public AnimalType animalType;      //축사 타입
        public string animalItemId;        //아이템 고유 ID
        public string animalProcessItemId; //동물 생산뭎 고유 ID

        public GameObject animalPrefab;//동물 프리팹

    }

    //IItem 인터페이스 정의
    public class Animal : MonoBehaviour //, IItem
    {
        [SerializeField] public List<AnimalDataInfo> animalDataList = new List<AnimalDataInfo>();

        //public string[] ItemName
        //{
        //    get
        //    {
        //        string[] names = new string[animalDataList.Count];
        //        for (int i = 0; i < animalDataList.Count; i++)
        //        {
        //            names[i] = animalDataList[i].animalName;
        //        }
        //        return names;
        //    }

        //}

        //public int[] ItemCost
        //{
        //    get
        //    {
        //        int[] costs = new int[animalDataList.Count];
        //        for (int i = 0; i < animalDataList.Count; i++)
        //        {
        //            costs[i] = animalDataList[i].animalCost;
        //        }
        //        return costs;
        //    }

        //}

        //public Sprite[] ItemImage
        //{
        //    get
        //    {
        //        Sprite[] images = new Sprite[animalDataList.Count];
        //        for (int i = 0; i < animalDataList.Count; i++)
        //        {
        //            images[i] = animalDataList[i].animalImage;
        //        }
        //        return images;
        //    }
        //}

        //public string[] ItemId
        //{
        //    get
        //    {
        //        string[] names = new string[animalDataList.Count];
        //        for (int i = 0; i < animalDataList.Count; i++)
        //        {
        //            names[i] = animalDataList[i].animalItemId;
        //        }
        //        return names;
        //    }
        //}

        //시작시 초기화 기능 시작
        private void Start()
        {
            InitializeCropItems();
            Debug.Log("동물 리스트 크기 : " + animalDataList.Count);
        }

        //초기화 기능
        private void InitializeCropItems()
        {
            //이미지 추가
            Sprite[] sprites = Resources.LoadAll<Sprite>("Cage");
            
            Sprite[] ChickenCage =
            {
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_2")), //닭1
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_5")), //닭2
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_8")), //닭3
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_11")),//닭4
            };

            Sprite[] CowCage =
            {
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_1")), //소1
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_4")), //소2
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_7")), //소3
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_10")), //소4
            };

            Sprite[] PigCage =
            {
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_0")), //돼지1
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_3")), //돼지2
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_6")), //돼지3
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_9")), //돼지4
            };

            //닭
            animalDataList.Add(new AnimalDataInfo()
            {
                animalType = AnimalType.CageChichen,
                animalName = "닭장",
                animalCost = 5,
                animalGrowTime = 20f,
                animalImage = ChickenCage,
                animalItemId = "cage_01",
                animalProcessItemId = "animal_02"
            });

            //소
            animalDataList.Add(new AnimalDataInfo()
            {
                animalType = AnimalType.CageCow,
                animalName = "젖소 축사",
                animalCost = 10,
                animalGrowTime = 15f,
                animalImage = CowCage,
                animalItemId = "cage_02",
                animalProcessItemId = "animal_01"
            });

            //돼지
            animalDataList.Add(new AnimalDataInfo()
            {
                animalType = AnimalType.CagePig,
                animalName = "돼지 농장",
                animalCost = 15,
                animalGrowTime = 20f,
                animalImage = PigCage,
                animalItemId = "cage_03",
                animalProcessItemId = "animal_0"
            });


        }
    }
}

