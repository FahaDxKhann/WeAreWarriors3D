using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class IngamePannelView : MonoBehaviour
{
    public Button[] troopButtons;
    public MeatSliderView meatSlider;
    public GameObject popUp;
    public TextMeshProUGUI popUpText;
    public GameObject bg;



    public int GetCurrentlyStoredMeat()
    {
        return meatSlider.currentlyMeatStored;
    }

    public void CutMeat(int amount)
    {
        if (meatSlider.currentlyMeatStored >= amount)
        {
            meatSlider.currentlyMeatStored = meatSlider.currentlyMeatStored - amount;
            meatSlider.meatStoredText.text = meatSlider.currentlyMeatStored.ToString();
            RefreshTroopButtons();
        }
    }

    public void RefreshTroopButtons()
    {
        int currentMeat = Controller.self.meatManager.GetCurrentlyStoredMeat();

        for (int i = 0; i < troopButtons.Length; i++)
        {
            if(currentMeat >= troopButtons[i].GetComponent<TroopButton>().meatConsume)
            {
                troopButtons[i].interactable = true;
            }
            else
            {
                troopButtons[i].interactable = false;
            }
        }

        for (int i = 0; i < troopButtons.Length; i++)
        {
            if(!Controller.self.troopsManager.IsTroopUnlocked(troopButtons[i].GetComponent<TroopButton>().troopNmbr))
            {
                troopButtons[i].gameObject.SetActive(false);
            }
        }
    }

    public void OnTroopButtonClick(int index)
    {
        troopButtons[index].GetComponent<Animation>().Play();
        Controller.self.troopsManager.SpawnTroops(index);
        SoundManager.Instance.PlaySound(SoundManager.Instance.button);
    }

    public void ShowGameOverPopUp()
    {
        popUp.SetActive(true);
        meatSlider.gameObject.SetActive(false);
        troopButtons[0].gameObject.transform.parent.gameObject.SetActive(false);
        bg.SetActive(false);
        popUpText.text = Controller.self.currencyManager.collectedCoin.ToString();
    }

    public void CollectButton()
    {
        SoundManager.Instance.PlaySound(SoundManager.Instance.coinCollected);
        Controller.self.currencyManager.AddCoin(Controller.self.currencyManager.collectedCoin);
        SceneManager.LoadScene(0);
    }
}
