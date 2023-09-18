using UnityEngine;
using UnityEngine.UI;
using TMPro;

//by.J:230811 재화 시스템 UI / 이벤트 리스너 추가
//by.J:230918 초기재화 설정 한번만
public class MoneyManagerUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;


    private void Start()
    {
        if (!MoneySystem.Instance.IsInitialized)
        {
            MoneySystem.Instance.AddGold(0);
        }

        UpdateMoneyUI();
        MoneySystem.Instance.OnMoneychange += UpdateMoneyUI; //이벤트 리스너
    }

    private void OnDestroy()
    {
        MoneySystem.Instance.OnMoneychange -= UpdateMoneyUI; //이벤트 리스너
    } 

    private void UpdateMoneyUI()
    {
        moneyText.text = MoneySystem.Instance.Gold.ToString();
    }
}
