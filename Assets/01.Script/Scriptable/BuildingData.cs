using UnityEngine;
using System.Collections.Generic;

//by.J:230719 건물 오브젝트 스크립터블 / 건물정보 저장
//by.J:230720 List 변경화
namespace JinnyBuildingData
{
    [CreateAssetMenu(fileName = "NewBuilding", menuName = "Building")]

    public class BuildingData : ScriptableObject
    {
        public List<BuildingInfo> buildings;

        [System.Serializable]
        public struct BuildingInfo
        {
            public string buildingName;     //이름
            public int buildingCost;        //가격
            public float buildingBuildTime; //건축 시간
        }
    }
}
