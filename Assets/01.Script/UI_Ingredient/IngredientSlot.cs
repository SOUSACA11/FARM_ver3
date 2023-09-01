using UnityEngine;
using UnityEngine.UI;
using TMPro;

//by.J:230817 �ǹ� ����� ���� ����
public class IngredientSlot : MonoBehaviour
{
    public Image ingredientImage; //����� �̹���
    public TextMeshProUGUI ingredientQuantity; //����� ����

    public void SetIngredient(IItem production, int quantity)
    {
        //ingredientImage.sprite = production.ItemImage;
        //ingredientQuantity.text = quantity.ToString();

        //Debug.Log("Setting ingredient: " + production.ItemName + " with image: " + production.ItemImage.name);
        ////Debug.Log("Setting ingredient: " + production.ItemName());
        ///

        if (ingredientImage == null)
        {
            Debug.LogError("ingredientImage�� null�Դϴ�!");
            return;
        }

        if (production == null)
        {
            Debug.LogError("production�� null�Դϴ�!");
            return;
        }

        if (production.ItemImage == null)
        {
            Debug.LogError("production.ItemImage�� null�Դϴ�!");
            return;
        }

        if (ingredientQuantity == null)
        {
            Debug.LogError("ingredientQuantity�� null�Դϴ�!");
            return;
        }

        ingredientImage.sprite = production.ItemImage;
        ingredientQuantity.text = quantity.ToString();
        Debug.Log("����� ����: " + production.ItemName + " �̹���: " + production.ItemImage.name);
    }
}
