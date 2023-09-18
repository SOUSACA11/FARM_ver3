using UnityEngine;
using UnityEngine.UI;
using TMPro;

//by.J:230811 ��ȭ �ý��� UI / �̺�Ʈ ������ �߰�
//by.J:230918 �ʱ���ȭ ���� �ѹ���
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
        MoneySystem.Instance.OnMoneychange += UpdateMoneyUI; //�̺�Ʈ ������
    }

    private void OnDestroy()
    {
        MoneySystem.Instance.OnMoneychange -= UpdateMoneyUI; //�̺�Ʈ ������
    } 

    private void UpdateMoneyUI()
    {
        moneyText.text = MoneySystem.Instance.Gold.ToString();
    }
}
