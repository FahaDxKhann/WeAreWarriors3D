using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public void StartGame()
    {
        GlobalData.instance.isGameStarted = this;
        Controller.self.uiController.ingamePannel.gameObject.SetActive(true);
        Controller.self.uiController.startPannelView.gameObject.SetActive(false);
        Controller.self.enemyWaveManager.StartEnemyWave();
    }

    public void OnGameOver()
    {
        Controller.self.uiController.OnGameOver();
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
        }
    }
}
