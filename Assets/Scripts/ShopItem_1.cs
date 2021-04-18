﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopItem_1 : MonoBehaviour
{
    public GameObject buyMessage;
    private bool isInZone = false;
    private int price = 10;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (isInZone)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (CoinController.Instance.CoinNumber >= price)
                {
                    CoinController.Instance.loseCoin(price);
                    HealthController.Instance.add2MaxHealth();
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isInZone = true;
            buyMessage.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            isInZone = false;
            buyMessage.SetActive(false);
        }
    }
}