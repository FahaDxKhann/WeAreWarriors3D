using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TroopsManager : MonoBehaviour
{
    public GameObject[] troops;
    public Transform playerHouse;
    public Transform enemyHouse;
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
        GameObject troop = Instantiate(troops[index]);
        troop.SetActive(true);
    }
}
