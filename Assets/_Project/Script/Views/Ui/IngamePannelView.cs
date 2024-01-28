using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IngamePannelView : MonoBehaviour
{
    public Button[] troopButtons;
    public MeatSliderView meatSlider;



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
        Controller.self.troopsManager.SpawnTroops(index);
    }
}
