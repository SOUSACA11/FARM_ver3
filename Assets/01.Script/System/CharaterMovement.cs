using UnityEngine;
using System.Collections;

//by.J:230726 ĳ���� ������
//by.J:230901 �ݶ��̴� ������ ���ϱ�
public class CharaterMovement : MonoBehaviour
{
    public float speed = 0.5f;     //�̵� �ӵ�
    public float minWaitTime = 1f; //���� �������� �����ϱ� ���� �ּ� ��� �ð�
    public float maxWaitTime = 3f; //���� �������� �����ϱ� ���� �ִ� ��� �ð�

    private Vector2 bottomLeft; //ȭ���� ���� �ϴ��� ���� ��ǥ
    private Vector2 topRight;   //ȭ���� ������ ����� ���� ��ǥ

    private Vector3 targetPosition; //���� �̵��� ��ǥ ��ġ

    //
    public float avoidDistance = 1.0f;  // �ݶ��̴��� ������ �Ÿ�
    public LayerMask obstacleMask;      // ������ ��ֹ� ���̾�
    //

    private Animator animator; //Animator ������Ʈ

    private void Start()
    {
        //Animator ������Ʈ ��������
        animator = GetComponent<Animator>();

        //ȭ���� ���� �ϴܰ� ������ ����� ���� ��ǥ ���ϱ�
        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
        topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));

        //Debug.Log("���� �ϴ�: " + bottomLeft);
        //Debug.Log("������ ���: " + topRight);

        //ó�� ������ ����
        SetNewDestination();
    }

    private void Update()
    {
        //���� ��ġ���� ��ǥ ��ġ�� �̵�
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        //��ǥ ��ġ�� �������� �� ���ο� �������� ����
        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
        {
            StartCoroutine(WaitAndSetNewDestination());
        }

        // ��ֹ� ���ϱ�: Ray�� ��� �տ� ��ֹ��� �ִ��� Ȯ��
        if (Physics2D.Raycast(transform.position, targetPosition - transform.position, avoidDistance, obstacleMask))
        {
            // ��ֹ��� �����Ǹ� ���ο� ������ ����
            SetNewDestination();
        }
    }

    private void SetNewDestination()
    {
        //ȭ�� ������ ������ ��ġ�� �������� ����
        float x = Random.Range(bottomLeft.x, topRight.x);
        float y = Random.Range(bottomLeft.y, topRight.y);

        float isoX = (x - y) / 2;
        float isoY = (x + y) / 2;
        //Debug.Log("X: " + isoX);
        //Debug.Log("Y: " + isoY);

        targetPosition = new Vector3(x, y, transform.position.z);
    }

    private IEnumerator WaitAndSetNewDestination()
    {
        //������ �ð���ŭ ����� �� ���ο� �������� ����
        float waitTime = Random.Range(minWaitTime, maxWaitTime);
        yield return new WaitForSeconds(waitTime);
        SetNewDestination();
    }
}
