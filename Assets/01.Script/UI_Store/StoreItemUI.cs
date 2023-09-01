using UnityEngine.UI;
using UnityEngine;
using TMPro;

//by.J:230724 ����UI ���� ����
//by.J:230809 ����UI �̹����� ��ġ�� �̹��� ���� ����!
public class StoreItemUI : MonoBehaviour
{
    public TextMeshProUGUI itemNameText; //������ �̸��� ǥ���� �ؽ�Ʈ ������Ʈ
    public TextMeshProUGUI itemCostText; //������ ������ ǥ���� �ؽ�Ʈ ������Ʈ
    public Image itemImage;              //������ �̹����� ǥ���� �̹��� ������Ʈ
    public SpriteRenderer itemImage2;    //������ �̹����� ǥ���� ��������Ʈ ������ ������Ʈ

    public GameObject itemPrefab;        //���� ���� ��ġ�� ������Ʈ ������

    //������ ������ UI�� �����ϴ� �޼���
    public void SetInfo(string itemName, int itemCost, Sprite itemSprite) ////, GameObject prefab)
    {
        itemNameText.text = itemName;            //������ �̸� ����
        itemCostText.text = itemCost.ToString(); //������ ���� ����
        itemImage.sprite = itemSprite;           //������ �̹��� ����
        itemImage2.sprite = itemSprite;          //������ ��������Ʈ ������ ����

        //���� ���� ���� ��ġ�� ������Ʈ ����
        GameObject sceneObject = Instantiate(itemPrefab);
        //SpriteRenderer�� ��������Ʈ ����
        SpriteRenderer spriteRenderer = sceneObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemSprite;

    }
}