//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using JinnyAnimal;
//using UnityEngine.EventSystems;
//using JinnyCropItem;
//using JinnyBuilding;
//using JinnyFarm;

//public class CageGrowth : MonoBehaviour
//{
//    private SpriteRenderer spriteRenderer;                         //��� �̹���
//    public AnimalDataInfo animalData;                              //���� ��� ������
//    private GrowthAnimalType currentStage = GrowthAnimalType.Baby; //���� ����� ���� �ܰ�
//    private float growthTimer = 1.0f;                              //��� ���� ���� Ÿ�̸�
//    public Storage playerStorage;                                  //â��

//    private void Start()
//    {
//        //â�� ��������
//        if (playerStorage == null)
//        {
//            playerStorage = Storage.Instance;
//        }
//        InitializeFromSelectedData();
//    }

//    public void InitializeFromSelectedData()
//    {
//        if (StoreSlot.SelectedAnimalData != null)
//        {
//            AnimalDataInfo selectedAnimalData = StoreSlot.SelectedAnimalData.Value;
//            Initialize(selectedAnimalData);
//        }
//    }

//    //��� ���� ������ �ʱ�ȭ
//    public void Initialize(AnimalDataInfo data)
//    {
//        Debug.Log("��� ���� ������ �ʱ�ȭ");

//        growthTimer = 0f; // �ʱ�ȭ
//        this.animalData = data;
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        UpdateSprite();
//    }

//    //���� üũ
//    private void Update()
//    {
//        growthTimer += Time.deltaTime;

//        //Debug.Log("��� ���� üũ");

//        //���� �ܰ� �����ϰ� ��������Ʈ�� ������Ʈ
//        if (currentStage == GrowthAnimalType.Baby && growthTimer >= animalData.animalGrowTime / 3)
//        {
//            currentStage = GrowthAnimalType.Child;
//            UpdateSprite();
//        }
//        else if (currentStage == GrowthAnimalType.Child && growthTimer >= animalData.animalGrowTime)
//        {
//            currentStage = GrowthAnimalType.Adult;
//            UpdateSprite();
//        }
//        else if (currentStage == GrowthAnimalType.Adult && growthTimer >= animalData.animalGrowTime)
//        {
//            currentStage = GrowthAnimalType.Man;
//            UpdateSprite();
//        }

//        // ���콺 Ŭ�� �Ǵ� ��ġ ����
//        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
//        {
//            // ��ġ�� ��ġ �Ǵ� Ŭ���� ��ġ ��������
//            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

//            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
//            {
//                HandleFarmTouchOrClick();
//            }
//        }

//        //Debug.Log("����ð�" + growthTimer);
//        //Debug.Log("����� ���� �ð�" + farmData.farmGrowTime);
//    }

//    //���� ���� �ܰ迡 ���� ��������Ʈ ����
//    private void UpdateSprite()
//    {
//        //Debug.Log("���� ���� ����");

//        if (spriteRenderer && animalData.animalImage.Length > (int)currentStage)
//        {
//            spriteRenderer.sprite = animalData.animalImage[(int)currentStage];
//        }
//    }

//    //��� Ŭ�� ��
//    void HandleFarmTouchOrClick()
//    {
//        Debug.Log("��� Ŭ��");

//        WorkBuilding buildingComponent = GetComponent<WorkBuilding>();

//        //���� Ÿ���� None �̰� ��Ÿ���� None�̸� ��� Ÿ���� Cage �ƴϸ� return
//        if (buildingComponent == null || (buildingComponent.buildingType != BuildingType.None || buildingComponent.farmType != FarmType.None || buildingComponent.animalType != AnimalType.Cage))
//        {
//            Debug.Log("������ �������� �ʽ��ϴ�.");
//            return; // �ƹ��͵� ���� �ʽ��ϴ�.
//        }

//        //���� Ÿ���� None �̰� ��Ÿ���� None�̸� ��� Ÿ���� Cage ��츸 ����
//        if (IngredientManagerUI.ProcessedBuildingClick)//���� Ÿ�� �Ҵ�� ��
//            return;



//        if (currentStage == GrowthAnimalType.Baby) //������ �ڶ� ����
//        {
//            CollectAnimalItem();
//        }
//    }

//    //�۹� ȹ��
//    void CollectAnimalItem()
//    {
//        Debug.Log("����ǰ ȹ��");
//        if (playerStorage == null)
//        {
//            Debug.LogError("â�� ����");
//            return;
//        }

//        //���� ����� �����͸� ������� ProcessItem���� �ش� �������� ã�´�.
//        JinnyProcessItem.ProcessItemDataInfo animalToCollect = JinnyProcessItem.ProcessItem.Instance.processItemDataInfoList.Find(item => item.processItemId == animalData.animalProcessItemId);

//        if (animalToCollect.processItemId == null)
//        {
//            Debug.LogError("���� �۹� ������ ����");
//            return;
//        }

//        //â�� ����ǰ �������� �߰�
//        if (playerStorage.AddItem(animalToCollect, 1))
//        {
//            Debug.Log("â�� �۹� �߰�");

//            //��� �ʱ�ȭ �� ��������Ʈ ������Ʈ
//            currentStage = GrowthAnimalType.Baby;
//            growthTimer = 0f;
//            UpdateSprite();
//        }
//        else
//        {
//            Debug.Log("â�� �� ��");
//        }

//    }
//}


