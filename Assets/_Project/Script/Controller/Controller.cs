using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller self;
    public GameController gameController;
    public LevelController levelController;
    public UiController uiController;
    public MeatManager meatManager;
    public CurrencyManager currencyManager;
    public PurchaseManager purchaseManager;

    private void Awake() 
    {
        self = this;
    }
}
