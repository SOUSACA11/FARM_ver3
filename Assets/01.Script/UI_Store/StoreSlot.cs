using JinnyAnimal;
using JinnyBuilding;
using JinnyFarm;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//by.J:230810 ���� ���� ���� / ���� �ǹ� �巡��
//by.J:230817 �ǹ� �巡�� �� ���� Ȱ��ȭ
//by.J:230825 ���̼Ҹ�Ʈ�� �� ����� ���� �������� ����
//Ÿ�� ����� �̺�Ʈ �߰�
public class StoreSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Camera mainCamera;                  //���� ī�޶�
    public Image itemImage;                     //������ �̹���
    public TextMeshProUGUI itemName;            //������ �̸�
    public TextMeshProUGUI itemCost;            //������ ����
    public GameObject currentPrefab;            //��ġ�� ������ ������
    private GameObject clone;                   //�巡�� ���� �ǹ� ������
    private SpriteRenderer itemSpriteRenderer;  //������ ��������Ʈ ������ �̹���
    public BuildingType currentBuildingType;    //�ǹ� Ÿ��
    public FarmType currentFarmType;            //���� Ÿ��
    public AnimalType currentAnimalType;        //��� Ÿ��

    private BuildingDataInfo currentBuildingData = new BuildingDataInfo(); //���� ���� �ǹ� ������
    private FarmDataInfo currentFarmData = new FarmDataInfo();             //���� ���� ����� ������
    private AnimalDataInfo currentAnimalData = new AnimalDataInfo();       //���� ���� ���� ������


    public static FarmDataInfo? SelectedFarmData = null;
    public static AnimalDataInfo? SelectedAnimalData = null;


    public delegate void BuildingTypeChangedDelegate(BuildingType newType);
    public static event BuildingTypeChangedDelegate OnBuildingTypeChanged;

    private void Awake()
    {
        mainCamera = Camera.main; //���� ī�޶� ��������
        itemSpriteRenderer = GetComponent<SpriteRenderer>(); //��������Ʈ ������ ��������
    }

    //�ʱ�ȭ
    public void ResetSlot()
    {
        
        //���� �ʱ�ȭ
        SelectedFarmData = null;
        SelectedAnimalData = null;
    }

    //���� ����
    public void SetSlotBuilding(BuildingDataInfo buildingData)
    {
        //Debug.Log("SetSlotBuilding �ǳ��� " + buildingData.buildingName);

        itemName.text = buildingData.buildingName;
        itemCost.text = buildingData.buildingCost.ToString();
        itemImage.sprite = buildingData.buildingImage;
        currentBuildingType = buildingData.buildingType;
        currentFarmType = FarmType.None;
        currentAnimalType = AnimalType.None;

        //Debug.Log("store slot ����Ÿ��" + currentBuildingType);
        ////�̺�Ʈ �߻�
        //OnBuildingTypeChanged?.Invoke(currentBuildingType);

        if (itemSpriteRenderer != null) //��������Ʈ �������� �ִٸ� ��������Ʈ ����
        {
            itemSpriteRenderer.sprite = buildingData.buildingImage;
            currentPrefab = buildingData.buildingPrefab;
        }

        currentBuildingData = buildingData; //currentBuildingDat ������Ʈ
        currentFarmData = new FarmDataInfo();
        //Debug.Log("���� ���� �ҷ����� ���� " + buildingData.buildingCost);///
        // Debug.Log("���Կ��� �������� " + gameObject.name + " ���� ����: " + buildingData.buildingCost);///
        //Debug.Log("���� ��������" + currentBuildingData.buildingCost);///
        Debug.Log("store slot ����Ÿ��" + currentBuildingType);
    }

    //����� ����
    public void SetSlotFarm(FarmDataInfo farmData)
    {
        Debug.Log("����������" + $"Setting farm data for slot {gameObject.name}: {farmData.farmName}");

        itemName.text = farmData.farmName;
        itemCost.text = farmData.farmCost.ToString();
        itemImage.sprite = farmData.farmImage[0];
        currentFarmType = farmData.farmType;
        currentBuildingType = BuildingType.None;
        currentAnimalType = AnimalType.None;

        //currentFarmData = farmData; //currentFarmData ������Ʈ
        //SelectedFarmData = farmData; // ���� ���� ������Ʈ


        if (itemSpriteRenderer != null) //��������Ʈ �������� �ִٸ� ��������Ʈ ����
        {
            itemSpriteRenderer.sprite = farmData.farmImage[0];
            currentPrefab = farmData.farmPrefab;
        }

        currentFarmData = farmData; //currentFarmData ������Ʈ
        currentBuildingData = new BuildingDataInfo();
        //SelectedFarmData = farmData;

        //Debug.Log("���� ���� �ҷ����� ���� " + farmData.farmCost);
    }

    //���� ����
    public void SetSlotAnimal(AnimalDataInfo animalData)
    {
        itemName.text = animalData.animalName;
        itemCost.text = animalData.animalCost.ToString();
        itemImage.sprite = animalData.animalImage[0];
        currentAnimalType = animalData.animalType;
        currentBuildingType = BuildingType.None;
        currentFarmType = FarmType.None;

        if (itemSpriteRenderer != null) //��������Ʈ �������� �ִٸ� ��������Ʈ ����
        {
            itemSpriteRenderer.sprite = animalData.animalImage[0];
            currentPrefab = animalData.animalPrefab;
        }

        currentAnimalData = animalData; //currentAnimalData ������Ʈ
        currentBuildingData = new BuildingDataInfo();
        currentFarmData = new FarmDataInfo();
        //Debug.Log("���� ���� �ҷ����� ���� " + animalData.animalCost);
    }


    // *�巡�� ���*

    //�巡�� ���۽�
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("���� Ȯ�� : �巡�� ");
        //Debug.Log("OnBeginDrag: " + currentBuildingData.buildingName);

        if (currentPrefab != null)
        {
            //�ǹ� ������ ���� �� �ʱ�ȭ
            clone = Instantiate(currentPrefab);
            clone.transform.position = GetWorldPosition(eventData);

            // ������ �ǹ��� �ʱ�ȭ�մϴ�.
            WorkBuilding workBuilding = clone.GetComponent<WorkBuilding>();
            if (workBuilding != null)
            {
                if (currentBuildingType != BuildingType.None)
                {
                    workBuilding.Initialize(currentBuildingType);
                }
                else if (currentFarmType != FarmType.None)
                {
                    workBuilding.Initialize(currentFarmType);
                    SelectedFarmData = currentFarmData; 
                    //Debug.Log("���徲��");
                }
                else if (currentAnimalType != AnimalType.None)
                {
                    workBuilding.Initialize(currentAnimalType);
                }
            }

            //������ ��������Ʈ ������ ����
            SpriteRenderer cloneSpriteRenderer = clone.GetComponent<SpriteRenderer>();
            if (cloneSpriteRenderer != null)
            {
                cloneSpriteRenderer.sprite = itemImage.sprite;
            }
            BoxColliderSize(clone);
            //Debug.Log("���Կ��� �巡�� ����: " + gameObject.name + " ���� ����: " + currentBuildingData.buildingCost);
            //Debug.Log("�巡������ �ǹ� ���� ����: " + JsonUtility.ToJson(currentBuildingData));
        }
    }

    //�巡�� ��
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag: " + currentBuildingData.buildingName);

        if (clone != null)
        {
            Vector3 mousePosition = GetWorldPosition(eventData);
            clone.transform.position = IsoGridSnap(mousePosition);
        }


    }

    //�巡�� ���� ��
    public void OnEndDrag(PointerEventData eventData)
    {
        if (clone != null)
        {
            WorkBuilding workBuilding = clone.GetComponent<WorkBuilding>();

            if (clone != null)
            {
                Vector3 mousePosition = GetWorldPosition(eventData);
                clone.transform.position = IsoGridSnap(mousePosition);
            }

            bool isSuccess = false; // �ݾ� ���� ���� ���� Ȯ�� ����

            if (currentBuildingType != BuildingType.None)
            {
                isSuccess = MoneySystem.Instance.DeductGold(currentBuildingData.buildingCost);
            }
            else if (currentFarmType != FarmType.None)
            {
                isSuccess = MoneySystem.Instance.DeductGold(currentFarmData.farmCost);
            }
            else if (currentAnimalType != AnimalType.None)
            {
                isSuccess = MoneySystem.Instance.DeductGold(currentAnimalData.animalCost);
            }

            if (!isSuccess)
            {
                // ���� �����ؼ� �ǹ��� ��ġ�� �� �����ϴ�. ���� �ǹ��� �����ϰ� ��� �޽��� ǥ��
                Destroy(clone);
                Debug.LogWarning("�ݾ��� �����Ͽ� �ǹ��� ��ġ�� �� �����ϴ�.");
                // �߰������� ����ڿ��� �޽����� ǥ���Ϸ���, ���⿡ �ڵ带 �߰��ϸ� �˴ϴ�.
            }
            else
            {
                // �ݾ� ������ ���������Ƿ�, �ǹ� ��ġ�� �����մϴ�.
                clone = null;
            }
        }

        //if (clone != null)
        //{
        //    WorkBuilding workBuilding = clone.GetComponent<WorkBuilding>();

        //    if (clone != null)
        //    {
        //        Vector3 mousePosition = GetWorldPosition(eventData);
        //        clone.transform.position = IsoGridSnap(mousePosition);
        //    }
        //    if (currentBuildingType != BuildingType.None)
        //    {
        //        MoneySystem.Instance.DeductGold(currentBuildingData.buildingCost);
        //    }
        //    else if (currentFarmType != FarmType.None)
        //    {
        //        MoneySystem.Instance.DeductGold(currentFarmData.farmCost);
        //    }
        //    else if (currentAnimalType != AnimalType.None)
        //    {
        //        MoneySystem.Instance.DeductGold(currentAnimalData.animalCost);
        //    }

        //    //�巡�װ� ������ ������ ���� ������Ʈ�� ����
        //    clone = null;

        //    //Debug.Log(buildingComponent.buildingType);

        //    //Debug.Log("���� ���� ����" + currentBuildingData.buildingCost);
        //    MoneySystem.Instance.DeductGold(currentBuildingData.buildingCost);
        //}

        ////Debug.Log(buildingComponent.buildingType);

    }

    private Vector3 GetWorldPosition(PointerEventData eventData)
    {
        //���콺�� ��ũ�� ��ġ�� ���� ��ġ�� ��ȯ
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(eventData.position);
        worldPosition.z = 0; //2D �����̶� z ��ǥ 0���� ����
        return worldPosition;
    }

    //���̼Ҹ�Ʈ�� ����
    private Vector3 IsoGridSnap(Vector3 position)
    {
        float gridSizeX = 0.84f;
        float gridSizeY = 0.47f;

        float isoX = (position.x / gridSizeX) - (position.y / gridSizeY);
        float isoY = (position.x / gridSizeX) + (position.y / gridSizeY);

        int snappedIsoX = Mathf.RoundToInt(isoX);
        int snappedIsoY = Mathf.RoundToInt(isoY);

        float worldX = (snappedIsoX + snappedIsoY) * gridSizeX / 2;
        float worldY = (-snappedIsoX + snappedIsoY) * gridSizeY / 2;

        return new Vector3(worldX, worldY, position.z);
    }

    //�ڽ� �ݶ��̴� 
    private void BoxColliderSize(GameObject buildingClone)
    {
        //��������Ʈ ������ ��������
        SpriteRenderer spriteRenderer = buildingClone.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) return;

        //BoxCollider2D�� �������ų�, ������ �߰�
        BoxCollider2D collider = buildingClone.GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            collider = buildingClone.AddComponent<BoxCollider2D>();
        }

        //Box Collider�� ũ�⸦ SpriteRenderer�� bounds ũ��� ����
        collider.size = spriteRenderer.bounds.size;
    }

}

