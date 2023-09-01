using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using JinnyBuilding;
using JinnyFarm;
using JinnyAnimal;

//by.J:230724 ���� â Ŭ���� Ȱ��ȭ / �޴� ��ư ��Ȱ��ȭ / �ݱ� ��ư / ���� ������ �б�
//by.J:230724 ���� �� UI ���� �߰�
//by.J:230809 ���� ������ �� ��� ���� â �Ʒ��� ������ ��ġ �Ϸ� �� ��� ����ġ
//by.J:230810 ���� ������ ����Ʈ ��ȸ�ؼ� ���������� ���Կ� ��ġ / ����, ���� ��ư Ŭ���� �´� ���� ����

public class StoreManagerUI : MonoBehaviour
{
    public Image image;            //������ ���� â �̹��� 
    public Vector3 endPosition;    //������ �̵� ��ġ
    public float speed = 120f;     //�̵� �ӵ�
    private Vector3 startPosition; //������ġ

    public Button closeButton;   //�ݱ� ��ư
    public Button inviButton1;   //��Ȱ��ȭ �� ��ư 1��
    public Button inviButton2;   //��Ȱ��ȭ �� ��ư 2��
    public Button inviButton3;   //��Ȱ��ȭ �� ��ư 3��
    public Button nextButton;    //���� ������ ��ư

    public StoreSlot[] slots;           //���� ����
    private int currentPage = 0;        //���� ������ ��ȣ
    private const int itemsPerPage = 3; //�������� ������ ��

    public ItemDic itemDic;                    //������ ������ ���� ItemDic Ŭ����
    private Dictionary<string, object> items;  //������ ������ ���� ����

    List<StoreItemUI> storeItems = new List<StoreItemUI>();  //���� ������ ��ü���� ������ ����Ʈ

    //public GameObject itemPrefab;
    //public Transform itemParan;

    // *������ ���� ����* 
    private TabType currentTab = TabType.Building; //ó�� ���� �� �����ֱ�

    public enum TabType
    {
        Building,
        Farm,
        Animal
    }

    public void TabBuilding() //���� ��
    {
        currentTab = TabType.Building;
        currentPage = 0; 
        DisplayItems();
    }

    public void TabFarm() //����� ��
    {
        currentTab = TabType.Farm;
        currentPage = 0; 
        DisplayItems();
    }

    public void TabAnimal() //���� ��
    {
        currentTab = TabType.Animal;
        currentPage = 0; 
        DisplayItems();
    }

    public void NextPage() //���� ��ư
    {
        currentPage++;
        DisplayItems();
    }

    public void PreviousPage() //���� ��ư
    {
        currentPage = Mathf.Max(0, currentPage - 1); //0���� ���� �ʰ�
        DisplayItems();
    }

    //���� ������ ��ư Ȱ��ȭ, ��Ȱ��ȭ / ����Ʈ ������ ������ �� ��� ���� ������ ��Ȱ��ȭ
    void NextButtonDecision<T>(List<T> itemList)
    {
        if ((currentPage +1) * itemsPerPage < itemList.Count)
        {
            nextButton.interactable = true;
        }
        else
        {
            nextButton.interactable = false;
        }
    }
  
    //���� ������
    void DisplayBuilding(List<BuildingDataInfo> buildingList) 
    {
        int start = currentPage * itemsPerPage;
        int end = Mathf.Min(start + itemsPerPage, buildingList.Count); //Mathf.Min = a,b �� ���� �� ��ȯ

        foreach (StoreSlot slot in slots)
        {
            slot.gameObject.SetActive(false);
        }

        for (int i = start; i < end; i++)
        {
            int slotIndex = i - start;
            slots[slotIndex].SetSlotBuilding(buildingList[i]);
            slots[slotIndex].gameObject.SetActive(true);
        }

        NextButtonDecision(buildingList);
    }

    //����� ������
    void DisplayFarm(List<FarmDataInfo> farmList)
    {
        int start = currentPage * itemsPerPage;
        int end = Mathf.Min(start + itemsPerPage, farmList.Count); //Mathf.Min = a,b �� ���� �� ��ȯ

        foreach (StoreSlot slot in slots)
        {
            slot.gameObject.SetActive(false);
        }

        for (int i = start; i < end; i++)
        {
            int slotIndex = i - start;
            slots[slotIndex].SetSlotFarm(farmList[i]);
            slots[slotIndex].gameObject.SetActive(true);
        }

        NextButtonDecision(farmList);
    }

    //���� ������
    void DisplayAnimal(List<AnimalDataInfo> animalList)
    {
        int start = currentPage * itemsPerPage;
        int end = Mathf.Min(start + itemsPerPage, animalList.Count); //Mathf.Min = a,b �� ���� �� ��ȯ

        foreach (StoreSlot slot in slots)
        {
            slot.gameObject.SetActive(false);
        }

        for (int i = start; i < end; i++)
        {
            int slotIndex = i - start;
            slots[slotIndex].SetSlotAnimal(animalList[i]);
            slots[slotIndex].gameObject.SetActive(true);
        }

        NextButtonDecision(animalList);
    }

    //������ ���� �����ֱ�
    void DisplayItems()
    {
        if (currentTab == TabType.Building) //�ǹ� �� ó��
        {
            Building buildingComponent = FindObjectOfType<Building>();
            if (buildingComponent != null)
            {
                List<BuildingDataInfo> buildingList = buildingComponent.buildingDataList;
                DisplayBuilding(buildingList);
            }
        }
        else if (currentTab == TabType.Farm) //����� �� ó��
        {
            Farm farmComponent = FindObjectOfType<Farm>();
            if (farmComponent != null)
            {
                List<FarmDataInfo> farmList = farmComponent.farmDataList; 
                DisplayFarm(farmList);
            }
        }
        else if (currentTab == TabType.Animal) //���� �� ó��
        {
            Animal animalComponent = FindObjectOfType<Animal>();
            if (animalComponent != null)
            {
                List<AnimalDataInfo> animalList = animalComponent.animalDataList;
                DisplayAnimal(animalList);
            }
        }
    }



    private void Start() 
    {
        //Debug.Log(image.rectTransform.position.x);
        //Debug.Log(image.rectTransform.position.y);

        closeButton.onClick.AddListener(CloseButtonOnClick);    //�ݱ� ��ư Ŭ��
        startPosition = image.transform.position;               //���� ��ġ ����

        itemDic = FindObjectOfType<ItemDic>(); //ItemDic Ŭ������ ã�Ƽ� itemDic�� ����
        items = itemDic.Item;                  //ItemDic Ŭ������ �ִ� Item ������ items�� ����

        DisplayItems(); //������ ���� �����ֱ� ����

    }



    // *UI �̹��� ������ ó��*
    public void CloseButtonOnClick()
    {
        //�޴� ��ư ��Ȱ��ȭ, �ݱ� ��ư Ȱ��ȭ
        image.transform.position = startPosition;
        inviButton1.gameObject.SetActive(true);
        inviButton2.gameObject.SetActive(true);
        inviButton3.gameObject.SetActive(true);
    }

    public void StoreButton_Click()
    {
        //�޴� ��ư ��Ȱ��ȭ
        inviButton1.gameObject.SetActive(false);
        inviButton2.gameObject.SetActive(false);
        inviButton3.gameObject.SetActive(false);

        foreach (var slot in slots)
        {
            slot.ResetSlot();
        }

        //���� â ��� Ȱ��ȭ
        StartCoroutine(MoveImageUp());
    }
    //�ۿ� �ִ� ���� â ȭ��� ��ġ
    IEnumerator MoveImageUp()
    {
        //ó�� y��    : -846
        //������ y��  : 318

        float t = 0f; //�ð� ����

        Vector3 startPosition = image.transform.position;  //���� ��ġ ����

        endPosition = new Vector3(948+400, image.rectTransform.position.y + 1150, 0); //������ ��ġ ����

        while (t < 1f) //t�� 1�� �� ������
        {
            if (image.rectTransform.position.y >= 318) //y���� 318 �̻��̸� ����
            {
                yield break;
            }

            t += Time.deltaTime * speed; //�ð� ����

            //Lerp�� �̿��� ���� ��ġ���� endPosition���� �ε巴�� �̵�
            image.transform.position = Vector3.Lerp(startPosition, endPosition, t);

            yield return null; //������ ���ݴ�� ����
        }
    }

    //ȭ��� ��ġ�� ���� â �Ʒ��� ������
    public void BottomStore_Click()
    {
        StartCoroutine(MoveImageBottom());
    }
    IEnumerator MoveImageBottom()
    {
        //y�� : -191
        //Debug.Log("���� â ������ �ñ׳�");
        float t = 0f; // �ð� ����

        Vector3 startPosition = image.transform.position;  //���� ��ġ ����

        endPosition = new Vector3(948+400, image.rectTransform.position.y - 480, 0); //������ ��ġ ����

        while (t < 1f) //t�� 1�� �� ������
        {
            if (image.rectTransform.position.y >= 2000) //y�� 2000 �̻��̸� ����
            {
                yield break;
            }

            t += Time.deltaTime * speed; //�ð� ����

            //Lerp�� �̿��� ���� ��ġ���� endPosition���� �ε巴�� �̵�
            image.transform.position = Vector3.Lerp(startPosition, endPosition, t);

            yield return null; //������ ���ݴ�� ����

        }
    }

    //ȭ��� ��ġ�� ���� â ���� �ø���
    public void TopStore_Click()
    {
        StartCoroutine(MoveImageTop());
    }
    IEnumerator MoveImageTop()
    {
        //Debug.Log("���� â �ø��� �ñ׳�");
        float t = 0f; // �ð� ����

        Vector3 startPosition = image.transform.position;  //���� ��ġ ����

        endPosition = new Vector3(948+400, image.rectTransform.position.y + 480, 0); //������ ��ġ ����

        while (t < 1f) //t�� 1�� �� ������
        {
            if (image.rectTransform.position.y >= 2000) //y�� 2000 �̻��̸� ����
            {
                yield break;
            }

            t += Time.deltaTime * speed; //�ð� ����

            //Lerp�� �̿��� ���� ��ġ���� endPosition���� �ε巴�� �̵�
            image.transform.position = Vector3.Lerp(startPosition, endPosition, t);

            yield return null; //������ ���ݴ�� ����

        }
    }

}

