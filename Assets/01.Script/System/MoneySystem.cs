using UnityEngine;
using UnityEngine.SceneManagement;

//by.J:230719 재화시스템 싱글톤
//by.J:230811 재화 변경시에만 UI 업데이트
//by.J:230823 엔딩씬 전환
public class MoneySystem : MonoBehaviour
{
    private static MoneySystem instance;

    public int Gold { get; private set; } = 0;

    public bool IsInitialized { get; private set; } = false;

    public delegate void MoneyChange(); //delegate -> 함수를 변수처럼 취급하여 사용 / 메서드 참조 / 메개변수 같아야 가능
    public event MoneyChange OnMoneychange;

    //싱글톤 인스턴스 접근자
    public static MoneySystem Instance
    {
        get
        {
            //인스턴스가 없을 경우 생성
            if (instance == null)
            {
                //씬에서 재화시스템 오브젝트 찾기
                instance = FindAnyObjectByType<MoneySystem>();

                //만약 씬에 없을 경우 새로 생성
                if (instance == null)
                {
                    GameObject singletonobj = new GameObject("MoneySystem");
                    instance = singletonobj.AddComponent<MoneySystem>();
                }
            }

            //함수 탈출
            return instance;
        }
    }

    //초기화
    private void Awake()
    {
        //인스턴스가 없을 경우
        if (instance == null)
        {
            //씬 변경 시 삭제 방지
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }

        //이미 인스턴스 존재 시 중복 인스턴스 삭제
        else
        {
            Destroy(gameObject);
        }

    }


    //재화 증가 기능
    public void AddGold(int amount)
    {
        if (!IsInitialized)
        {
            Gold = 30; //초기재화
            IsInitialized = true;
        }

        Gold += amount;
        Debug.Log("현재 재화 플 증가됨?" + Gold);
        OnMoneychange?.Invoke(); //? -> null 아니라면(이벤트 핸들러 1개 이상 연결된 경우) Invoke 호출

        CheckForEnding();
    }

    //재화 감소 기능(마이너스 방지)
    public bool DeductGold(int amount)
    {
       
        if (Gold < amount)
        {
            Debug.LogWarning("금액이 부족합니다.");
            return false;
        }

        Gold -= amount;
        OnMoneychange?.Invoke(); //? -> null 아니라면 (이벤트 핸들러 1개 이상 연결된 경우) Invoke 호출
        return true; //금액 차감에 성공하면 true 반환
    
}
    
    //엔딩 조건
    private void CheckForEnding()
    {
        if (Gold >= 100) //금화100
        {
            SceneManager.LoadScene("Epilogue");
        }
    }
}
