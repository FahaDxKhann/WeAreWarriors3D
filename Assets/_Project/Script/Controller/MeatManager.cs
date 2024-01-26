using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatManager : MonoBehaviour
{
    public float perMeatGenarationTime = 5.5f;
    public float maxMeatGenarationTime = 0.8f;


    public int GetCurrentlyStoredMeat()
    {
        return Controller.self.uiController.ingamePannel.GetCurrentlyStoredMeat();
    }

    public void CutMeat(int amount)
    {
        Controller.self.uiController.ingamePannel.CutMeat(amount);
    }

    public float GetCurrentMeatGenarationTime()
    {
        if(PlayerPrefs.HasKey("PerMeatGenarationTime"))
        {
            return PlayerPrefs.GetFloat("PerMeatGenarationTime");
        }
        else
        {
            return perMeatGenarationTime;
        }
    }

    public void DecreaseMeatGenarationTime()
    {
        float defaultDecreaseUnit = 0.18f;
        float time = GetCurrentMeatGenarationTime();

        if(time <= maxMeatGenarationTime) return;
        if(GetCurrentMeatGenerationLevel() == 0)
        {
            time = time - defaultDecreaseUnit;
            PlayerPrefs.SetFloat("PerMeatGenarationTime", time);
            PlayerPrefs.SetFloat("CurrentDecreaseTime", defaultDecreaseUnit);
            IncreaseMeatGenerationLevel();
            Controller.self.uiController.ingamePannel.meatSlider.ChangeDuration(time);
        }
        else
        {
            float decreseAmount = GetCurrentDecreaseAmount() + 0.02f;
            PlayerPrefs.SetFloat("CurrentDecreaseTime", decreseAmount);
            time = time - decreseAmount;
            PlayerPrefs.SetFloat("PerMeatGenarationTime", time);
            IncreaseMeatGenerationLevel();
            Controller.self.uiController.ingamePannel.meatSlider.ChangeDuration(time);
        }
    }

    public float GetCurrentDecreaseAmount()
    {
        if(!PlayerPrefs.HasKey("CurrentDecreaseTime"))
        {
            return 0.18f;
        }
        else
        {
            return PlayerPrefs.GetFloat("CurrentDecreaseTime");
        }
    }

    public int GetCurrentMeatGenerationLevel()
    {
        return PlayerPrefs.GetInt("GenerationLevel");
    }

    public void IncreaseMeatGenerationLevel()
    {
        int level = GetCurrentMeatGenerationLevel();
        level = level + 1;
        PlayerPrefs.SetInt("GenerationLevel", level);
    }
}
