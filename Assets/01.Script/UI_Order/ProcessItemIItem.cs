using UnityEngine;
using JinnyProcessItem;

//by.J:230814 ����Ŭ���� / ProcessItemDataInfo �����͸� IItem �°� ����
//by,j:230829 ProcessItemIItem�� ProcessItemDataInfo ���� �޸� ����� ���� �߰� �۾�
public class ProcessItemIItem : IItem
{
    private ProcessItemDataInfo _info;
    //_info -> ���������� �����ϰ� �ִ� ���� ������ ��ü / ProcessItemDataInfoŸ���� �ν��Ͻ� ����

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
