using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public static GlobalData instance;
    public bool isGameStarted;
    public bool isGameOver;



    private void Awake() 
    {
        instance = this;
    }
}
