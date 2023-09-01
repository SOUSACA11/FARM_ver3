using UnityEngine;
using System.Collections.Generic;
using JinnyCropItem;

//by.J:230720 농장밭 오브젝트 
//by.J:230721 List 변경화
namespace JinnyFarm
{
    //타입 정의
    public enum FarmType
    {
        None,
        Farm
    }

    //농장밭 성장 타입 정의
    public enum GrowthFarmType
    {
        Plant,  //심음
        Growth, //자람
        Born    //탄생
    }

    //구조체 정의
    [System.Serializable]
    public struct FarmDataInfo
    {
        public string farmName;     //이름
        public int farmCost;        //가격
        public int farmHaverst;     //작물 수확량 
        public float farmGrowTime;  //작물 성장 시간
        public Sprite[] farmImage;  //농장밭 이미지
        public FarmType farmType;   //농장 타입
        public string farmItemId;   //농장밭 고유 ID
        public string cropItemId;   //작물 고유 ID

        public GameObject farmPrefab;//농장밭 프리팹
    }

    //IFarm 인터페이스 정의
    public class Farm : MonoBehaviour, IFarm
    {
        [SerializeField] public List<FarmDataInfo> farmDataList = new List<FarmDataInfo>();

        public string[] FarmName
        {
            get
            {
                string[] names = new string[farmDataList.Count];
                for (int i = 0; i < farmDataList.Count; i++)
                {
                    names[i] = farmDataList[i].farmName;
                }
                return names;
            }
            
        }

        public int[] FarmCost
        {
            get
            {
                int[] costs = new int[farmDataList.Count];
                for (int i = 0; i < farmDataList.Count; i++)
                {
                    costs[i] = farmDataList[i].farmCost;
                }
                return costs;
            }
        }

        public int[] FarmHaverst
        {
            get
            {
                int[] haversts = new int[farmDataList.Count];
                for (int i = 0; i <farmDataList.Count; i++)
                {
                    haversts[i] = farmDataList[i].farmHaverst;
                }
                return haversts;
            }
        }

        public float[] FarmGrowTime
        {
            get
            {
                float[] growTimes = new float[farmDataList.Count];
                for (int i = 0; i < farmDataList.Count; i++)
                {
                    growTimes[i] = farmDataList[i].farmGrowTime;
                }
                return growTimes;
            }

        }

        //시작시 초기화 기능 시작
        private void Start()
        {
            InitializeFarms();
            Debug.Log("농장밭 리스트 크기 : " + farmDataList.Count);
        }


        //초기화 기능
        private void InitializeFarms()
        {
            //이미지 추가
            Sprite[] sprites = Resources.LoadAll<Sprite>("FarmCrop");

            Sprite[] wheat =
            {
                System.Array.Find(sprites, sprite => sprite.name.Equals("FarmCrop_3")), //밀1
                System.Array.Find(sprites, sprite => sprite.name.Equals("FarmCrop_1")), //밀2
                System.Array.Find(sprites, sprite => sprite.name.Equals("FarmCrop_2")), //밀3
            };
            Sprite[] carrot =
            {
                System.Array.Find(sprites, sprite => sprite.name.Equals("FarmCrop_4")), //당근1
                System.Array.Find(sprites, sprite => sprite.name.Equals("FarmCrop_5")), //당근2
                System.Array.Find(sprites, sprite => sprite.name.Equals("FarmCrop_6")), //당근3
            };
            Sprite[] bean =
            {
                System.Array.Find(sprites, sprite => sprite.name.Equals("FarmCrop_7")), //콩1
                System.Array.Find(sprites, sprite => sprite.name.Equals("FarmCrop_8")), //콩2
                System.Array.Find(sprites, sprite => sprite.name.Equals("FarmCrop_9")), //콩3
            };
            Sprite[] tomato =
            {
                System.Array.Find(sprites, sprite => sprite.name.Equals("FarmCrop_14")), //토마토1
                System.Array.Find(sprites, sprite => sprite.name.Equals("FarmCrop_11")), //토마토2
                System.Array.Find(sprites, sprite => sprite.name.Equals("FarmCrop_12")), //토마토3
            };
            Sprite[] corn =
            {
                System.Array.Find(sprites, sprite => sprite.name.Equals("FarmCrop_15")), //옥수수1
                System.Array.Find(sprites, sprite => sprite.name.Equals("FarmCrop_16")), //옥수수2
                System.Array.Find(sprites, sprite => sprite.name.Equals("FarmCrop_17")), //옥수수3
            };

            //밀밭
            farmDataList.Add(new FarmDataInfo()
            {
                farmType = FarmType.Farm,
                farmName = "밀밭",
                farmCost = 0,
                farmHaverst = 3,
                farmGrowTime = 10f,
                farmImage = wheat,
                farmItemId = "farm_01",
                cropItemId = "crop_01"
            });

            //옥수수밭
            farmDataList.Add(new FarmDataInfo()
            {
                farmType = FarmType.Farm,
                farmName = "옥수수밭",
                farmCost = 3,
                farmHaverst = 3,
                farmGrowTime = 10f,
                farmImage = corn,
                farmItemId = "farm_02",
                cropItemId = "crop_02"
            });

            //콩밭
            farmDataList.Add(new FarmDataInfo()
            {
                farmType = FarmType.Farm,
                farmName = "콩밭",
                farmCost = 5,
                farmHaverst = 3,
                farmGrowTime = 15f,
                farmImage = bean,
                farmItemId = "farm_03",
                cropItemId = "crop_03"
            });

            //토마토밭
            farmDataList.Add(new FarmDataInfo()
            {
                farmType = FarmType.Farm,
                farmName = "토마토밭",
                farmCost = 10,
                farmHaverst = 3,
                farmGrowTime = 20f,
                farmImage = tomato,
                farmItemId = "farm_04",
                cropItemId = "crop_04"
            });

            //당근밭
            farmDataList.Add(new FarmDataInfo()
            {
                farmType = FarmType.Farm,
                farmName = "당근밭",
                farmCost = 15,
                farmHaverst = 3,
                farmGrowTime = 25f,
                farmImage = carrot,
                farmItemId = "farm_05",
                cropItemId = "crop_05"
            });


        }
    }
}

