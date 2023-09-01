using UnityEngine;
using JinnyProcessItem;

//by.J:230814 래퍼클래스 / ProcessItemDataInfo 데이터를 IItem 맞게 포장
//by,j:230829 ProcessItemIItem와 ProcessItemDataInfo 같은 메모리 사용을 위한 추가 작업
public class ProcessItemIItem : IItem
{
    private ProcessItemDataInfo _info;
    //_info -> 내부적으로 참조하고 있는 원래 데이터 객체 / ProcessItemDataInfo타입의 인스턴스 변수

    public ProcessItemIItem(ProcessItemDataInfo info)
    {
        _info = info;
    }

    public string ItemName => _info.processItemName;
    public int ItemCost => _info.processItemCost;
    public Sprite ItemImage => _info.processItemImage;
    public string ItemId => _info.processItemId;

    public override bool Equals(object obj)
    {
        if (obj is ProcessItemIItem other)
        {
            return this.ItemId == other.ItemId;
        }
        return false;
    }

    public override int GetHashCode()
    {
        return this.ItemId.GetHashCode();
    }
}
