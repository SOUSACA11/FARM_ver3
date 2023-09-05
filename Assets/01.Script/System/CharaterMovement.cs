using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//by.J:230726 ĳ���� ������
//by.J:230901 �ݶ��̴� ������ ���ϱ�
//by.J:230904 ���� �ĸ� �ٸ� �ִϸ����� ������ ����
public class CharaterMovement : MonoBehaviour
{
    public float movementSpeed = 3.0f;
    Vector2 movement = new Vector2();
    Rigidbody2D rigidbody2D;

    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        UPdateState();
    }

    private void FixedUpdate()
    {
        MoveCharacter();
    }

    void MoveCharacter()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        movement.Normalize();

        rigidbody2D.velocity = movement * movementSpeed;
    }

    void UPdateState()
    {
        if (Mathf.Approximately(movement.x, 0) && Mathf.Approximately(movement.y, 0))
        {
            animator.SetBool("isWalk", false);
        }
        else
        {
            animator.SetBool("isWalk", true);
        }
        animator.SetFloat("DirectionX", movement.x);
        animator.SetFloat("DirectionY", movement.y);
    }
}
//    public float speed = 0.5f;     //�̵� �ӵ�
//    public float minWaitTime = 1f; //���� �������� �����ϱ� ���� �ּ� ��� �ð�
//    public float maxWaitTime = 3f; //���� �������� �����ϱ� ���� �ִ� ��� �ð�

//    private Vector2 bottomLeft; //ȭ���� ���� �ϴ��� ���� ��ǥ
//    private Vector2 topRight;   //ȭ���� ������ ����� ���� ��ǥ

//    private Vector3 targetPosition; //���� �̵��� ��ǥ ��ġ

//    public float avoidDistance = 1.0f;  // �ݶ��̴��� ������ �Ÿ�
//    public LayerMask obstacleMask;      // ������ ��ֹ� ���̾�
 
//    private Animator animator; //Animator ������Ʈ

//    private void Start()
//    {
//        //Animator ������Ʈ ��������
//        animator = GetComponent<Animator>();

//        //ȭ���� ���� �ϴܰ� ������ ����� ���� ��ǥ ���ϱ�
//        bottomLeft = Camera.main.ScreenToWorldPoint(new Vector3(0, 0, Camera.main.nearClipPlane));
//        topRight = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.nearClipPlane));

//        //Debug.Log("���� �ϴ�: " + bottomLeft);
//        //Debug.Log("������ ���: " + topRight);

//        //ó�� ������ ����
//        SetNewDestination();
//    }

//    private void Update()
//    {
//        //���� ��ġ���� ��ǥ ��ġ�� �̵�
//        //transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

//        Vector2 movementDirection = (targetPosition - transform.position).normalized;
//        SetAnimatorDirection(movementDirection);


//        //��ǥ ��ġ�� �������� �� ���ο� �������� ����
//        if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
//        {
//            StartCoroutine(WaitAndSetNewDestination());
//        }

//        //��ֹ� ���ϱ�: Ray�� ��� �տ� ��ֹ��� �ִ��� Ȯ��
//        RaycastHit2D hit = Physics2D.Raycast(transform.position, targetPosition - transform.position, avoidDistance, obstacleMask);
//        if (hit.collider != null && hit.distance < 0.5f) // ��ֹ��� Ư�� �Ÿ� �̳��� ���� ���
//        {
//            SetNewDestination();
//        }
//    }

//    private void SetNewDestination()
//    {
//        Vector3 oldTargetPosition = targetPosition;

//        float x;
//        float y;

//        if (Random.value < 0.5f)
//        {
//            x = transform.position.x - Random.Range(0f, Mathf.Abs(topRight.x - transform.position.x));
//            y = transform.position.y;
//        }
//        else
//        {
//            x = transform.position.x;
//            y = transform.position.y + Random.Range(0f, Mathf.Abs(topRight.y - transform.position.y));
//        }

//        targetPosition = new Vector3(x, y, transform.position.z);

//        StartCoroutine(MoveToTargetPosition());
//    }

  

//    private void SetAnimatorDirection(Vector2 movementDirection)
//    {
//        animator.SetFloat("DirectionX", movementDirection.x);
//        animator.SetFloat("DirectionY", movementDirection.y);
//    }
    
//    private IEnumerator MoveToTargetPosition()
//    {
//        // ���������� �̵��ϱ� ���� �ִϸ����� ���¸� ����
//        Vector2 movementDirection = (targetPosition - transform.position).normalized;
//        SetAnimatorDirection(movementDirection);

//        yield return new WaitForSeconds(0.1f); // �ʿ��� ��� ��� �ð� ����

//        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
//        {
//            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
//            yield return null;
//        }
//    }

//    private IEnumerator WaitAndSetNewDestination()
//    {
//        //������ �ð���ŭ ����� �� ���ο� �������� ����
//        float waitTime = Random.Range(minWaitTime, maxWaitTime);
//        yield return new WaitForSeconds(waitTime);
//        SetNewDestination();
//    }
//}
