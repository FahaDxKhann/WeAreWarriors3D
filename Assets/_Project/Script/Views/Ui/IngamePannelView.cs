using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngamePannelView : MonoBehaviour
{
    public MeatSliderView meatSlider;


    public int GetCurrentlyStoredMeat()
    {
        return meatSlider.currentlyMeatStored;
    }

    public void CutMeat(int amount)
    {
        if (amount >= meatSlider.currentlyMeatStored)
        {
            meatSlider.currentlyMeatStored = meatSlider.currentlyMeatStored - amount;
            meatSlider.meatStoredText.text = meatSlider.currentlyMeatStored.ToString();
        }
    }
}
