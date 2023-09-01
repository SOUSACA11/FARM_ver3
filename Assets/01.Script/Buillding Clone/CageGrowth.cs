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
//    private SpriteRenderer spriteRenderer;                         //축사 이미지
//    public AnimalDataInfo animalData;                              //현재 축사 데이터
//    private GrowthAnimalType currentStage = GrowthAnimalType.Baby; //현재 축사의 성장 단계
//    private float growthTimer = 1.0f;                              //축사 성장 추적 타이머
//    public Storage playerStorage;                                  //창고

//    private void Start()
//    {
//        //창고 가져오기
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

//    //축사 성장 데이터 초기화
//    public void Initialize(AnimalDataInfo data)
//    {
//        Debug.Log("축사 성장 데이터 초기화");

//        growthTimer = 0f; // 초기화
//        this.animalData = data;
//        spriteRenderer = GetComponent<SpriteRenderer>();
//        UpdateSprite();
//    }

//    //성장 체크
//    private void Update()
//    {
//        growthTimer += Time.deltaTime;

//        //Debug.Log("축사 성장 체크");

//        //성장 단계 변경하고 스프라이트를 업데이트
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

//        // 마우스 클릭 또는 터치 감지
//        if (Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began))
//        {
//            // 터치된 위치 또는 클릭된 위치 가져오기
//            Vector3 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
//            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

//            if (hit.collider != null && hit.collider.gameObject == this.gameObject)
//            {
//                HandleFarmTouchOrClick();
//            }
//        }

//        //Debug.Log("성장시간" + growthTimer);
//        //Debug.Log("농장밭 성장 시간" + farmData.farmGrowTime);
//    }

//    //현재 성장 단계에 따른 스프라이트 업뎃
//    private void UpdateSprite()
//    {
//        //Debug.Log("농장 성장 업뎃");

//        if (spriteRenderer && animalData.animalImage.Length > (int)currentStage)
//        {
//            spriteRenderer.sprite = animalData.animalImage[(int)currentStage];
//        }
//    }

//    //축사 클릭 시
//    void HandleFarmTouchOrClick()
//    {
//        Debug.Log("축사 클릭");

//        WorkBuilding buildingComponent = GetComponent<WorkBuilding>();

//        //빌딩 타입이 None 이고 팜타입이 None이며 축사 타입이 Cage 아니면 return
//        if (buildingComponent == null || (buildingComponent.buildingType != BuildingType.None || buildingComponent.farmType != FarmType.None || buildingComponent.animalType != AnimalType.Cage))
//        {
//            Debug.Log("조건이 만족되지 않습니다.");
//            return; // 아무것도 하지 않습니다.
//        }

//        //빌딩 타입이 None 이고 팜타입이 None이며 축사 타입이 Cage 경우만 실행
//        if (IngredientManagerUI.ProcessedBuildingClick)//빌딩 타입 할당시 끝
//            return;



//        if (currentStage == GrowthAnimalType.Baby) //완전히 자란 상태
//        {
//            CollectAnimalItem();
//        }
//    }

//    //작물 획득
//    void CollectAnimalItem()
//    {
//        Debug.Log("생산품 획득");
//        if (playerStorage == null)
//        {
//            Debug.LogError("창고 없음");
//            return;
//        }

//        //현재 축사의 데이터를 기반으로 ProcessItem에서 해당 아이템을 찾는다.
//        JinnyProcessItem.ProcessItemDataInfo animalToCollect = JinnyProcessItem.ProcessItem.Instance.processItemDataInfoList.Find(item => item.processItemId == animalData.animalProcessItemId);

//        if (animalToCollect.processItemId == null)
//        {
//            Debug.LogError("연관 작물 아이템 없음");
//            return;
//        }

//        //창고에 생산품 아이템을 추가
//        if (playerStorage.AddItem(animalToCollect, 1))
//        {
//            Debug.Log("창고에 작물 추가");

//            //축사 초기화 및 스프라이트 업데이트
//            currentStage = GrowthAnimalType.Baby;
//            growthTimer = 0f;
//            UpdateSprite();
//        }
//        else
//        {
//            Debug.Log("창고 꽉 참");
//        }

//    }
//}


