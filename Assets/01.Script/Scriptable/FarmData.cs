using UnityEngine;
using System.Collections.Generic;

//by.J:230720 ����� ������Ʈ ��ũ���ͺ� / ����� ���� ����
//by.J:230721 List ����ȭ
namespace JinnyFarmData
{
    [CreateAssetMenu(fileName = "NewFarm", menuName = "Farm")]

    public class FarmData : ScriptableObject
    {
        public List<FarmInfo> farms;

        [System.Serializable]

        public struct FarmInfo
        {
            public string farmName;     //�̸�
            public int farmCost;        //����
            public int farmHaverst;     //�۹� ��Ȯ��
            public float farmGrowTime;  //�۹� ���� �ð�
        }
    }
}