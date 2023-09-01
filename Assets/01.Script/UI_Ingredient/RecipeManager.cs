using System.Collections.Generic;
using UnityEngine;
using JinnyCropItem;
using JinnyProcessItem;
using JinnyBuilding;
using JinnyAnimal;

//by.J:230816 ������ ������
//by.J:230818 �ϼ�ǰ ID �߰�
//by.J:230829 ����ð� �߰�
public class RecipeManager : MonoBehaviour
{
    public static RecipeManager Instance; //�̱��� ó��
    public Dictionary<BuildingType, List<Recipe>> buildingRecipes = new Dictionary<BuildingType, List<Recipe>>(); //�ǹ� Ÿ��
    public Dictionary<AnimalType, List<Recipe>> animalRecipes = new Dictionary<AnimalType, List<Recipe>>();       //��� Ÿ��
    public Dictionary<string, Recipe> recipesByName = new Dictionary<string, Recipe>();

    public Recipe milkRecipe;         //����
    public Recipe eggRecipe;          //�ް�
    public Recipe porkRecipe;         //�������

    public Recipe breadRecipe;          //�Ļ�
    public Recipe baguetteRecipe;       //�ٰ�Ʈ
    public Recipe croissantRecipe;      //ũ��ͻ�
    public Recipe flourRecipe;          //�а���
    public Recipe chickenfeedRecipe;    //�� ���
    public Recipe pigfeedRecipe;        //���� ���
    public Recipe cowfeedRecipe;        //�� ���
    public Recipe eggflowerRecipe;      //����Ķ���
    public Recipe baconRecipe;          //������
    public Recipe tomatojuiceRecipe;    //�丶�� �꽺
    public Recipe carrotjuiceRecipe;    //��� �꽺
    public Recipe butterRecipe;         //����
    public Recipe cheeseRecipe;         //ġ��

    public CropItem cropItems;
    public ProcessItem processItems;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

  
            cropItems = FindObjectOfType<CropItem>();
            processItems = FindObjectOfType<ProcessItem>();
        

    }

    //ID ������� ������ ��������
    public Recipe GetRecipeByProductId(string productId) ///////���� �߰�
    {
        foreach (var recipes in buildingRecipes.Values)
        {
            foreach (var recipe in recipes)
            {
                if (recipe.finishedProductId == productId)
                {
                    return recipe;
                }
            }
        }
        return null;
    }

    ////�̸� ������� ������ ��������
    //public Recipe GetRecipeByName(string recipeName)
    //{
    //    if (recipesByName.ContainsKey(recipeName))
    //    {
    //        return recipesByName[recipeName];
    //    }
    //    return null;
    //}

    private void Start()
    {
        InitializeRecipes();
    }


    //������ ���� �ʱ�ȭ
    private void InitializeRecipes()
    {
        Debug.Log("�����ǸŴ��� �ʱ�ȭ �ǽ� ����");


        // JinnyCropItem���� ������ ��������
        CropItemDataInfo wheat = cropItems.cropItemDataInfoList.Find(item => item.cropItemId == "crop_01");  //��
        CropItemDataInfo corn = cropItems.cropItemDataInfoList.Find(item => item.cropItemId == "crop_02");   //������
        CropItemDataInfo bean = cropItems.cropItemDataInfoList.Find(item => item.cropItemId == "crop_03");   //��
        CropItemDataInfo tomato = cropItems.cropItemDataInfoList.Find(item => item.cropItemId == "crop_04"); //�丶��
        CropItemDataInfo carrot = cropItems.cropItemDataInfoList.Find(item => item.cropItemId == "crop_05"); //���

        // JinnyProcessItem���� ������ ��������
        ProcessItemDataInfo milk = processItems.processItemDataInfoList.Find(item => item.processItemId == "animal_01");          //����
        ProcessItemDataInfo egg = processItems.processItemDataInfoList.Find(item => item.processItemId == "animal_02");           //�ް�
        ProcessItemDataInfo pork = processItems.processItemDataInfoList.Find(item => item.processItemId == "animal_03");          //�������
        ProcessItemDataInfo bread = processItems.processItemDataInfoList.Find(item => item.processItemId == "bread_01");          //�Ļ�
        ProcessItemDataInfo baguette = processItems.processItemDataInfoList.Find(item => item.processItemId == "bread_02");       //�ٰ�Ʈ
        ProcessItemDataInfo croissant = processItems.processItemDataInfoList.Find(item => item.processItemId == "bread_03");      //ũ��ͻ�
        ProcessItemDataInfo flour = processItems.processItemDataInfoList.Find(item => item.processItemId == "windmill_01");       //�а���
        ProcessItemDataInfo chickenfeed = processItems.processItemDataInfoList.Find(item => item.processItemId == "windmill_02"); //�� ���
        ProcessItemDataInfo pigfeed = processItems.processItemDataInfoList.Find(item => item.processItemId == "windmill_03");     //���� ���
        ProcessItemDataInfo cowfeed = processItems.processItemDataInfoList.Find(item => item.processItemId == "windmill_04");     //�� ���
        ProcessItemDataInfo eggflower = processItems.processItemDataInfoList.Find(item => item.processItemId == "grill_01");      //����Ķ���
        ProcessItemDataInfo bacon = processItems.processItemDataInfoList.Find(item => item.processItemId == "grill_02");          //������
        ProcessItemDataInfo tomatojuice = processItems.processItemDataInfoList.Find(item => item.processItemId == "juice_01");    //�丶�� �꽺
        ProcessItemDataInfo carrotjuice = processItems.processItemDataInfoList.Find(item => item.processItemId == "juice_02");    //��� �꽺
        ProcessItemDataInfo butter = processItems.processItemDataInfoList.Find(item => item.processItemId == "dairy_01");         //����
        ProcessItemDataInfo cheese = processItems.processItemDataInfoList.Find(item => item.processItemId == "dairy_02");         //ġ��

        if (cropItems == null || processItems == null)
        {
            Debug.LogError("���۹� , ����ǰ �ֳ�");
            return;
        }

        //����
        List<object> milkIngredients = new List<object>
        {
           new Ingredient<ProcessItemDataInfo>(cowfeed, 1)
        };
        milkRecipe = new Recipe(milkIngredients, milk, 1, 10f);
        milkRecipe.finishedProductId = "animal_01";
        recipesByName["����"] = milkRecipe;
        milkRecipe.recipeName = "����";

        //�ް�
        List<object> eggIngredients = new List<object>
        {
           new Ingredient<ProcessItemDataInfo>(chickenfeed, 1)
        };
        eggRecipe = new Recipe(eggIngredients, egg, 1, 10f);
        eggRecipe.finishedProductId = "animal_02";
        recipesByName["�ް�"] = eggRecipe;
        eggRecipe.recipeName = "�ް�";

        //�������
        List<object> porkIngredients = new List<object>
        {
           new Ingredient<ProcessItemDataInfo>(pigfeed, 1)
        };
        porkRecipe = new Recipe(porkIngredients, pork, 1, 10f);
        porkRecipe.finishedProductId = "animal_03";
        recipesByName["�������"] = porkRecipe;
        porkRecipe.recipeName = "�������";

        //�Ļ�
        List<object> breadIngredients = new List<object>
        {
           new Ingredient<CropItemDataInfo>(wheat, 1)
        };
        breadRecipe = new Recipe(breadIngredients, bread, 1, 10f);
        breadRecipe.finishedProductId = "bread_01";
        recipesByName["�Ļ�"] = breadRecipe;
        breadRecipe.recipeName = "�Ļ�";

        //�ٰ�Ʈ
        List<object> baguetteIngredients = new List<object>
        {
           new Ingredient<CropItemDataInfo>(wheat, 2)
        };
        baguetteRecipe = new Recipe(baguetteIngredients, baguette, 1, 10f);
        baguetteRecipe.finishedProductId = "bread_02";
        recipesByName["�ٰ�Ʈ"] = baguetteRecipe;
        baguetteRecipe.recipeName = "�ٰ�Ʈ";

        //ũ��ͻ� (����)
        List<Ingredient<CropItemDataInfo>> croissantCropIngredients = new List<Ingredient<CropItemDataInfo>>
        {
           new Ingredient<CropItemDataInfo>(wheat, 1)
        };
        List<Ingredient<ProcessItemDataInfo>> croissantProcessIngredients = new List<Ingredient<ProcessItemDataInfo>>
        {
           new Ingredient<ProcessItemDataInfo>(butter, 1)
        };
        List<object> croissantAllIngredients = new List<object>();
        croissantAllIngredients.AddRange(croissantCropIngredients);
        croissantAllIngredients.AddRange(croissantProcessIngredients);
        croissantRecipe = new Recipe(croissantAllIngredients, croissant, 1, 10f);
        croissantRecipe.finishedProductId = "bread_03";
        recipesByName["ũ��ͻ�"] = croissantRecipe;
        croissantRecipe.recipeName = "ũ��ͻ�";

        //�а���
        List<object> flourIngredients = new List<object>
        {
           new Ingredient<CropItemDataInfo>(wheat, 1)
        };
        flourRecipe = new Recipe(flourIngredients, flour, 1, 10f);
        flourRecipe.finishedProductId = "windmill_01";
        recipesByName["�а���"] = flourRecipe;
        flourRecipe.recipeName = "�а���";

        //�� ���
        List<object> chickenfeedIngredients = new List<object>
        {
           new Ingredient<CropItemDataInfo>(corn, 1)
        };
        chickenfeedRecipe = new Recipe(chickenfeedIngredients, chickenfeed, 1, 10f);
        chickenfeedRecipe.finishedProductId = "windmill_02";
        recipesByName["�� ���"] = chickenfeedRecipe;
        chickenfeedRecipe.recipeName = "�� ���";

        //���� ���
        List<object> pigfeedIngredients = new List<object>
        {
           new Ingredient<CropItemDataInfo>(corn, 1)
        };
        pigfeedRecipe = new Recipe(pigfeedIngredients, pigfeed, 1, 10f);
        pigfeedRecipe.finishedProductId = "windmill_03";
        recipesByName["���� ���"] = pigfeedRecipe;
        pigfeedRecipe.recipeName = "���� ���";

        //�� ���
        List<object> cowfeedIngredients = new List<object>
        {
           new Ingredient<CropItemDataInfo>(bean, 1)
        };
        cowfeedRecipe = new Recipe(pigfeedIngredients, cowfeed, 1, 10f);
        cowfeedRecipe.finishedProductId = "windmill_04";
        recipesByName["�� ���"] = cowfeedRecipe;
        cowfeedRecipe.recipeName = "�� ���";

        //����Ķ���
        List<object> eggflowerIngredients = new List<object>
        {
           new Ingredient<ProcessItemDataInfo>(egg, 1)
        };
        eggflowerRecipe = new Recipe(eggflowerIngredients, eggflower, 1, 10f);
        eggflowerRecipe.finishedProductId = "grill_01";
        recipesByName["����Ķ���"] = eggflowerRecipe;
        eggflowerRecipe.recipeName = "��� �Ķ���";

        //������ 
        List<object> baconIngredients = new List<object>
        {
           new Ingredient<ProcessItemDataInfo>(pork, 1)
        };
        baconRecipe = new Recipe(baconIngredients, bacon, 1, 10f);
        baconRecipe.finishedProductId = "grill_02";
        recipesByName["������"] = baconRecipe;
        baconRecipe.recipeName = "������";

        //�丶�� �꽺
        List<object> tomatojuiceIngredients = new List<object>
        {
           new Ingredient<CropItemDataInfo>(tomato, 1)
        };
        tomatojuiceRecipe = new Recipe(tomatojuiceIngredients, tomatojuice, 1, 10f);
        tomatojuiceRecipe.finishedProductId = "juice_01";
        recipesByName["�丶���꽺"] = tomatojuiceRecipe;
        tomatojuiceRecipe.recipeName = "�丶���꽺";

        //��� �꽺
        List<object> carrotjuiceIngredients = new List<object>
        {
           new Ingredient<CropItemDataInfo>(carrot, 1)
        };
        carrotjuiceRecipe = new Recipe(carrotjuiceIngredients, carrotjuice, 1, 10f);
        carrotjuiceRecipe.finishedProductId = "juice_02";
        recipesByName["����꽺"] = carrotjuiceRecipe;
        carrotjuiceRecipe.recipeName = "����꽺";

        //����
        List<object> butterIngredients = new List<object>
        {
           new Ingredient<ProcessItemDataInfo>(milk, 1)
        };
        butterRecipe = new Recipe(butterIngredients, butter, 1, 10f);
        butterRecipe.finishedProductId = "dairy_01";
        recipesByName["����"] = butterRecipe;
        butterRecipe.recipeName = "����";

        //ġ��
        List<object> cheeseIngredients = new List<object>
        {
           new Ingredient<ProcessItemDataInfo>(milk, 1)
        };
        cheeseRecipe = new Recipe(cheeseIngredients, cheese, 1, 10f);
        cheeseRecipe.finishedProductId = "dairy_02";
        recipesByName["ġ��"] = cheeseRecipe;
        cheeseRecipe.recipeName = "ġ��";


        //�ǹ� Ÿ�Ժ� ������ ����
        animalRecipes[AnimalType.CageChichen] = new List<Recipe> { eggRecipe };
        animalRecipes[AnimalType.CageCow] = new List<Recipe> { milkRecipe };
        animalRecipes[AnimalType.CagePig] = new List<Recipe> { porkRecipe }; 
        buildingRecipes[BuildingType.Bakery] = new List<Recipe> { breadRecipe, baguetteRecipe, croissantRecipe };                   //����
        buildingRecipes[BuildingType.Windmill] = new List<Recipe> { flourRecipe, chickenfeedRecipe, pigfeedRecipe, cowfeedRecipe }; //���̼�
        buildingRecipes[BuildingType.GrillShop] = new List<Recipe> { eggflowerRecipe, baconRecipe };                                //ö�ǰ���
        buildingRecipes[BuildingType.JuiceShop] = new List<Recipe> { tomatojuiceRecipe, carrotjuiceRecipe };                        //�꽺����
        buildingRecipes[BuildingType.Dairy] = new List<Recipe> { butterRecipe, cheeseRecipe };                                      //����ǰ ������

    }
}