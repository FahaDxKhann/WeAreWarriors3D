using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class StartPannelView : MonoBehaviour
{
    public GameObject homeButton;
    private Vector3 homeBOrgRect;
    public GameObject upgradeShopButton;
    private Vector3 upgradeShopBOrgRect;
    public GameObject battleButton;


    private void Start() 
    {
        homeBOrgRect = homeButton.GetComponent<RectTransform>().position;
        upgradeShopBOrgRect = upgradeShopButton.GetComponent<RectTransform>().position;

        homeButton.GetComponent<RectTransform>().position = new Vector3(homeBOrgRect.x,homeBOrgRect.y + 40f,homeBOrgRect.z);
    }


    public void HomeButton()
    {
        homeButton.GetComponent<RectTransform>().DOMove(new Vector3(homeBOrgRect.x,homeBOrgRect.y + 40f,homeBOrgRect.z),0.2f).SetEase(Ease.OutBack);
        upgradeShopButton.GetComponent<RectTransform>().DOMove(upgradeShopBOrgRect, 0.1f);
        battleButton.SetActive(true);
        Controller.self.uiController.upgradeShopPannelView.gameObject.SetActive(false);
    }
    public void UpgradeShopButton()
    {
        upgradeShopButton.GetComponent<RectTransform>().DOMove(new Vector3(upgradeShopBOrgRect.x,upgradeShopBOrgRect.y + 40f,upgradeShopBOrgRect.z),0.2f).SetEase(Ease.OutBack);
        homeButton.GetComponent<RectTransform>().DOMove(homeBOrgRect, 0.1f);
        battleButton.SetActive(false);
        Controller.self.uiController.upgradeShopPannelView.gameObject.SetActive(true);
        Controller.self.uiController.upgradeShopPannelView.UpdateTexts();
    }

    public void BattleButton()
    {
        Controller.self.gameController.StartGame();
    }
}
