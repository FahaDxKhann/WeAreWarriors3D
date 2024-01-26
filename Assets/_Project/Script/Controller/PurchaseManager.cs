using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PurchaseManager : MonoBehaviour
{
    [SerializeField]
    private int defaultMGUpgradePrice;
    public int[] increamentValue;

    private void Update() {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            UpgradeMeatGeneration();
        }
    }
    public void UpgradeMeatGeneration()
    {
        if(Controller.self.meatManager.GetCurrentMeatGenarationTime() <= Controller.self.meatManager.maxMeatGenarationTime) return;
        if(Controller.self.currencyManager.GetCurrentCoin() >= GetCurrentMeatUpgradePrice())
        {
            Controller.self.meatManager.DecreaseMeatGenarationTime();
            Controller.self.currencyManager.CutCoin(GetCurrentMeatUpgradePrice());
            CalculateGenerationUpgradePrice();
        }
    }

    public void CalculateGenerationUpgradePrice()
    {
        int price = GetCurrentMeatUpgradePrice();
        price = defaultMGUpgradePrice + increamentValue[Controller.self.meatManager.GetCurrentMeatGenerationLevel()];
        PlayerPrefs.SetInt("CurrentMeatUpgradePrice", price);
    }
    public int GetCurrentMeatUpgradePrice()
    {
        if(!PlayerPrefs.HasKey("CurrentMeatUpgradePrice"))
        {
            return defaultMGUpgradePrice;
        }
        else
        {
            return PlayerPrefs.GetInt("CurrentMeatUpgradePrice");
        }
    }
}
