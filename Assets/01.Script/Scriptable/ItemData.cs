using UnityEngine;
using System.Collections.Generic;


//by.J:230720 ����ǰ ������Ʈ ��ũ���ͺ� / ����ǰ ���� ����
//by.J:230721 List ����ȭ
namespace JinnyItemData
{
    [CreateAssetMenu(fileName = "NewItem", menuName = "Item")]

    public class ItemData : ScriptableObject
    {
        public List<ItemInfo> items;

        [System.Serializable]

        public struct ItemInfo
        {
            public string itemName;     //�̸�
            public int itemCost;        //����




            //public ItemType itemType;   //������ Ÿ��
            //public Sprite itemImage;    //������ �̹���
        }



        //public enum ItemType  // ������ ����
        //{
        //    Process,
        //    Crop
        //}
    }
}
