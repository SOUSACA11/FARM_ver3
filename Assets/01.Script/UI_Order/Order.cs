using UnityEngine;

//by.J:230811 주문서 프로퍼티
public class Order : MonoBehaviour
{
    [SerializeField] //이름
    private string itemName;
    public string ItemName
    {
        get { return itemName; }
        set { itemName = value; }
    }

    [SerializeField] //ID
    private string itemId;
    public string ItemId
    {
        get { return itemId; }
        set { itemId = value; }
    }

    [SerializeField] //이미지
    private Sprite itemImage;
    public Sprite ItemImage
    {
        get { return itemImage; }
        set { itemImage = value; }
    }

    [SerializeField] //수량
    private int quantity;
    public int Quantity
    {
        get { return quantity; }
        set { quantity = value; }
    }

    [SerializeField] //가격
    private int totalCost;
    public int TotalCost
    {
        get { return totalCost; }
        set { totalCost = value; }
    }
}