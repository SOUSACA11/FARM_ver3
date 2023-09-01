using UnityEngine;
using System.Collections.Generic;

//by.J:230725 ���� ���
//by.J:230814 IItem ������ ���� �۾�
namespace JinnyAnimal
{
    //Ÿ�� ����
    public enum AnimalType
    {
        None,
        CageChichen,
        CageCow,
        CagePig
    }

    //��� ���� Ÿ�� ����
    public enum GrowthAnimalType
    {
        Baby,  //����
        Child, //�ڶ�
        Adult, //�ڶ�2
        Man    //�ϼ�
    }

    //����ü ����
    [System.Serializable]
    public struct AnimalDataInfo
    {
        public string animalName;          //�̸�
        public int animalCost;             //����
        public float animalGrowTime;         //��� ���� �ð�
        public Sprite[] animalImage;       //���� ��� �̹���
        public AnimalType animalType;      //��� Ÿ��
        public string animalItemId;        //������ ���� ID
        public string animalProcessItemId; //���� ����X ���� ID

        public GameObject animalPrefab;//���� ������

    }

    //IItem �������̽� ����
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

        //���۽� �ʱ�ȭ ��� ����
        private void Start()
        {
            InitializeCropItems();
            Debug.Log("���� ����Ʈ ũ�� : " + animalDataList.Count);
        }

        //�ʱ�ȭ ���
        private void InitializeCropItems()
        {
            //�̹��� �߰�
            Sprite[] sprites = Resources.LoadAll<Sprite>("Cage");
            
            Sprite[] ChickenCage =
            {
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_2")), //��1
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_5")), //��2
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_8")), //��3
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_11")),//��4
            };

            Sprite[] CowCage =
            {
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_1")), //��1
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_4")), //��2
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_7")), //��3
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_10")), //��4
            };

            Sprite[] PigCage =
            {
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_0")), //����1
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_3")), //����2
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_6")), //����3
                System.Array.Find(sprites, sprite => sprite.name.Equals("Cage_9")), //����4
            };

            //��
            animalDataList.Add(new AnimalDataInfo()
            {
                animalType = AnimalType.CageChichen,
                animalName = "����",
                animalCost = 5,
                animalGrowTime = 20f,
                animalImage = ChickenCage,
                animalItemId = "cage_01",
                animalProcessItemId = "animal_02"
            });

            //��
            animalDataList.Add(new AnimalDataInfo()
            {
                animalType = AnimalType.CageCow,
                animalName = "���� ���",
                animalCost = 10,
                animalGrowTime = 15f,
                animalImage = CowCage,
                animalItemId = "cage_02",
                animalProcessItemId = "animal_01"
            });

            //����
            animalDataList.Add(new AnimalDataInfo()
            {
                animalType = AnimalType.CagePig,
                animalName = "���� ����",
                animalCost = 15,
                animalGrowTime = 20f,
                animalImage = PigCage,
                animalItemId = "cage_03",
                animalProcessItemId = "animal_0"
            });


        }
    }
}

