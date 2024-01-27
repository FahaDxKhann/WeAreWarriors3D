using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MeatSliderView : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI meatStoredText;
    [HideInInspector]
    public int currentlyMeatStored;
    public float duration; // Initial duration

    private float timer;
    private bool isSliderFull = false;

    void Start()
    {
        timer = 0f;
        slider.value = 0;
        duration = Controller.self.meatManager.GetCurrentMeatGenarationTime();
        meatStoredText.text = currentlyMeatStored.ToString();
    }

    void Update()
    {
        if(!GlobalData.instance.isGameStarted) return;

        if (!isSliderFull)
        {
            timer += Time.deltaTime;

            // Update the slider value smoothly based on the duration
            float progress = Mathf.Clamp01(timer / duration);
            slider.value = progress;

            // Check if the slider is full
            if (progress >= 1f)
            {
                SliderFull();
            }
        }
    }

    void SliderFull()
    {
        // Perform actions when the slider is full
        Debug.Log("Slider is full! Performing actions.");

        // Reset the timer and slider value
        timer = 0f;
        slider.value = 0f;

        // Set the flag to false to resume slider progression
        isSliderFull = false;

        IncreaseMeat();
    }

    // Function to change the duration at runtime
    public void ChangeDuration(float newDuration)
    {
        duration = newDuration;
        Debug.Log("Duration changed to: " + newDuration);
    }

    public void IncreaseMeat()
    {
        currentlyMeatStored = currentlyMeatStored + 1;
        meatStoredText.text = currentlyMeatStored.ToString();

        Controller.self.uiController.ingamePannel.RefreshTroopButtons();
    }
}
