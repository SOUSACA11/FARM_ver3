using UnityEngine;
using JinnyCropItem;

//by.J:230814 ����Ŭ���� / CropItemDataInfo �����͸� IItem �°� ����
//by,j:230829 CropItemIItem�� CropItemDataInfo ���� �޸� ����� ���� �߰� �۾�
public class CropItemIItem : IItem
{
    private CropItemDataInfo _info;
    //_info -> ���������� �����ϰ� �ִ� ���� ������ ��ü / CropItemDataInfoŸ���� �ν��Ͻ� ����

    public CropItemIItem(CropItemDataInfo info)
        {
            _info = info;
        }

    public string ItemName => _info.cropItemName;
    public int ItemCost => _info.cropItemCost;
    public Sprite ItemImage => _info.cropItemImage;
    public string ItemId => _info.cropItemId;

    public override bool Equals(object obj)
    {
        if (obj is CropItemIItem other)
        {
            return this.ItemId == other.ItemId;
        }
        return false;
    }

    public override int GetHashCode()
    {
        if (this.ItemId != null)
        {
            return this.ItemId.GetHashCode();
        }
        else
        {
            return 0; // or some default value
        }
    }
}
