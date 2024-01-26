using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public void StartGame()
    {
        GlobalData.instance.isGameStarted = this;
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }
}
