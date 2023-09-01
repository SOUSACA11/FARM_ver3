using UnityEngine.UI;
using UnityEngine;
using TMPro;

//by.J:230724 상점UI 설정 도움
//by.J:230809 상점UI 이미지와 배치할 이미지 연결 성공!
public class StoreItemUI : MonoBehaviour
{
    public TextMeshProUGUI itemNameText; //아이템 이름을 표시할 텍스트 컴포넌트
    public TextMeshProUGUI itemCostText; //아이템 가격을 표시할 텍스트 컴포넌트
    public Image itemImage;              //아이템 이미지를 표시할 이미지 컴포넌트
    public SpriteRenderer itemImage2;    //아이템 이미지를 표시할 스프라이트 렌더러 컴포넌트

    public GameObject itemPrefab;        //게임 씬에 배치할 오브젝트 프리팹

    //아이템 정보를 UI에 설정하는 메서드
    public void SetInfo(string itemName, int itemCost, Sprite itemSprite) ////, GameObject prefab)
    {
        itemNameText.text = itemName;            //아이템 이름 설정
        itemCostText.text = itemCost.ToString(); //아이템 가격 설정
        itemImage.sprite = itemSprite;           //아이템 이미지 설정
        itemImage2.sprite = itemSprite;          //아이템 스프라이트 렌더러 설정

        //실제 게임 씬에 배치할 오브젝트 생성
        GameObject sceneObject = Instantiate(itemPrefab);
        //SpriteRenderer에 스프라이트 설정
        SpriteRenderer spriteRenderer = sceneObject.GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = itemSprite;

    }
}