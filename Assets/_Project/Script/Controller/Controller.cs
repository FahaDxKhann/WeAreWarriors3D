using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public static Controller self;
    public GameController gameController;
    public LevelController levelController;
    public UiController uiController;

    private void Awake() 
    {
        self = this;
    }
}
