using JinnyBuilding;
using JinnyProcessItem;
using JinnyCropItem;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using JinnyAnimal;

//by.J:230817 건물에 원재료 넣는 UI / 원재료 창 이동
//by.J:230818 완성품 이미지 ID 추가
//by.J:230824 완성품 이미지 클릭시 원재료 이미지 나타나기
//by.J:230829 건물 복제본 클릭시 빌딩 타입이 할당 된 경우만 작동
//by.J:230901 완성품UI창 없어질시 원재료UI창 삭제 후 초기화
public class IngredientManagerUI : MonoBehaviour
{
    public GameObject copyBuilding;                   //건물 복제본
    public Vector3 uiOffset = new Vector3(-1, 1, 0);  //UI 위치 오프셋. 건물의 좌측 상단 모서리에 나타나게 만드는데 사용
    private Vector3 finishOriginalUIPosition;         //완성품 이미지 위치값

    public GameObject arrow;                      //Arrow 게임 오브젝트
    public RectTransform arrowRectTransform;      //Arrow 오브젝트 RectTransform
    public Image arrowImage;                      //Arrow 이미지
    public Transform copyFinishSlot;              //완성품 슬롯

    private GameObject clonedIngredientUI;        //원재료 UI창
    private Transform currentClickedFinishImage;  //현재 클릭된 finish Image(Clone)

    public static IngredientManagerUI Instance; //싱글톤
    public Recipe specificRecipe;               //레시피
    public IngredientSlot ingredientSlotPrefab; //원재료 프리팹
    public Image finishImagePrefab;             //완성품 이미지 프리팹
    public Image ingredientImagePrefab;         //원재료 이미지 프리팹

    public List<IngredientSlot> ingredientSlots = new List<IngredientSlot>(); //원재료 이미지 할당 슬롯
    public List<Image> productImageDisplays = new List<Image>();              //UI에 표시될 완성품 이미지

    public static bool ProcessedBuildingClick = false; //클릭 복제본

    private void Awake()
    {
        //싱글톤
        Debug.Log("원재료 스크립트");
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        finishOriginalUIPosition = transform.position;
    }

    private void Start()
    {
        for (int i = 0; i < productImageDisplays.Count; i++)
        {
            AddEventTriggerToImage(productImageDisplays[i], i);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            CheckClick();
        }
    }

    //이미지 트리거 추가
    void AddEventTriggerToImage(Image targetImage, int index)
    {
        Debug.Log("함수 추가 기능" + index);

        EventTrigger eventTrigger = targetImage.gameObject.GetComponent<EventTrigger>();
        if (eventTrigger == null)
            eventTrigger = targetImage.gameObject.AddComponent<EventTrigger>();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerClick;
        entry.callback.AddListener((eventData) => { ProductImageClicked(index); });

        eventTrigger.triggers.Clear();
        eventTrigger.triggers.Add(entry);
    }

    //건물 복제본 클릭시
    public void IngredientClick()
    {
        Debug.Log("건물 복제본 클릭");
        //레이 캐스팅해서 건물 클릭 인식
        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(mousePos, Vector2.zero);

        GameObject clickedBuilding = null;
        if (hit.collider != null)
        {
            Debug.Log("건물 클릭됨: " + hit.collider.gameObject.name);
            clickedBuilding = hit.collider.gameObject;
        }

        //클릭된 건물이 없으면 반환
        if (clickedBuilding == null) return;

        //클릭된 건물 copyBuilding에 저장
        copyBuilding = clickedBuilding;

        WorkBuilding buildingComponent = copyBuilding.GetComponent<WorkBuilding>();
        //BuildingType type = buildingComponent.buildingType;
        //AnimalType type2 = buildingComponent.animalType;
        Debug.Log(buildingComponent.buildingType);

        //빌딩 타입이 None이 아닌지 확인
        bool isBuildingNotNone = buildingComponent.buildingType == BuildingType.None;

        //축사 타입이 None이 아닌지 확인
        bool isAnimalNotNone =  buildingComponent.animalType == AnimalType.None;

        // 두 조건 중 하나라도 만족하면 실행
        if (buildingComponent == null || isBuildingNotNone && isAnimalNotNone)
        {
            Debug.Log("클릭된 건물 또는 축사의 타입이 None이 아님");
            ProcessedBuildingClick = false;
            return;
        }
       
        else
        {
            Debug.Log("클릭된 건물 또는 축사의 타입이 None");
            ProcessedBuildingClick = true;
        }
   
        Debug.Log("원재료 띠용");

        //완성품 UI 창 이동
        SetUIPosition(clickedBuilding);

        buildingComponent = clickedBuilding.GetComponent<WorkBuilding>();

        if (buildingComponent != null)
        {
            Debug.Log("완성품 이미지 뜸?");
            BuildingType buildingType = buildingComponent.buildingType; //빌딩 타입 저장
            AnimalType animalType = buildingComponent.animalType; //축사 타입 저장 

            if (RecipeManager.Instance.buildingRecipes.ContainsKey(buildingType))
            {
                List<Recipe> recipesForBuilding = RecipeManager.Instance.buildingRecipes[buildingType];

                //동적으로 이미지 슬롯 생성
                CreateProductImageSlots(recipesForBuilding.Count);
                Debug.Log("이미지 슬롯 생성이 되나?" + recipesForBuilding.Count);

                //이미지 할당
                for (int i = 0; i < recipesForBuilding.Count; i++)
                {
                    productImageDisplays[i].sprite = recipesForBuilding[i].FinishedProductImage;
                }
            }

            else if (RecipeManager.Instance.animalRecipes.ContainsKey(animalType))
            {
                List<Recipe> recipesForAnimal = RecipeManager.Instance.animalRecipes[animalType];

                //동적으로 이미지 슬롯 생성
                CreateProductImageSlots(recipesForAnimal.Count);
                Debug.Log("이미지 슬롯 생성이 되나?" + recipesForAnimal.Count);

                //이미지 할당
                for (int i = 0; i < recipesForAnimal.Count; i++)
                {
                    productImageDisplays[i].sprite = recipesForAnimal[i].FinishedProductImage;
                }
            }
            else
            {
                Debug.LogError("빌딩타입 " + buildingType + " is not present in buildingRecipes dictionary.");
            }
        }

        //ingredient 태그 가진 모든 인스턴스 찾기
        GameObject[] allIngredientUIs = GameObject.FindGameObjectsWithTag("ingredient");
        clonedIngredientUI = null;

        Debug.Log("순서 왔나?");

        //복제본 UI 찾기
        foreach (var ui in allIngredientUIs)
        {
            Debug.Log("복제본 찾기 중: " + ui.name);

            if (ui.name.StartsWith("finish Image(Clone)")) //이름이 정확히 "finish image(Clone)"로 시작하는 경우만 선택
            {
                clonedIngredientUI = ui;
                Debug.Log("선택된 복제본: " + clonedIngredientUI.name);
                break;
            }
        }

        //clonedIngredientUI가 없다면 새로 생성
        if (clonedIngredientUI == null)
        {
            clonedIngredientUI = Instantiate(ingredientSlotPrefab.gameObject, currentClickedFinishImage);
        }
        clonedIngredientUI.SetActive(true);

    }

    //완성품 클릭시 원재료 표시
    public void ShowfinishIngredient(Recipe recipe)
    {

        //원재료 표시
        Debug.Log("완성품 클릭 유도");
        ShowIngredient(recipe);
    }

    //완성품 이미지 클릭
    public void ProductImageClicked(int index)
    {
        Debug.Log("완성품 클릭 실행");

        Debug.Log("ProductImageClicked 호출됨.");
        if (copyBuilding == null)
        {
            Debug.LogError("copyBuilding이 null입니다.");
            return;
        }

        if (copyBuilding == null)
        {
            Debug.LogError("No building has been clicked yet.");
            return;
        }

        //클릭 이미지 인덱스로 해당 레시피 찾기 
        BuildingType currentBuildingType = copyBuilding.GetComponent<WorkBuilding>().buildingType;
        AnimalType currentAnimalType = copyBuilding.GetComponent<WorkBuilding>().animalType;

        Recipe clickedRecipe = null;

        if (currentBuildingType != BuildingType.None)
        {
            clickedRecipe = RecipeManager.Instance.buildingRecipes[currentBuildingType][index];
        }
        else if (currentAnimalType != AnimalType.None)
        {
            clickedRecipe = RecipeManager.Instance.animalRecipes[currentAnimalType][index];
        }

        else if (clickedRecipe == null)
        {
            Debug.LogError("Clicked recipe could not be retrieved.");
            return;
        }

        Debug.Log("클릭된 레시피" + clickedRecipe);

        DragFinishItem dragItem = productImageDisplays[index].GetComponent<DragFinishItem>();
        if (dragItem != null)
        {
            dragItem.SetCurrentRecipe(clickedRecipe);
        }


        //현재 클릭된 완성품 이미지 업뎃
        currentClickedFinishImage = productImageDisplays[index].transform;
        Debug.Log("클릭된 이미지 인덱스" + productImageDisplays[index].transform);

        //해당 레시피의 원재료 표시
        ShowfinishIngredient(clickedRecipe);

        //Ingredient Slot(Clone) 생성
        CreateIngredientSlot(clickedRecipe);

        //클릭된 이미지 위치 참조
        RectTransform clickedImageTransform = productImageDisplays[index].rectTransform;

        //Ingredient Slot(Clone)의 위치 설정 - 클릭된 이미지 아래
        for (int i = 0; i < ingredientSlots.Count; i++)
        {
            RectTransform slotRect = ingredientSlots[i].GetComponent<RectTransform>();

            //클릭된 이미지 아래에 Ingredient Slot(Clone) 배치
            float offsetY = (i + 1) * slotRect.sizeDelta.y;
            slotRect.position = new Vector3
                (currentClickedFinishImage.position.x, currentClickedFinishImage.position.y - offsetY, currentClickedFinishImage.position.z);
        }

        //이 부분에서 원재료 창을 활성화 시키도록 수정
        if (clonedIngredientUI != null)
        {
            clonedIngredientUI.SetActive(true);
          
        }
    }

    //레시피 별 원재료 보여주기
    public void ShowIngredient(Recipe recipe)
    {
        Debug.Log("원재료 함수쓰");

        for (int i = 0; i < ingredientSlots.Count; i++)
        {
            Debug.Log("0");
            if (i < recipe.ingredients.Count)
            {
                Debug.Log("1");
                object ingredientObj = recipe.ingredients[i];

                if (ingredientObj is Ingredient<IItem> itemIngredient)
                {
                    Debug.Log("2");
                    ingredientSlots[i].SetIngredient(itemIngredient.item, itemIngredient.quantity);
                    ingredientSlots[i].gameObject.SetActive(true);
                }
                else if (ingredientObj is Ingredient<ProcessItemDataInfo> processedIngredient)
                {
                    Debug.Log("3");
                    ingredientSlots[i].SetIngredient(new ProcessItemIItem(processedIngredient.item), processedIngredient.quantity);
                    ingredientSlots[i].gameObject.SetActive(true);
                }
                else
                {
                    Debug.Log("4");
                    ingredientSlots[i].gameObject.SetActive(false);
                }
            }
            else
            {
                Debug.Log("5");
                ingredientSlots[i].gameObject.SetActive(false);
            }

            Debug.Log("6");
        }

        Debug.Log("7");
    }

    //완성품 UI 창 이동
    public void SetUIPosition(GameObject targetBuilding)
    {
        Debug.Log("UI 이동 이동");
        if (targetBuilding == null) return;

        SpriteRenderer buildingRenderer = targetBuilding.GetComponent<SpriteRenderer>();
        if (buildingRenderer != null)
        {
            Bounds buildingBounds = buildingRenderer.bounds;

            //건물의 좌측 상단 모서리 위치에 오프셋 추가
            Vector3 targetPosition = buildingBounds.center + uiOffset;

            //UI창의 위치를 건물의 좌측 상단 모서리 맞게 조정
            transform.position = Camera.main.WorldToScreenPoint(targetPosition);

            Debug.Log("Setting UI to: " + transform.position.ToString());
        }
    }

    //완성품 이미지 슬롯 생성
    public void CreateProductImageSlots(int numberOfSlots)
    {
        Debug.Log("완성품 이미지 슬롯 생성");
        //기존 슬롯 비활성화
        foreach (Image img in productImageDisplays)
        {
            img.gameObject.SetActive(false);
        }

        //필요한 슬롯 활성화, 생성 
        for (int i = 0; i < numberOfSlots; i++)
        {
            if (i >= productImageDisplays.Count)
            {
                Image newImage = Instantiate(finishImagePrefab, transform);
                productImageDisplays.Add(newImage);
                Debug.Log("Image created at: " + Time.time);
            }
            else
            {
                productImageDisplays[i].gameObject.SetActive(true);
            }

            DragFinishItem dragItem = productImageDisplays[i].GetComponent<DragFinishItem>();
            if (dragItem != null)
            {
                dragItem.index = i;
            }


            //각 이미지에 EventTrigger 추가 및 인덱스 설정
            AddEventTriggerToImage(productImageDisplays[i], i);
        }
    }

    //원재료 슬롯 생성
    public void CreateIngredientSlot(Recipe recipe)
    {
        Debug.Log("원재료 슬롯 활성화?");

        // 현재 슬롯 모두 비활성화
        foreach (var slot in ingredientSlots)
        {
            Debug.Log("비활성화");
            Destroy(slot.gameObject);

        }
        ingredientSlots.Clear();

        //IngredientSlot 한 번 생성
        IngredientSlot newSlot = Instantiate(ingredientSlotPrefab, clonedIngredientUI.transform);
        newSlot.gameObject.SetActive(true);


        //복제된 슬롯에서 arrow 찾기
        Transform arrowChild = newSlot.transform.Find("arrow");
        if (arrowChild != null)
        {
            //arrow 오브젝트 제거
            Destroy(arrowChild.gameObject);
            Debug.Log("삭제");
        }

        //레시피 원재료 수에 따라 이미지 오브젝트 생성
        for (int i = 0; i < recipe.ingredients.Count; i++)
        {
            Debug.Log("원재료 이미지 추가");

            Image ingredientImage = Instantiate(ingredientImagePrefab, newSlot.transform);
            ingredientImage.gameObject.SetActive(true);

            object ingredientObj = recipe.ingredients[i];
            if (ingredientObj is Ingredient<CropItemDataInfo> cropIngredient)
            {
                Debug.Log("if문");
                ingredientImage.sprite = cropIngredient.item.cropItemImage;
                newSlot.SetIngredient(new CropItemIItem(cropIngredient.item), cropIngredient.quantity);
            }
            else if (ingredientObj is Ingredient<ProcessItemDataInfo> processedIngredient)
            {
                Debug.Log("else if문");
                ingredientImage.sprite = processedIngredient.item.processItemImage;
                newSlot.SetIngredient(new ProcessItemIItem(processedIngredient.item), processedIngredient.quantity);
            }
        }
        ingredientSlots.Add(newSlot);
        Debug.Log("원재료 슬롯 끝");
    }

    //클릭 확인
    void CheckClick()
    {
        bool isOverUI = false;

        // PC에서 마우스 클릭을 확인
        if (EventSystem.current.IsPointerOverGameObject())
        {
            isOverUI = true;
        }

        // 모바일 환경에서의 터치를 확인
        for (int i = 0; i < Input.touchCount; i++)
        {
            if (EventSystem.current.IsPointerOverGameObject(Input.GetTouch(i).fingerId))
            {
                isOverUI = true;
                break;
            }
        }

        if (!isOverUI)
        {
            CloseFinishUI();
        }
    }

    //완성품 창 초기화
    public void CloseFinishUI()
    {
        Debug.Log("완성품 창 원래대로");
        transform.position = finishOriginalUIPosition;

        //완성품 UI를 비활성화합니다.
        foreach (Image img in productImageDisplays)
        {
            img.gameObject.SetActive(false);
        }

        copyBuilding = null;

        //clonedIngredientUI 초기화
        if (clonedIngredientUI != null)
        {
            clonedIngredientUI.SetActive(false);
            Destroy(clonedIngredientUI); //클릭할 때마다 새로운 UI를 만들려면 기존의 것 파괴
            clonedIngredientUI = null;
        }
        productImageDisplays.Clear();
        ingredientSlots.Clear();
    }

}

