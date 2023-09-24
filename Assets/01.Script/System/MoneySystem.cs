using UnityEngine;
using UnityEngine.SceneManagement;

//by.J:230719 ��ȭ�ý��� �̱���
//by.J:230811 ��ȭ ����ÿ��� UI ������Ʈ
//by.J:230823 ������ ��ȯ
public class MoneySystem : MonoBehaviour
{
    private static MoneySystem instance;

    public int Gold { get; private set; } = 0;

    public bool IsInitialized { get; private set; } = false;

    public delegate void MoneyChange(); //delegate -> �Լ��� ����ó�� ����Ͽ� ��� / �޼��� ���� / �ް����� ���ƾ� ����
    public event MoneyChange OnMoneychange;

    //�̱��� �ν��Ͻ� ������
    public static MoneySystem Instance
    {
        get
        {
            //�ν��Ͻ��� ���� ��� ����
            if (instance == null)
            {
                //������ ��ȭ�ý��� ������Ʈ ã��
                instance = FindAnyObjectByType<MoneySystem>();

                //���� ���� ���� ��� ���� ����
                if (instance == null)
                {
                    GameObject singletonobj = new GameObject("MoneySystem");
                    instance = singletonobj.AddComponent<MoneySystem>();
                }
            }

            //�Լ� Ż��
            return instance;
        }
    }

    //�ʱ�ȭ
    private void Awake()
    {
        //�ν��Ͻ��� ���� ���
        if (instance == null)
        {
            //�� ���� �� ���� ����
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        //�̹� �ν��Ͻ� ���� �� �ߺ� �ν��Ͻ� ����
        else
        {
            Destroy(gameObject);
        }

    }


    //��ȭ ���� ���
    public void AddGold(int amount)
    {
        if (!IsInitialized)
        {
            Gold = 30; //�ʱ���ȭ
            IsInitialized = true;
        }

        Gold += amount;
        Debug.Log("���� ��ȭ �� ������?" + Gold);
        OnMoneychange?.Invoke(); //? -> null �ƴ϶��(�̺�Ʈ �ڵ鷯 1�� �̻� ����� ���) Invoke ȣ��

        CheckForEnding();
    }

    //��ȭ ���� ���(���̳ʽ� ����)
    public bool DeductGold(int amount)
    {
       
        if (Gold < amount)
        {
            Debug.LogWarning("�ݾ��� �����մϴ�.");
            return false;
        }

        Gold -= amount;
        OnMoneychange?.Invoke(); //? -> null �ƴ϶�� (�̺�Ʈ �ڵ鷯 1�� �̻� ����� ���) Invoke ȣ��
        return true; //�ݾ� ������ �����ϸ� true ��ȯ
    
}
    
    //���� ����
    private void CheckForEnding()
    {
        if (Gold >= 100) //��ȭ100
        {
            SceneManager.LoadScene("Epilogue");
        }
    }
}
