using UnityEngine;
using UnityEngine.UI;
using TMPro;

//by.J:230817 건물 원재료 슬롯 설정
public class IngredientSlot : MonoBehaviour
{
    public Image ingredientImage; //원재료 이미지
    public TextMeshProUGUI ingredientQuantity; //원재료 수량

    public void SetIngredient(IItem production, int quantity)
    {
        //ingredientImage.sprite = production.ItemImage;
        //ingredientQuantity.text = quantity.ToString();

        //Debug.Log("Setting ingredient: " + production.ItemName + " with image: " + production.ItemImage.name);
        ////Debug.Log("Setting ingredient: " + production.ItemName());
        ///

        if (ingredientImage == null)
        {
            Debug.LogError("ingredientImage이 null입니다!");
            return;
        }

        if (production == null)
        {
            Debug.LogError("production이 null입니다!");
            return;
        }

        if (production.ItemImage == null)
        {
            Debug.LogError("production.ItemImage이 null입니다!");
            return;
        }

        if (ingredientQuantity == null)
        {
            Debug.LogError("ingredientQuantity가 null입니다!");
            return;
        }

        ingredientImage.sprite = production.ItemImage;
        ingredientQuantity.text = quantity.ToString();
        Debug.Log("원재료 설정: " + production.ItemName + " 이미지: " + production.ItemImage.name);
    }
}
