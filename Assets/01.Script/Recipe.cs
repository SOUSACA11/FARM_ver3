using UnityEngine;
using System.Collections.Generic;
using JinnyProcessItem;
using JinnyCropItem;
using System;


//by.J:230816 아이템, 원재료, 레시피 설정
//by.J;230829 생산시간 추가

//원재료 설정 리스트 정의
[System.Serializable]
public class Ingredient<T> //제네릭 -> 데이터 타입 유연화 / 타입 미리 지정하지 않고 실행시 지정
{
    public T item;       //아이템
    public int quantity; //수량

    public Ingredient(T item, int quantity)
    {
        this.item = item;
        this.quantity = quantity;
    }
}

//레시피 설정
[System.Serializable]
public class Recipe
{

    public bool IsInitialized;

    public List<object> ingredients;
    public ProcessItemDataInfo originalFinishedProduct;  //finishedProduct;
    public int finishedProductCount;
    public float productionTime;
    public string finishedProductId;
    public string recipeName;

    private ProcessItemDataInfo? _finishedProduct;

    public ProcessItemDataInfo finishedProduct
    {
        get
        {
            if (!_finishedProduct.HasValue || !_finishedProduct.Value.IsInitialized)
            {
                return this.originalFinishedProduct;  // Lazy Initialization을 통해 값을 반환합니다.
            }
            return _finishedProduct.Value;
        }
        set
        {
            _finishedProduct = value;
        }
    }

    public Recipe()
    {
        this.IsInitialized = true;
    }


    //완성품 ID 정보에서 이미지 추가
    public Sprite FinishedProductImage
    {
        get
        {
            Debug.Log("프로퍼티 시작쓰");

            Sprite resultSprite = null;

            Debug.Log("Instance: " + CropItem.Instance);
            Debug.Log("List: " + CropItem.Instance.cropItemDataInfoList);
            Debug.Log("Finished Product ID: " + finishedProductId);

            if (finishedProductId.StartsWith("crop_"))
            {
                Debug.Log("크롭쓰");
                var foundItem = CropItem.Instance.cropItemDataInfoList
                    .Find(item => item.cropItemId == finishedProductId);

                if (!foundItem.Equals(default(CropItemDataInfo)))
                {
                    resultSprite = foundItem.cropItemImage;
                }
            }

            else if (finishedProductId.StartsWith("animal_") ||
                     finishedProductId.StartsWith("bread_") ||
                     finishedProductId.StartsWith("windmill_") ||
                     finishedProductId.StartsWith("grill_") ||
                     finishedProductId.StartsWith("juice_") ||
                     finishedProductId.StartsWith("dairy_"))
            {
                Debug.Log("찾는다");
                var foundItem = ProcessItem.Instance.processItemDataInfoList
                    .Find(item => item.processItemId == finishedProductId);
                
                if (!foundItem.Equals(default(ProcessItemDataInfo)))
                {
                    resultSprite = foundItem.processItemImage;
                }
            }

            Debug.Log("Finished Product ID: " + finishedProductId);
            Debug.Log("완성품 이미지 : " + (resultSprite ? resultSprite.name : "None"));
            return resultSprite;
        }
    }

    public Recipe(List<object> ingredients, ProcessItemDataInfo finishedProduct, int finishedProductCount, float productionTime)
    : this()
    {
        //this.ingredients = ingredients;
        //this.originalFinishedProduct = finishedProduct;
        //this.finishedProductCount = finishedProductCount;
        //this.productionTime = productionTime;

        //this.originalFinishedProduct = finishedProduct;
        //this.finishedProductId = finishedProduct.processItemId;
        //this.IsInitialized = true;

        if (!finishedProduct.IsInitialized)
        {
            throw new ArgumentException("FinishedProduct is not initialized!");
        }

        this.ingredients = ingredients;
        this.originalFinishedProduct = finishedProduct;
        this.finishedProductCount = finishedProductCount;
        this.productionTime = productionTime;
        this.finishedProduct = this.originalFinishedProduct;  // 이 부분에서 초기화를 해줍니다.
        this.IsInitialized = true;
    }



}
   

