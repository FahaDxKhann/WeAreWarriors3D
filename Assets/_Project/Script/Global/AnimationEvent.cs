using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AnimationEvent : MonoBehaviour
{
    public void GiveDamage()
    {
        this.transform.parent.gameObject.transform.parent.GetComponent<TroopsView>().StartDamaging();
    }

    public void ReloadScene()
    {
        SceneManager.LoadScene(0);
    }
}
