using UnityEngine;
using UnityEngine.UI;
using TMPro;

//by.J:230811 ��ȭ �ý��� UI / �̺�Ʈ ������ �߰�
public class MoneyManagerUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;

    private void Start()
    {
        //�ʱ� ��ȭ ����
        MoneySystem.Instance.AddGold(30);
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
