using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

public class HouseView : MonoBehaviour
{
    public int Health;

    public Slider slider;
    public TextMeshProUGUI text;



    private void Start() 
    {
        slider.maxValue = Health;
        UpdateSlider();
    }

    public void UpdateSlider()
    {
        text.text = Health.ToString();
        slider.value = Health;
    }

    public void DecreaseHealth()
    {
        if(Health > 0)
        {
            Health = Health - 2;
        }
        UpdateSlider();
        OnFinishHealth();
    }

    public void OnFinishHealth()
    {
        if(GlobalData.instance.isGameOver) return;
        if(Health <= 0)
        {
            Controller.self.gameController.OnGameOver();
        }
    }

}
