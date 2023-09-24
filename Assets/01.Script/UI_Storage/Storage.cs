using System.Collections.Generic;
using UnityEngine;
using System.Linq;

//by.J:230808 창고 시스템
//by.J:230829 싱글톤
//by.J:230901 모든 아이템 래퍼클래스 타입
public class Storage : MonoBehaviour
{
    //Storage = Processitem, CropItem 을 IItem 타입으로 취급
    private Dictionary<IItem, int> items; //보유 아이템 사전
    private int capa;                     //최대 용량

    public static Storage Instance { get; private set; } //싱글톤

    //이벤트
    public delegate void StorageChanged();
    public event StorageChanged OnStorageChanged;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Storages(100); //초기 용량
        }
        else
        {
            Destroy(gameObject);
        }

    }

    public void Storages(int capa)
    {
        this.items = new Dictionary<IItem, int>();
        this.capa = capa;
    }

    public Dictionary<IItem, int> Items { get { return items; } } 

    //아이템 추가
    public bool AddItem(IItem item, int count)
    {
        Debug.Log("창고 아이템 추가");

        Debug.Log($"[Storage] Adding Item Type: {item.GetType().Name}");

        if (item == null)
        {
            Debug.LogError("Attempting to add a null item to storage.");
            return false;
        }


        if (GetTotalItemCount() + count > capa)
        {
            //창고의 최대 용량을 초과하면 아이템을 추가하지 않음
            return false;
        }

        if (items.ContainsKey(item))
        {
            items[item] += count;  //이미 보유중인 아이템인 경우 수량을 늘림
        }
        else
        {
            items[item] = count;  //새로운 아이템인 경우 아이템을 추가
        }

        OnStorageChanged?.Invoke(); //아이템이 추가되면 이벤트 호출

        Debug.Log($"[Storage] After adding item. Current item list:");
        foreach (var pair in items)
        {
            Debug.Log($"- Item: {pair.Key.ItemName} | Count: {pair.Value}");
        }

        Debug.Log($"[Storage] Added Item: {item.ItemName}");

        Debug.Log("창고 추가 아템" + item);

        return true;
    }

    //아이템 제거
    public bool RemoveItem(IItem item, int count)
    {
        Debug.Log("창고 아이템 제거");

        if (!items.ContainsKey(item) || items[item] < count)
        {
            //보유중인 아이템이 아니거나, 수량이 충분하지 않으면 아이템을 제거하지 않음
            return false;
        }

        items[item] -= count;  //아이템 수량 줄임
        if (items[item] == 0)
        {
            items.Remove(item);  //아이템 수량이 0이 되면 아이템을 제거
        }

        OnStorageChanged?.Invoke(); //아이템이 제거되면 이벤트 호출
        return true;
    }

    //보유 중 아이템 총 개수 반환
    private int GetTotalItemCount()
    {
        if (items == null)
        {
            Debug.LogError("Items dictionary is not initialized!");
            return 0;
        }

        int totalCount = 0;
        foreach (int count in items.Values)
        {
            totalCount += count;
        }
        return totalCount;
    }
    
    //창고 내 특정 아이템 수량 확인 기능
    public int GetItemAmount(IItem item)
    {
        //아이템 리스트에 있는 해당 아이템의 개수 반환
        if (items.TryGetValue(item, out int count))
        {
            Debug.Log($"[Storage] Current item list in storage:");
            Debug.Log($"[Storage] Item: {item.ItemName} | Count: {count}");
            foreach (var pair in items)
            {
                Debug.Log($"- Item: {pair.Key.ItemName} | Count: {pair.Value}");
            }
            return count;
        }
        else
        {
            return 0; //아이템이 창고에 없으면 0 반환
        }
    }

    //작물 아이템만 가져오기
    public Dictionary<IItem, int> GetCropItems()
    {
        Debug.Log("작물");
        return items.Where(pair => pair.Key is CropItemIItem).ToDictionary(pair => pair.Key, pair => pair.Value);
    }

    //생산품 아이템만 가져오기
    public Dictionary<IItem, int> GetProcessItems()
    {
        Debug.Log("생산품");
        return items.Where(pair => pair.Key is ProcessItemIItem).ToDictionary(pair => pair.Key, pair => pair.Value);
    }
}
