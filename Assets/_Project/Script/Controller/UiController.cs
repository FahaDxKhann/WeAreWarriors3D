using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiController : MonoBehaviour
{
    public IngamePannelView ingamePannel;
    public StartPannelView startPannelView;
    public UpgradeShopPannelView upgradeShopPannelView;


    private void Start() 
    {
        ingamePannel.gameObject.SetActive(false);
        startPannelView.gameObject.SetActive(true);
        upgradeShopPannelView.gameObject.SetActive(false);
    }
}
