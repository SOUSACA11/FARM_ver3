using System.Collections;
using UnityEngine;
using UnityEngine.UI;

//by.J:230809 �ֹ� â Ŭ���� Ȱ��ȭ / �޴� ��ư ��Ȱ��ȭ / �ݱ� ��ư
public class OrderManagerUI : MonoBehaviour
{
    public Image image;         //������ �̹���
    public Vector3 endPosition; //������ �̵� ��ġ
    public float speed;         //�̵� �ӵ�

    public Button closeButton;      //�ݱ� ��ư
    public Button inviButton1;      //��Ȱ��ȭ �� ��ư 1��
    public Button inviButton2;      //��Ȱ��ȭ �� ��ư 2��
    public Button inviButton3;      //��Ȱ��ȭ �� ��ư 3��

    private Vector3 startPosition; //���� ��ġ

    private void Start()
    {
        //Debug.Log("x�� :" + image.rectTransform.position.x); //955
        //Debug.Log("y�� :"+ image.rectTransform.position.y);  //-3030

        closeButton.onClick.AddListener(CloseButtonOnClick);    //�ݱ� ��ư Ŭ��
        startPosition = image.transform.position;               //���� ��ġ ����
    }

    public void CloseButtonOnClick()
    {
        //�޴� ��ư ��Ȱ��ȭ, �ݱ� ��ư Ȱ��ȭ
        image.transform.position = startPosition;
        inviButton1.gameObject.SetActive(true);
        inviButton2.gameObject.SetActive(true);
        inviButton3.gameObject.SetActive(true);
    }

    public void OrderButton_Click()
    {
        //���� â ��� Ȱ��ȭ
        StartCoroutine(MoveImage());

        //�޴� ��ư ��Ȱ��ȭ
        inviButton1.gameObject.SetActive(false);
        inviButton2.gameObject.SetActive(false);
        inviButton3.gameObject.SetActive(false);
    }

    IEnumerator MoveImage()
    {

        //ó�� y��    : -3030
        //������ y��  : -680

        float t = 0f; // �ð� ����

        Vector3 startPosition = image.transform.position;  // ���� ��ġ ����

        endPosition = new Vector3(977+400, image.rectTransform.position.y + 3550, 0); //������ ��ġ ����

        while (t < 1f) // t�� 1�� �� ������
        {
            if (image.rectTransform.position.y >= 480) //y���� 480 �̻��̸� ����
            {
                yield break;
            }

            t += Time.deltaTime * speed; // �ð� ����

            // Lerp�� �̿��� ���� ��ġ���� endPosition���� �ε巴�� �̵�
            image.transform.position = Vector3.Lerp(startPosition, endPosition, t);

            yield return null; // ������ ���ݴ�� ����

        }
    }
}
