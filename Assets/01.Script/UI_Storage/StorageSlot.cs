using UnityEngine;
using UnityEngine.UI;
using TMPro;

//by.J:230808 â�� ����
public class StorageSlot : MonoBehaviour
{
    //public Text itemNameText;
    public Image itemImage;
    public TextMeshProUGUI itemCountText;
    

    public void SetItem(IItem item, int count)
    {
        Debug.Log("â�� ���� �����ۼ�");
        
        itemImage.sprite = item.ItemImage;      //item.ItemImage[0];
        itemCountText.text = count.ToString();
        //itemNameText.text = item.ItemName[0]; // ó�� �������� �̸��� ���



    }
}


