using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    public void GiveDamage()
    {
        this.transform.parent.gameObject.transform.parent.GetComponent<TroopsView>().StartDamaging();
    }
}
