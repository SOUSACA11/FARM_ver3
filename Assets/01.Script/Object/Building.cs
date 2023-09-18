using UnityEngine;
using System.Collections.Generic;
using System.Linq;

//by.J:230719 건물 오브젝트
// by.J:230720 List 변경화
//by.J:230724 이미지 추가 작업
namespace JinnyBuilding
{
    //타입 정의
    public enum BuildingType
    {
        None,
        Cage,       //축사
        Bakery,     //빵집
        Windmill,   //정미소
        GrillShop,  //철판가게
        Dairy,      //유제품가공소
        JuiceShop   //쥬스가게
    }

    //구조체 정의
    [System.Serializable]
    public struct BuildingDataInfo
    {
        public string buildingName;       //이름
        public int buildingCost;          //가격
        public float buildingBuildTime;   //건축 시간
        public Sprite buildingImage;      //건물 이미지
        public BuildingType buildingType; //건물 타입

        public GameObject buildingPrefab;//건물 프리팹


        //public bool isValid;
    }

    //IBuilding 인터페이스 정의
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


        //시작시 초기화 기능 시작
        private void Start() 
        {
            InitializeBuildings();
            Debug.Log("빌딩 리스트 크기 : " + buildingDataList.Count);

        }

        //초기화 기능
        private void InitializeBuildings()
        {
            //이미지 추가
           
            Sprite[] combinedSprites = Resources.LoadAll<Sprite>("Buid_1").Concat(Resources.LoadAll<Sprite>("222")).ToArray();
            Sprite bakery = System.Array.Find(combinedSprites, sprite => sprite.name.Equals("Buid_1_1"));
            Sprite GrillShop = System.Array.Find(combinedSprites, sprite => sprite.name.Equals("Buid_1_3"));
            Sprite Windmill = Resources.Load<Sprite>("Wi");
            Sprite Dairy = Resources.Load<Sprite>("Mi");
            Sprite JuiceShop = Resources.Load<Sprite>("Ju");


            //Sprite[] sprites = Resources.LoadAll<Sprite>("Buid_1");

            //Sprite bakery = System.Array.Find(sprites, sprite => sprite.name.Equals("Buid_1_1"));
            //Sprite GrillShop = System.Array.Find(sprites, sprite => sprite.name.Equals("Buid_1_3"));


            // 빵집
            buildingDataList.Add(new BuildingDataInfo()
            {
                buildingType = BuildingType.Bakery,
                buildingName = "빵집",
                buildingCost = 0,
                buildingBuildTime = 15f,
                buildingImage = bakery
            });

            // 축사
            //buildingDataList.Add(new BuildingDataInfo()
            //{
            //    buildingType = BuildingType.Cage,
            //    buildingName = "축사",
            //    buildingCost = 10,
            //    buildingBuildTime = 5.0f,
            //    buildingImage = buid_1_2
            //});

            // 정미소
            buildingDataList.Add(new BuildingDataInfo()
            {
                buildingType = BuildingType.Windmill,
                buildingName = "정미소",
                buildingCost = 3,
                buildingBuildTime =15f,
                buildingImage = Windmill
            });

            // 철판가게
            buildingDataList.Add(new BuildingDataInfo()
            {
                buildingType = BuildingType.GrillShop,
                buildingName = "철판 가게",
                buildingCost = 5,
                buildingBuildTime = 20f,
                buildingImage = GrillShop
            });

            //유제품 가공소
            buildingDataList.Add(new BuildingDataInfo()
            {
                buildingType = BuildingType.Dairy,
                buildingName = "유제품 가공소",
                buildingCost = 10,
                buildingBuildTime = 20f,
                buildingImage = Dairy
            });

            //쥬스가게
            buildingDataList.Add(new BuildingDataInfo()
            {
                buildingType = BuildingType.JuiceShop,
                buildingName = "쥬스 가게",
                buildingCost = 15,
                buildingBuildTime = 25f,
                buildingImage = JuiceShop
            });


        }

       
    }
}
