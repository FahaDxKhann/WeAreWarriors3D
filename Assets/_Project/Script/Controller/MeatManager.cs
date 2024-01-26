using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatManager : MonoBehaviour
{
    public float perMeatGenarationTime = 5.5f;
    public float maxMeatGenarationTime = 0.8f;


    private void Update() 
    {
        #if UNITY_EDITOR
        if(Input.GetKeyDown(KeyCode.D))
        {
            DecreaseMeatGenarationTime();
        }
        #endif
    }
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
            IncreaseMeatGenerationLevel();
            Controller.self.uiController.ingamePannel.meatSlider.ChangeDuration(time);
        }
        else
        {
            float decreseAmount = defaultDecreaseUnit + 0.02f;
            time = time - decreseAmount;
            PlayerPrefs.SetFloat("PerMeatGenarationTime", time);
            IncreaseMeatGenerationLevel();
            Controller.self.uiController.ingamePannel.meatSlider.ChangeDuration(time);
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
