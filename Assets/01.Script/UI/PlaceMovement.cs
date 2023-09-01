using UnityEngine;

//by.J:230809 �������� �� �ǹ� ���� ��ġ�� ���� �̵� �ý���
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
        //���콺 ��ġ�� ������ ������Ʈ�� �̵�
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, 0);

        //���콺 ��ư�� Ŭ���ϸ� ������ ������Ʈ�� ���� ������Ʈ�� Ȯ��
        if (Input.GetMouseButtonDown(0))
        {
            spriteRenderer.color = Color.white; //���� ��������
            this.enabled = false;              //�� ��ũ��Ʈ ��Ȱ��ȭ
        }
    }
}
