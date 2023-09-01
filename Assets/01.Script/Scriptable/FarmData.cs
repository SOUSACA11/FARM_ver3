using UnityEngine;
using System.Collections.Generic;

//by.J:230720 농장밭 오브젝트 스크립터블 / 농장밭 정보 저장
//by.J:230721 List 변경화
namespace JinnyFarmData
{
    [CreateAssetMenu(fileName = "NewFarm", menuName = "Farm")]

    public class FarmData : ScriptableObject
    {
        public List<FarmInfo> farms;

        [System.Serializable]

        public struct FarmInfo
        {
            public string farmName;     //이름
            public int farmCost;        //가격
            public int farmHaverst;     //작물 수확량
            public float farmGrowTime;  //작물 성장 시간
        }
    }
}