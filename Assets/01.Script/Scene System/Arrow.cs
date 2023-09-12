using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    public GameObject storeArrowObject;  // 활성화/비활성화할 GameObject를 참조합니다.

    // 이 함수는 애니메이션 이벤트에서 호출됩니다.
    public void OpenStoreArrow()
    {
        Debug.Log("OpenStoreArrow called!");
        storeArrowObject.SetActive(true);
    }

    public void EndStoreArrow()
    {
        storeArrowObject.SetActive(false);
    }
}
