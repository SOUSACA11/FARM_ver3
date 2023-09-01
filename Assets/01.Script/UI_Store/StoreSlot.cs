using JinnyAnimal;
using JinnyBuilding;
using JinnyFarm;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//by.J:230810 상점 슬롯 정보 / 상점 건물 드래그
//by.J:230817 건물 드래그 시 스냅 활성화
//by.J:230825 아이소메트릭 셀 사이즈에 맞춘 스냅으로 변경
//타입 변경시 이벤트 추가
public class StoreSlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    private Camera mainCamera;                  //메인 카메라
    public Image itemImage;                     //아이템 이미지
    public TextMeshProUGUI itemName;            //아이템 이름
    public TextMeshProUGUI itemCost;            //아이템 가격
    public GameObject currentPrefab;            //배치할 아이템 프리팹
    private GameObject clone;                   //드래그 중인 건물 복제본
    private SpriteRenderer itemSpriteRenderer;  //아이템 스프라이트 렌더러 이미지
    public BuildingType currentBuildingType;    //건물 타입
    public FarmType currentFarmType;            //농장 타입
    public AnimalType currentAnimalType;        //축사 타입

    private BuildingDataInfo currentBuildingData = new BuildingDataInfo(); //현재 설정 건물 데이터
    private FarmDataInfo currentFarmData = new FarmDataInfo();             //현재 설정 농장밭 데이터
    private AnimalDataInfo currentAnimalData = new AnimalDataInfo();       //현재 설정 동물 데이터


    public static FarmDataInfo? SelectedFarmData = null;
    public static AnimalDataInfo? SelectedAnimalData = null;


    public delegate void BuildingTypeChangedDelegate(BuildingType newType);
    public static event BuildingTypeChangedDelegate OnBuildingTypeChanged;

    private void Awake()
    {
        mainCamera = Camera.main; //메인 카메라 가져오기
        itemSpriteRenderer = GetComponent<SpriteRenderer>(); //스프라이트 렌더러 가져오기
    }

    //초기화
    public void ResetSlot()
    {
        
        //변수 초기화
        SelectedFarmData = null;
        SelectedAnimalData = null;
    }

    //빌딩 정보
    public void SetSlotBuilding(BuildingDataInfo buildingData)
    {
        //Debug.Log("SetSlotBuilding 되나염 " + buildingData.buildingName);

        itemName.text = buildingData.buildingName;
        itemCost.text = buildingData.buildingCost.ToString();
        itemImage.sprite = buildingData.buildingImage;
        currentBuildingType = buildingData.buildingType;
        currentFarmType = FarmType.None;
        currentAnimalType = AnimalType.None;

        //Debug.Log("store slot 빌딩타입" + currentBuildingType);
        ////이벤트 발생
        //OnBuildingTypeChanged?.Invoke(currentBuildingType);

        if (itemSpriteRenderer != null) //스프라이트 렌더러가 있다면 스프라이트 설정
        {
            itemSpriteRenderer.sprite = buildingData.buildingImage;
            currentPrefab = buildingData.buildingPrefab;
        }

        currentBuildingData = buildingData; //currentBuildingDat 업데이트
        currentFarmData = new FarmDataInfo();
        //Debug.Log("빌딩 가격 불러오기 성공 " + buildingData.buildingCost);///
        // Debug.Log("슬롯에서 빌딩정보 " + gameObject.name + " 빌딩 가격: " + buildingData.buildingCost);///
        //Debug.Log("현재 빌딩가격" + currentBuildingData.buildingCost);///
        Debug.Log("store slot 빌딩타입" + currentBuildingType);
    }

    //농장밭 정보
    public void SetSlotFarm(FarmDataInfo farmData)
    {
        Debug.Log("ㅋㅋㅋㅋㅋ" + $"Setting farm data for slot {gameObject.name}: {farmData.farmName}");

        itemName.text = farmData.farmName;
        itemCost.text = farmData.farmCost.ToString();
        itemImage.sprite = farmData.farmImage[0];
        currentFarmType = farmData.farmType;
        currentBuildingType = BuildingType.None;
        currentAnimalType = AnimalType.None;

        //currentFarmData = farmData; //currentFarmData 업데이트
        //SelectedFarmData = farmData; // 정적 변수 업데이트


        if (itemSpriteRenderer != null) //스프라이트 렌더러가 있다면 스프라이트 설정
        {
            itemSpriteRenderer.sprite = farmData.farmImage[0];
            currentPrefab = farmData.farmPrefab;
        }

        currentFarmData = farmData; //currentFarmData 업데이트
        currentBuildingData = new BuildingDataInfo();
        //SelectedFarmData = farmData;

        //Debug.Log("농장 가격 불러오기 성공 " + farmData.farmCost);
    }

    //동물 정보
    public void SetSlotAnimal(AnimalDataInfo animalData)
    {
        itemName.text = animalData.animalName;
        itemCost.text = animalData.animalCost.ToString();
        itemImage.sprite = animalData.animalImage[0];
        currentAnimalType = animalData.animalType;
        currentBuildingType = BuildingType.None;
        currentFarmType = FarmType.None;

        if (itemSpriteRenderer != null) //스프라이트 렌더러가 있다면 스프라이트 설정
        {
            itemSpriteRenderer.sprite = animalData.animalImage[0];
            currentPrefab = animalData.animalPrefab;
        }

        currentAnimalData = animalData; //currentAnimalData 업데이트
        currentBuildingData = new BuildingDataInfo();
        currentFarmData = new FarmDataInfo();
        //Debug.Log("동물 가격 불러오기 성공 " + animalData.animalCost);
    }


    // *드래그 기능*

    //드래그 시작시
    public void OnBeginDrag(PointerEventData eventData)
    {
        Debug.Log("순서 확인 : 드래그 ");
        //Debug.Log("OnBeginDrag: " + currentBuildingData.buildingName);

        if (currentPrefab != null)
        {
            //건물 복제본 생성 및 초기화
            clone = Instantiate(currentPrefab);
            clone.transform.position = GetWorldPosition(eventData);

            // 복제된 건물을 초기화합니다.
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
                    //Debug.Log("농장쓰스");
                }
                else if (currentAnimalType != AnimalType.None)
                {
                    workBuilding.Initialize(currentAnimalType);
                }
            }

            //복제본 스프라이트 렌더러 설정
            SpriteRenderer cloneSpriteRenderer = clone.GetComponent<SpriteRenderer>();
            if (cloneSpriteRenderer != null)
            {
                cloneSpriteRenderer.sprite = itemImage.sprite;
            }
            BoxColliderSize(clone);
            //Debug.Log("슬롯에서 드래그 시작: " + gameObject.name + " 빌딩 가격: " + currentBuildingData.buildingCost);
            //Debug.Log("드래그중인 건물 현재 가격: " + JsonUtility.ToJson(currentBuildingData));
        }
    }

    //드래그 중
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("OnDrag: " + currentBuildingData.buildingName);

        if (clone != null)
        {
            Vector3 mousePosition = GetWorldPosition(eventData);
            clone.transform.position = IsoGridSnap(mousePosition);
        }


    }

    //드래그 끝난 후
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

            bool isSuccess = false; // 금액 차감 성공 여부 확인 변수

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
                // 돈이 부족해서 건물을 배치할 수 없습니다. 따라서 건물을 제거하고 경고 메시지 표시
                Destroy(clone);
                Debug.LogWarning("금액이 부족하여 건물을 배치할 수 없습니다.");
                // 추가적으로 사용자에게 메시지를 표시하려면, 여기에 코드를 추가하면 됩니다.
            }
            else
            {
                // 금액 차감에 성공했으므로, 건물 배치를 유지합니다.
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

        //    //드래그가 끝나면 복제본 게임 오브젝트로 존재
        //    clone = null;

        //    //Debug.Log(buildingComponent.buildingType);

        //    //Debug.Log("현재 빌딩 가격" + currentBuildingData.buildingCost);
        //    MoneySystem.Instance.DeductGold(currentBuildingData.buildingCost);
        //}

        ////Debug.Log(buildingComponent.buildingType);

    }

    private Vector3 GetWorldPosition(PointerEventData eventData)
    {
        //마우스의 스크린 위치를 월드 위치로 변환
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(eventData.position);
        worldPosition.z = 0; //2D 게임이라 z 좌표 0으로 설정
        return worldPosition;
    }

    //아이소메트릭 스냅
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

    //박스 콜라이더 
    private void BoxColliderSize(GameObject buildingClone)
    {
        //스프라이트 렌더러 가져오기
        SpriteRenderer spriteRenderer = buildingClone.GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) return;

        //BoxCollider2D를 가져오거나, 없으면 추가
        BoxCollider2D collider = buildingClone.GetComponent<BoxCollider2D>();
        if (collider == null)
        {
            collider = buildingClone.AddComponent<BoxCollider2D>();
        }

        //Box Collider의 크기를 SpriteRenderer의 bounds 크기로 설정
        collider.size = spriteRenderer.bounds.size;
    }

}

