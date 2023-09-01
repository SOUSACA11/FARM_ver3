using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrdersTable : MonoBehaviour
{
    public static OrdersTable instance;

    public List<Order> orders;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        //Debug.Log("orders Count : " + orders.Count);
    }
}
