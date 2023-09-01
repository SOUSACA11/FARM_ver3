using UnityEngine;
using System.Collections.Generic;

//by.J:230719 �ǹ� ������Ʈ ��ũ���ͺ� / �ǹ����� ����
//by.J:230720 List ����ȭ
namespace JinnyBuildingData
{
    [CreateAssetMenu(fileName = "NewBuilding", menuName = "Building")]

    public class BuildingData : ScriptableObject
    {
        public List<BuildingInfo> buildings;

        [System.Serializable]
        public struct BuildingInfo
        {
            public string buildingName;     //�̸�
            public int buildingCost;        //����
            public float buildingBuildTime; //���� �ð�
        }
    }
}
