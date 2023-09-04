using JinnyAnimal;
using JinnyBuilding;
using JinnyCropItem;
using JinnyFarm;
using JinnyProcessItem;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

//by.J:230811 복제된 건물 관련 정보 / 생산품 제작
//by.J:230816 레시피 적용 추가
//by.J:230825 타입 설정 추가
//by.J:230827 완성품 이미지 드래그 드롭시 생산 시작
//by.J:230829 생산완료시 반짝반짝 효과
//by.J:230901 생산동안 커졌다 원래대로 반복
public class WorkBuilding : MonoBehaviour
{
    public bool isProducing = false;                            //생산중
    public float startTime;                                     //생산시작
    private float productionDuration;                           //생산 필요 시간
    public List<IItem> ingredientList = new List<IItem>();      //필요 재료 목록
    public List<IItem> needIngredient = new List<IItem>();      //생산에 필요한 재료 목록
    public IItem product;                                       //생산 완료 아이템
    public BuildingType buildingType;                           //생산 건물 타입
    public FarmType farmType;                                   //농장 타입
    public AnimalType animalType;                               //축사 타입

    public Recipe currentRecipe;                                //현재 건물에서 사용할 레시피

    public Sprite finishedProductImage;                         //완성품 이미지
    public GameObject finishedProductUI;                        //드래그 할 완성품 창

    private SpriteRenderer spriteRenderer;
    private Color originalColor;

    private List<Recipe> availableRecipes = new List<Recipe>();

    private Coroutine productionCoroutine;

    public GameObject copyBuilding;
    private Transform currentClickedFinishImage;
    public List<Image> productImageDisplays = new List<Image>();


    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            originalColor = spriteRenderer.color;
        }
    }

    void Start()
    {
        ////////////////////////////
        SetRecipesForBuilding();
        for (int i = 0; i < productImageDisplays.Count; i++)
        {
            AddEventTriggerToImage(productImageDisplays[i], i);
        }
        //////////////////////
    }

    private void Update()
    {
        CheckProduction();
    }

    //빌딩 타입 자동 설정
    public void Initialize(BuildingType type)
    {
        this.buildingType = type;
        this.farmType = FarmType.None;
        this.animalType = AnimalType.None;

    }

    //농장 타입 자동 설정
    public void Initialize(FarmType type)
    {
        this.farmType = type;
        this.buildingType = BuildingType.None;
        this.animalType = AnimalType.None;
    }

    //축사 타입 자동 설정
    public void Initialize(AnimalType type)
    {
        this.animalType = type;
        this.farmType = FarmType.None;
        this.buildingType = BuildingType.None;
    }

    // 이 메서드는 needIngredient 리스트에 아이템을 추가합니다.
    private void AddToNeedIngredient(IItem item)
    {
        if (item != null)
        {
            needIngredient.Add(item);
            Debug.Log($"Added {item.ItemName} to needIngredient list.");
        }
    }

    // 이 메서드는 needIngredient 리스트를 초기화합니다.
    private void ClearNeedIngredient()
    {
        needIngredient.Clear();
        Debug.Log($"Cleared needIngredient list.");
    }


    //빌딩 타입별 레시피 설정
    private void SetRecipesForBuilding()
    {
        

        Debug.Log("타입별 레시피 설정" + $"Setting recipes for building type: {buildingType}");

        if (RecipeManager.Instance.buildingRecipes.TryGetValue(buildingType, out List<Recipe> recipesForThisBuilding))
        {
            availableRecipes = recipesForThisBuilding;
        }
        else
        {
            Debug.Log("No recipes found for building type: " + buildingType);
        }
    }



    //레시피 선택
    public void SelectRecipe(Recipe recipe)
    {
        Debug.Log("레시피 선택");
        Debug.Log($"Before Clearing: needIngredient count = {needIngredient.Count}");
        ClearNeedIngredient();  // ClearNeedIngredient 메서드로 리스트 초기화
        Debug.Log($"After Clearing: needIngredient count = {needIngredient.Count}");

        if (availableRecipes.Contains(recipe))
        {
            Debug.Log($"Recipe {recipe.finishedProduct.processItemName} is available for this building.");

            ClearNeedIngredient();
            SetRecipeDetails(recipe);
        }
        else
        {
            Debug.Log("This building can't use this recipe!");
        }
    }

    private void SetRecipeDetails(Recipe recipe)
    {
        Debug.Log($"Setting recipe for: {recipe.finishedProduct.processItemName}");

        currentRecipe = recipe;
        ClearNeedIngredient();  // ClearNeedIngredient 메서드로 리스트 초기화

        foreach (var ingredientObj in recipe.ingredients)
        {
            if (ingredientObj is Ingredient<ProcessItemDataInfo> processedIngredient)
            {
                AddToNeedIngredient(new ProcessItemIItem(processedIngredient.item));  // AddToNeedIngredient 메서드로 아이템 추가
            }
            else if (ingredientObj is Ingredient<CropItemDataInfo> cropIngredient)
            {
                AddToNeedIngredient(new CropItemIItem(cropIngredient.item));  // AddToNeedIngredient 메서드로 아이템 추가
            }
        }

        product = new ProcessItemIItem(recipe.finishedProduct);
        productionDuration = recipe.productionTime;
        Debug.Log("Setting productionDuration to: " + recipe.productionTime);



    }


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
        Recipe clickedRecipe = RecipeManager.Instance.buildingRecipes[currentBuildingType][index];

        Debug.Log("클릭된 레시피" + clickedRecipe);

        //현재 클릭된 완성품 이미지 업뎃
        currentClickedFinishImage = productImageDisplays[index].transform;
        Debug.Log("클릭된 이미지 인덱스" + productImageDisplays[index].transform);
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

    



    //레시피 설정
    public void SetRecipe(Recipe recipe)
    {
        Debug.Log("레시피 설정");
        if (recipe == null)
        {
            Debug.LogError("Provided recipe is null!");
            return;
        }

        if (!recipe.IsInitialized)
        {
            Debug.LogError("Recipe is not initialized!");
            return;
        }

        if (!recipe.finishedProduct.IsInitialized)
        {

            Debug.LogError("recipe.finishedProduct is not initialized!");
            return;
        }

        ClearNeedIngredient();

        foreach (var ingredientObj in recipe.ingredients)
        {
            if (ingredientObj is Ingredient<ProcessItemDataInfo> processedIngredient)
            {
                needIngredient.Add(new ProcessItemIItem(processedIngredient.item));
            }
            else if (ingredientObj is Ingredient<CropItemDataInfo> cropIngredient)
            {
                needIngredient.Add(new CropItemIItem(cropIngredient.item));
            }
        }

        product = new ProcessItemIItem(recipe.finishedProduct);
        productionDuration = recipe.productionTime;
        Debug.Log("Setting productionDuration to: " + recipe.productionTime);

    }

    //재료 추가
    public void AddItem(IItem item)
    {
        if (buildingType == BuildingType.None) return;

        if (!isProducing && needIngredient.Contains(item)) //레시피에 필요한 재료인지 확인 및 생산 중이 아닌지 확인
        {
            ingredientList.Add(item);

            //모든 재료가 추가되었는지 확인
            if (ingredientList.Count == needIngredient.Count)
            {
                StartProduction();
            }
        }
        else
        {
            //필요하지 않은 재료
        }
    }



    // 드래그로 완성품 이미지를 놓았을 때 호출되는 메서드
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("완성품 이미지 놓고 호출");

        if (eventData.pointerDrag == finishedProductUI)
        {
            StartProduction();
        }
    }


    //생산 시작
    public void StartProduction()
    {
        Debug.Log("생산 시작");


        Debug.Log($"Before Deduction: needIngredient count = {needIngredient.Count}");
        foreach (var item in needIngredient)
        {
            Debug.Log(item.ItemName);
        }

        // 현재 선택된 레시피와 필요 원재료 디버그 로그 추가
        Debug.Log($"Selected Recipe: {currentRecipe.finishedProduct.processItemName}");
        Debug.Log("Required Ingredients for this recipe:");
      
        Debug.Log("재료 충분?");


        bool enoughIngredients = true;//////////////////
        foreach (IItem requiredItem in needIngredient)
        {
            int requiredCount = needIngredient.Count(item => item.Equals(requiredItem));
            int availableCount = Storage.Instance.GetItemAmount(requiredItem);

            if (availableCount < requiredCount)
            {
                Debug.Log($"재료 {requiredItem.ItemName} 가 부족합니다!");
                enoughIngredients = false;
                break;
            }

        }

        // 필요한 재료가 충분하지 않다면, 생산을 시작하지 않는다.
        if (!enoughIngredients) return;

        HashSet<IItem> processedItems = new HashSet<IItem>();
        // 필요한 재료를 창고에서 제거
        foreach (IItem requiredItem in needIngredient)
        {
            //int requiredCount = needIngredient.Count(item => item.Equals(requiredItem));
            //Storage.Instance.RemoveItem(requiredItem, requiredCount);
            if (!processedItems.Contains(requiredItem))
            {
                int requiredCount = needIngredient.Count(item => item.Equals(requiredItem));
                Storage.Instance.RemoveItem(requiredItem, requiredCount);
                processedItems.Add(requiredItem);
            }
        }



        Debug.Log($"After Deduction: needIngredient count = {needIngredient.Count}");
        foreach (var item in needIngredient)
        {
            Debug.Log(item.ItemName);
        }



        // 애니메이션 시작
        StartCoroutine(StartProductionAnimation());
        //이미지 숨기기
        if (finishedProductUI)
        {
            finishedProductUI.SetActive(false);
        }

        isProducing = true;
        startTime = Time.time;
        Debug.Log(isProducing);
        Debug.Log("Start Time: " + startTime);
        Debug.Log("Production Duration: " + productionDuration);
        Debug.Log("Current Time.time: " + Time.time);

        ClearNeedIngredient();

        if (productionCoroutine != null)
            StopCoroutine(productionCoroutine); // 이미 실행 중인 애니메이션 코루틴이 있으면 중지

        productionCoroutine = StartCoroutine(StartProductionAnimation()); // 애니메이션 시작
    }



    //생산 완료 체크
    private void CheckProduction()
    {
        if (isProducing && Time.time - startTime >= productionDuration)
        {
            Debug.Log("Inside production check condition.");
            isProducing = false;
            CompleteProduction();
        }

    }

    //생산 완료
    private void CompleteProduction()
    {
        Debug.Log("생산 완료");


        // 완성품을 창고에 추가
        Storage.Instance.AddItem(product, 1);

        //재료 목록 초기화
        ingredientList.Clear();

        if (spriteRenderer != null)
        {
            StartCoroutine(BlinkEffect());
        }

        if (productionCoroutine != null)
            StopCoroutine(productionCoroutine); // 애니메이션 중지

    }
 
    //반짝반짝 효과
    IEnumerator BlinkEffect()
    {
        int blinkTimes = 5;
        float blinkDuration = 0.1f;

        for (int i = 0; i < blinkTimes; i++)
        {
            // 투명하게 설정
            spriteRenderer.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0.5f);
            yield return new WaitForSeconds(blinkDuration);

            // 원래 색상으로 설정
            spriteRenderer.color = originalColor;
            yield return new WaitForSeconds(blinkDuration);
        }
    }

    //커졌다 줄었다
    IEnumerator StartProductionAnimation()
    {
        Vector3 originalScale = transform.localScale;
        Vector3 targetScale = originalScale * 1.2f; // 1.2배 크기로 설정 (원하는 배율로 조절 가능)

        float duration = 0.5f; // 애니메이션 지속 시간

        while (isProducing) // 생산 중일 동안 계속 애니메이션 반복
        {
            float elapsed = 0;
            // 커지는 애니메이션
            while (elapsed < duration)
            {
                transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.localScale = targetScale;

            elapsed = 0;
            // 원래 크기로 돌아오는 애니메이션
            while (elapsed < duration)
            {
                transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsed / duration);
                elapsed += Time.deltaTime;
                yield return null;
            }
            transform.localScale = originalScale;
        }

        //float elapsed = 0;

        //// 커지는 애니메이션
        //while (elapsed < duration)
        //{
        //    transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsed / duration);
        //    elapsed += Time.deltaTime;
        //    yield return null;
        //}
        //transform.localScale = targetScale;

        //elapsed = 0;
        //// 원래 크기로 돌아오는 애니메이션
        //while (elapsed < duration)
        //{
        //    transform.localScale = Vector3.Lerp(targetScale, originalScale, elapsed / duration);
        //    elapsed += Time.deltaTime;
        //    yield return null;
        //}
        //transform.localScale = originalScale;
    }
}
