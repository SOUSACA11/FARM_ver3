using UnityEngine;

//by.J:230725 ��ġ ���� �ý���
public class TouchManager : MonoBehaviour
{
    private Camera mainCamera;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            //��� ��ġ �̺�Ʈ ��������
            Touch[] touches = Input.touches;

            //��ġ�� ��� ��ġ ����ĳ��Ʈ �˻�
            foreach (Touch touch in touches)
            {
                Vector3 touchPosition = mainCamera.ScreenToWorldPoint(touch.position); //��ġ�� ȭ�� ��ǥ ���� ��ǥ�� ��ȯ
                touchPosition.z = 0f;                                                  //ȭ�� ��ǥ z�� 0 ����

                RaycastHit2D hit = Physics2D.Raycast(touchPosition, Vector2.zero);

                //����ĳ���� �浹 ������Ʈ
                if (hit.collider != null)
                {
                    // ���⿡ ������Ʈ�� Ŭ���� ����� ������ �ۼ��մϴ�.
                    // hit.collider.gameObject �� ��ġ�� ������Ʈ�� ��Ÿ���ϴ�.
                    // ���� ���, hit.collider.gameObject.GetComponent<Building>() �� ����Ͽ� �ǹ� ������ ������ �� �ֽ��ϴ�.
                }
            }
        }
    }
}
