using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

//by.J:230808 â�� â Ŭ���� Ȱ��ȭ / �޴� ��ư ��Ȱ��ȭ / �ݱ� ��ư 
public class StorageManagerUI : MonoBehaviour
{
    public Image image;         //������ �̹���
    public Vector3 endPosition; //������ �̵� ��ġ
    public float speed;         //�̵� �ӵ�

    public Button closeButton;  //�ݱ� ��ư

    public Button inviButton1;      //��Ȱ��ȭ �� ��ư 1��
    public Button inviButton2;      //��Ȱ��ȭ �� ��ư 2��
    public Button inviButton3;      //��Ȱ��ȭ �� ��ư 3��

    private Vector3 startPosition; //���� ��ġ
    public StorageSlotUI storageSlotUI;

    private TabType currentTab = TabType.CropItem; //ó�� ���� �� �����ֱ�

    public enum TabType
    {
        CropItem,
        ProcessItem
    }

    public void TabCropItem()
    {
        Debug.Log("����ȯ �۹�");
        currentTab = TabType.CropItem;
        storageSlotUI.SetCurrentTab(currentTab);
        Dictionary<IItem, int> cropItems = Storage.Instance.GetCropItems();
        DisplayItems(cropItems);
    }

    public void TabProcessItem()
    {
        Debug.Log("����ȯ ����ǰ");
        currentTab = TabType.ProcessItem;
        storageSlotUI.SetCurrentTab(currentTab);
        Dictionary<IItem, int> processItems = Storage.Instance.GetProcessItems();
        DisplayItems(processItems);
    }

    private void Start()
    {
        //Debug.Log("x�� :" + image.rectTransform.position.x); //977
        //Debug.Log("y�� :"+ image.rectTransform.position.y);  //-1824

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

    public void StorageButton_Click()
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

        //ó�� y��    : -1824
        //������ y��  : 526

        float t = 0f; //�ð� ����

        Vector3 startPosition = image.transform.position;  //���� ��ġ ����

        endPosition = new Vector3(977 + 400, image.rectTransform.position.y + 2350, 0); //������ ��ġ ����

        while (t < 1f) //t�� 1�� �� ������
        {
            if (image.rectTransform.position.y >= 480) //y�� 480 �̻��̸� ����
            {
                yield break;
            }

            t += Time.deltaTime * speed; //�ð� ����

            //Lerp�� �̿��� ���� ��ġ���� endPosition���� �ε巴�� �̵�
            image.transform.position = Vector3.Lerp(startPosition, endPosition, t);

            yield return null; //������ ���ݴ�� ����

        }
    }

    public void DisplayItems(Dictionary<IItem, int> itemsToDisplay)
    {
        storageSlotUI.UpdateUI(); // UpdateUI �Լ� ȣ��
    }
}

