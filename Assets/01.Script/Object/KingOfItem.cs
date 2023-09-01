using System.Collections.Generic;
using UnityEngine;
using System.Linq; //(Language INtegrated Query) 컬렉션 형태의 데이터 가공시 도움 메서드 제공
using JinnyProcessItem;
using JinnyCropItem;

//by.J:230808 ProcessItem, CropItem 총괄자
public class KingOfItem : MonoBehaviour
{
    //아이템들을 저장하는 리스트
    public List<IItem> items = new List<IItem>();

    //아이템 반환
    public List<IItem> GetAllItems()
    {
        return items;
    }

    private void Start()
    {
        //Scene에 있는 모든 IItem을 찾아서 리스트에 추가
        //LINQ의 OfType() 특정 타입의 데이터만 추출
        foreach (IItem item in FindObjectsOfType<MonoBehaviour>().OfType<IItem>())
        {
            items.Add(item);
        }

        //모든 아이템의 정보를 출력
        foreach (IItem item in items)
        {
            Debug.Log("Item names: " + string.Join(", ", item.ItemName));
            Debug.Log("Item costs: " + string.Join(", ", item.ItemCost));
        }
    }
}
