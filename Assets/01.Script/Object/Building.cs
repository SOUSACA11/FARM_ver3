using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//by.J:230719 �ǹ� ������Ʈ
// by.J:230720 List ����ȭ
//by.J:230724 �̹��� �߰� �۾�
namespace JinnyBuilding
{
    //Ÿ�� ����
    public enum BuildingType
    {
        None,
        Cage,       //���
        Bakery,     //����
        Windmill,   //���̼�
        GrillShop,  //ö�ǰ���
        Dairy,      //����ǰ������
        JuiceShop   //�꽺����
    }

    //����ü ����
    [System.Serializable]
    public struct BuildingDataInfo
    {
        public string buildingName;       //�̸�
        public int buildingCost;          //����
        public float buildingBuildTime;   //���� �ð�
        public Sprite buildingImage;      //�ǹ� �̹���
        public BuildingType buildingType; //�ǹ� Ÿ��

        public GameObject buildingPrefab;//�ǹ� ������


        //public bool isValid;
    }

    //IBuilding �������̽� ����
    public class Building : MonoBehaviour, IBuilding
    {
        [SerializeField] public List<BuildingDataInfo> buildingDataList = new List<BuildingDataInfo>();

        public string[] BuildingName
        {
            get
            {
                string[] names = new string[buildingDataList.Count];
                for (int i = 0; i < buildingDataList.Count; i++)
                {
                    names[i] = buildingDataList[i].buildingName;
                }
                return names;
            }
            
        }

        public int[] Buildingcost
        {
            get
            {
                int[] costs = new int[buildingDataList.Count];
                for (int i = 0; i < buildingDataList.Count; i++)
                {
                    costs[i] = buildingDataList[i].buildingCost;
                }
                return costs;
            }
           
        }

        public float[] BuildingBuildTime
        {
            get
            {
                float[] buildTimes = new float[buildingDataList.Count];
                for (int i = 0; i < buildingDataList.Count; i++)
                {
                    buildTimes[i] = buildingDataList[i].buildingBuildTime;
                }
                return buildTimes;
            }
          
        }


        //���۽� �ʱ�ȭ ��� ����
        private void Start() 
        {
            InitializeBuildings();
            Debug.Log("���� ����Ʈ ũ�� : " + buildingDataList.Count);

        }

        //�ʱ�ȭ ���
        private void InitializeBuildings()
        {
            //�̹��� �߰�
           
            Sprite[] combinedSprites = Resources.LoadAll<Sprite>("Buid_1").Concat(Resources.LoadAll<Sprite>("222")).ToArray();
            Sprite bakery = System.Array.Find(combinedSprites, sprite => sprite.name.Equals("Buid_1_1"));
            Sprite GrillShop = System.Array.Find(combinedSprites, sprite => sprite.name.Equals("Buid_1_3"));
            Sprite Windmill = Resources.Load<Sprite>("Wi");
            Sprite Dairy = Resources.Load<Sprite>("Mi");
            Sprite JuiceShop = Resources.Load<Sprite>("Ju");


            //Sprite[] sprites = Resources.LoadAll<Sprite>("Buid_1");

            //Sprite bakery = System.Array.Find(sprites, sprite => sprite.name.Equals("Buid_1_1"));
            //Sprite GrillShop = System.Array.Find(sprites, sprite => sprite.name.Equals("Buid_1_3"));


            // ����
            buildingDataList.Add(new BuildingDataInfo()
            {
                buildingType = BuildingType.Bakery,
                buildingName = "����",
                buildingCost = 0,
                buildingBuildTime = 15f,
                buildingImage = bakery
            });

            // ���
            //buildingDataList.Add(new BuildingDataInfo()
            //{
            //    buildingType = BuildingType.Cage,
            //    buildingName = "���",
            //    buildingCost = 10,
            //    buildingBuildTime = 5.0f,
            //    buildingImage = buid_1_2
            //});

            // ���̼�
            buildingDataList.Add(new BuildingDataInfo()
            {
                buildingType = BuildingType.Windmill,
                buildingName = "���̼�",
                buildingCost = 3,
                buildingBuildTime =15f,
                buildingImage = Windmill
            });

            // ö�ǰ���
            buildingDataList.Add(new BuildingDataInfo()
            {
                buildingType = BuildingType.GrillShop,
                buildingName = "ö�� ����",
                buildingCost = 5,
                buildingBuildTime = 20f,
                buildingImage = GrillShop
            });

            //����ǰ ������
            buildingDataList.Add(new BuildingDataInfo()
            {
                buildingType = BuildingType.Dairy,
                buildingName = "����ǰ ������",
                buildingCost = 10,
                buildingBuildTime = 20f,
                buildingImage = Dairy
            });

            //�꽺����
            buildingDataList.Add(new BuildingDataInfo()
            {
                buildingType = BuildingType.JuiceShop,
                buildingName = "�꽺 ����",
                buildingCost = 15,
                buildingBuildTime = 25f,
                buildingImage = JuiceShop
            });


        }

       
    }
}
