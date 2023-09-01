using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using JinnyProcessItem;
using JinnyCropItem;

//by,J:230728: 창고를 위한 인벤토리
namespace JinnyInventory
{
    public class Inventory : MonoBehaviour
    {
        //아이템 정보
        [System.Serializable]
        public class Item
        {
            public string id;    //아이템의 고유 ID
            public int quantity; //아이템의 수량
        }

        private Dictionary<string, Item> items = new Dictionary<string, Item>(); // ID를 키로 가지는 아이템 사전

        //아이템 추가
        public void AddItem(string id, int quantity)
        {
            if (items.ContainsKey(id)) //이미 해당 ID의 아이템이 있으면
            {
                items[id].quantity += quantity; //수량을 더함
            }
            else //해당 ID의 아이템이 없으면
            {
                items[id] = new Item { id = id, quantity = quantity }; //새 아이템을 생성
            }
        }

        //아이템 제거
        public bool RemoveItem(string id, int quantity)
        {
            if (!items.ContainsKey(id) || items[id].quantity < quantity) //아이템이 없거나, 수량이 부족하면
            {
                return false; //제거 실패
            }

            items[id].quantity -= quantity; //수량을 뺌

            if (items[id].quantity <= 0) //아이템의 수량이 0이하가 되면
            {
                items.Remove(id); //아이템을 제거
            }

            return true; //제거 성공
        }

        //아이템 수량
        public int GetQuantity(string id)
        {
            if (!items.ContainsKey(id)) //해당 ID의 아이템이 없으면
            {
                return 0; //0을 반환
            }

            return items[id].quantity; //아이템의 수량을 반환
        }

        //모든 아이템 가져오는
        public List<Item> GetAllItems()
        {
            return new List<Item>(items.Values);
        }
    }
}