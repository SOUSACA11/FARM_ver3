using UnityEngine;
using System.Collections;

//by.J:230726 ĳ���� ������
//by.J:230904 ���� �ĸ� �ٸ� �ִϸ����� ������ ����
//by.J:230914 ���⿡ ���� �ִϸ��̼� Ŭ�� ���� ����
public class CharaterMovement : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed = 2.0f;
    public Vector2 moveDurationRange = new Vector2(1.0f, 3.0f);
    public Vector2 waitDurationRange = new Vector2(0.5f, 2.0f);

    public float moveDuration;
    private float waitDuration;
    private bool isMoving = false;

    private Vector3 currentMoveDir = Vector3.zero;

    public Vector2 minBounds;  // �������� �ּ� ��谪
    public Vector2 maxBounds;  // �������� �ִ� ��谪

    public GameObject frontRightObject; // ù ��° ������Ʈ
    public GameObject backLeftObject; // �� ��° ������Ʈ

    private float animationDelay = 8f;  // �ִϸ��̼� ��� �ð� ���� (8��)
    private void Start()
    {
        animator = GetComponent<Animator>();
        PickRandomDirection();
        Vector3 worldPosition = gameObject.transform.position;
        Debug.Log("������ǥ" + worldPosition);

        //ó�� ������ �� �� ������Ʈ �� �ϳ��� Ȱ��ȭ
        frontRightObject.gameObject.SetActive(true);
        backLeftObject.gameObject.SetActive(false);
    }


    private void Update()
    {
        GameObject activeObject = frontRightObject.activeSelf ? frontRightObject : backLeftObject;

        if (isMoving)
        {
            moveDuration -= Time.deltaTime;

            // ���� ��ġ�� ����մϴ�.
            Vector3 expectedPosition = activeObject.transform.position + currentMoveDir * moveSpeed * Time.deltaTime;

            // ���� ��ġ�� ��� ���� �ִ��� Ȯ���մϴ�.
            if (expectedPosition.x >= minBounds.x && expectedPosition.x <= maxBounds.x &&
                expectedPosition.y >= minBounds.y && expectedPosition.y <= maxBounds.y)
            {
                activeObject.transform.position = expectedPosition;
            }

            if (moveDuration <= 0)
            {
                moveDuration = 0;
                isMoving = false;
                waitDuration = Random.Range(waitDurationRange.x, waitDurationRange.y);
            }
        }
        else
        {
            waitDuration -= Time.deltaTime;
            if (waitDuration <= 0)
            {
                PickRandomDirection();
            }
        }

    }

    void PickRandomDirection()
    {
        StopCoroutine(StartMovementAfterDelay(animationDelay));

        Vector3 moveDir = Vector3.zero;
        int randomChoice = Random.Range(0, 4);

        switch (randomChoice)
        {
            case 0: // �� (Front)
                ActivateObject(frontRightObject, backLeftObject);
                moveDir = new Vector3(-1, -1, 0).normalized;
                frontRightObject.GetComponent<Animator>().SetTrigger("isFront");
                break;
            case 1: // ������ (Right)
                ActivateObject(frontRightObject, backLeftObject);
                moveDir = new Vector3(1, -1, 0).normalized;
                frontRightObject.GetComponent<Animator>().SetTrigger("isRight");
                break;
            case 2: // ���� (Left)
                ActivateObject(backLeftObject, frontRightObject);
                moveDir = new Vector3(-1, 1, 0).normalized;
                backLeftObject.GetComponent<Animator>().SetTrigger("isLeft");
                break;
            case 3: // ���� (Back)
                ActivateObject(backLeftObject, frontRightObject);
                moveDir = new Vector3(1, 1, 0).normalized;
                backLeftObject.GetComponent<Animator>().SetTrigger("isBack");
                break;
        }

        currentMoveDir = moveDir;
        moveDuration = Random.Range(moveDurationRange.x, moveDurationRange.y);
        isMoving = true;

        // ������Ʈ Ȱ��ȭ Ȯ�� �� Ȱ��ȭ
        if (!this.gameObject.activeSelf)
        {
            this.gameObject.SetActive(true);
        }

        StartCoroutine(StartMovementAfterDelay(animationDelay));
    }

    void ActivateObject(GameObject toActivate, GameObject toDeactivate)
    {
        Debug.Log("�������");

        if (toDeactivate.activeSelf)
        {
            toDeactivate.SetActive(false);
        }

        if (!toActivate.activeSelf)
        {
            toActivate.transform.position = toDeactivate.transform.position;
            toActivate.SetActive(true);
        }

    }

    IEnumerator StartMovementAfterDelay(float delay)
    {
        // ������ �ð���ŭ ���
        yield return new WaitForSeconds(delay);

        // ������ ����
        isMoving = true;
    }

}