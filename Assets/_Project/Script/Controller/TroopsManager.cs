using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class TroopsManager : MonoBehaviour
{
    public GameObject[] playerTroops;
    public GameObject[] enemyTroops;
    public Transform playerHouse;
    public Transform enemyHouse;
    public GameObject troopDeadEffect;
    public bool IsTroopUnlocked(int troopNumber)
    {
        if(!PlayerPrefs.HasKey("IsTroop"+troopNumber.ToString()+"Unlocked"))
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public void UnlockTroop(int troopNumber)
    {
        PlayerPrefs.SetString("IsTroop" + troopNumber.ToString() + "Unlocked", "true");
        Controller.self.uiController.ingamePannel.RefreshTroopButtons();
    }
    
    public void SpawnTroops(int index)
    {
        GameObject troop = Instantiate(playerTroops[index]);
        playerHouse.GetComponent<Animation>().Play();
    }
}
