using UnityEngine;
using UnityEngine.UI;
using TMPro;

//by.J:230808 창고 슬롯
public class StorageSlot : MonoBehaviour
{
    //public Text itemNameText;
    public Image itemImage;
    public TextMeshProUGUI itemCountText;
    

    public void SetItem(IItem item, int count)
    {
        Debug.Log("창고 슬롯 아이템셋");
        
        itemImage.sprite = item.ItemImage;      //item.ItemImage[0];
        itemCountText.text = count.ToString();
        //itemNameText.text = item.ItemName[0]; // 처음 아이템의 이름을 사용

    }
}


