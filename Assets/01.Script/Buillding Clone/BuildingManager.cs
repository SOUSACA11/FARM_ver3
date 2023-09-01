using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public float productionTime = 5.0f; // 얼마나 자주 생산할 것인가? 예를 들어 5초마다.
    private float elapsedTime = 0.0f; // 마지막 생산 이후 경과한 시간.
    public bool isProducing = false;

}
