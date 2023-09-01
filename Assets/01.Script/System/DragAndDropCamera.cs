using UnityEngine;

//by.J:230725 �巡�� �� ȭ�� �� ī�޶� �̵�
public class DragAndDropCamera : MonoBehaviour
{
    private Vector3 touchStart;
    public Camera cam;

    private Vector2 minPosition = new Vector2(-10, -10); //ī�޶� �̵� �ּ� ��ġ
    private Vector2 maxPosition = new Vector2(20, 20);   //ī�޶� �̵� �ִ� ��ġ

    public bool dragOk = true; //�̵� ���� ����

    void Update()
    {
        if (!dragOk)
        {
            return;
        }
           

        //����� ����
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began: //Began = ȭ�鿡 ��ġ ���� ����
                    touchStart = cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, cam.transform.position.z));
                    break;
                case TouchPhase.Moved: //Moved = �հ��� ��ġ�� ȭ�鿡�� ������ ���
                    Vector3 direction = touchStart - cam.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, cam.transform.position.z));
                    Vector3 newPosition = cam.transform.position + direction;

                    //���ο� ��ġ�� ���� ���� ���� �ִ��� Ȯ��
                    newPosition.x = Mathf.Clamp(newPosition.x, minPosition.x, maxPosition.x);
                    newPosition.y = Mathf.Clamp(newPosition.y, minPosition.y, maxPosition.y);

                    //ī�޶� ��ġ�� ���ο� ��ġ�� ������Ʈ
                    cam.transform.position = newPosition;
                    break;
            }
        }

        //PC ����
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z));
        }
        if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, cam.transform.position.z));
            Vector3 newPosition = cam.transform.position + direction;

            //���ο� ��ġ�� ���� ���� ���� �ִ��� Ȯ��
            newPosition.x = Mathf.Clamp(newPosition.x, minPosition.x, maxPosition.x);
            newPosition.y = Mathf.Clamp(newPosition.y, minPosition.y, maxPosition.y);

            //ī�޶� ��ġ�� ���ο� ��ġ�� ������Ʈ
            cam.transform.position = newPosition;
        }
    }

    public void OkDrag()
    {
        dragOk = true;
        Debug.Log("������");
    }

    public void NoDrag()
    {
        dragOk = false;
        Debug.Log("�ȿ�����");
    }
}

