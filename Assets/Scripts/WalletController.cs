using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WalletController : MonoBehaviour
{
    public int CurrentGold = 50;
    private int _goldShown;
    private Text _goldValue;

    void Start()
    {
        _goldShown = CurrentGold ;
        _goldValue = GetComponentInChildren<Text>();
    }

    void Update()
    {
        _goldShown = (int) Mathf.MoveTowards(_goldShown, CurrentGold, 250 * Time.deltaTime);
        _goldValue.text = _goldShown.ToString();
    }

    public bool UpdateWallet(int price)
    {
        if (CurrentGold + price < 0)
        {
            return false;
        }
        CurrentGold += price;
        return true;
    }
}
