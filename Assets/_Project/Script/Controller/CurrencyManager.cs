using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyManager : MonoBehaviour
{
    public TextMeshProUGUI CoinText;


    private void Start() 
    {
        CoinText.text = GetCurrentCoin().ToString();
    }

    private void Update() 
    {
        #if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.X))
        {
            AddCoin(500);
        }
        #endif
    }

    public int GetCurrentCoin()
    {
        return PlayerPrefs.GetInt("VirtualCoin");
    }

    public void AddCoin(int amount)
    {
        int coin = GetCurrentCoin();
        coin = coin + amount;
        PlayerPrefs.SetInt("VirtualCoin", coin);
        CoinText.text = GetCurrentCoin().ToString();
    }
    public void CutCoin(int amount)
    {
        int coin = GetCurrentCoin();
        coin = coin - amount;
        PlayerPrefs.SetInt("VirtualCoin", coin);
        CoinText.text = GetCurrentCoin().ToString();
    }
}
