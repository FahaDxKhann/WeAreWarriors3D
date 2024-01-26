using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalData : MonoBehaviour
{
    public static GlobalData instance;
    public bool isGameStarted;



    private void Awake() 
    {
        instance = this;
    }
}
