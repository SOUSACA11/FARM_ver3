using UnityEngine;

//by.J:230811 �ֹ��� ������Ƽ
public class Order : MonoBehaviour
{
    //public string ItemName { get; set; }  //�̸�
    //public string ItemId { get; set; }    //ID
    //public Sprite ItemImage { get; set; } //�̹���
    //public int Quantity { get; set; }     //����
    //public int TotalCost { get; set; }    //�� ���

    [SerializeField]
    private string itemName;
    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }

    [SerializeField]
    private string itemId;
    public string ItemId
    {
        get { return itemId; }
        set { itemId = value; }
    }

    [SerializeField]
    private Sprite itemImage;
    public Sprite ItemImage
    {
        get { return itemImage; }
        set { itemImage = value; }
    }

    [SerializeField]
    private int quantity;
    public int Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }

    [SerializeField]
    private int totalCost;
    public int TotalCost
    {
        get { return totalCost; }
        set { totalCost = value; }
    }
}