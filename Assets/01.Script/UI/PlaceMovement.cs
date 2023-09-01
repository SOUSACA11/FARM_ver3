using UnityEngine;

//by.J:230809 상점에서 산 건물 자유 배치를 위한 이동 시스템
public class PlaceMovement : MonoBehaviour
{
    private StoreItemUI storeItem;
    private SpriteRenderer spriteRenderer;

    public void Initialize(StoreItemUI storeItemUI)
    {
        storeItem = storeItemUI;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        //마우스 위치에 프리뷰 오브젝트를 이동
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);

        //마우스 버튼을 클릭하면 프리뷰 오브젝트를 게임 오브젝트로 확정
        if (Input.GetMouseButtonDown(0))
        {
            spriteRenderer.color = Color.white; //원래 색상으로
            this.enabled = false;              //이 스크립트 비활성화
        }
    }
}
