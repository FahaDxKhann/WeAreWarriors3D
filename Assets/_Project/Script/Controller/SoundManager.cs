using UnityEngine;
using System.Collections;
using System;
using DG.Tweening;
//using MoreMountains.NiceVibrations;

public class SoundManager : MonoBehaviour {

	public static SoundManager Instance;

	public AudioSource button;
	
    public AudioSource shot;
    public AudioSource serve;
    public AudioSource hitOnTable;
    public AudioSource normalPoint;
    public AudioSource speedPoint;
    public AudioSource timeUp;
    public AudioSource win;
    public AudioSource lost;
    public AudioSource lostPoint;
    public AudioSource celebrate;
    public AudioSource point;
    public AudioSource coinCollected;
    public AudioSource popUp;
    public AudioSource popIn;
    public AudioSource ropSuccess;
    public AudioClip untangled;
    public AudioSource gotHit;
    public AudioSource trapped;

    public AudioSource upgrade;
    public AudioSource bg;

    [Space]
    [Space]
    public AudioClip hitClip;
    public AudioClip deathClip;
    public AudioClip arrowClip;
    public AudioClip glassBreakClip;

    public AudioSource[] perfects;

    public static bool isSound;

	private const string IS_SOUND = "isSound";

    void Awake()
    {
        DontDestroyOnLoad(this);
        if (Instance == null)
        {
            Instance = this;
        }
        // CheckSound();
    }

	// public void CheckSound(){		
	// 	isSound = (PlayerPrefs.GetInt(IS_SOUND) == 0);
	// 	UISound.UpdateSoundIcon (isSound);

	// 	//PlaySound (bgMenu);

	// }

	public int index;

    public void PlayPerfectSound() {
        PlaySound(perfects[index]);
        index++;
        if(index >= perfects.Length) {
            index = perfects.Length - 1;
        }
    }

    public void ResetSequence() {
        index = 0;
    }

	public void ToggleSound(Action<bool> isSoundOn) {
		if (isSound) {
			isSound = false;           
			PlayerPrefs.SetInt(IS_SOUND, 1);
			isSoundOn(false);
            
		}
		else {					
			isSound = true;
			PlayerPrefs.SetInt(IS_SOUND, 0);
			isSoundOn(true);
            
        }

		//PlaySound (menuBG);

		// UISound.UpdateSoundIcon (isSound);
	}

	public void PlaySound(AudioSource audio){
		if (audio != null) {
            audio.pitch = 1;
			audio.Play ();
		}
	}

    public void PlaySound(AudioSource audio, AudioClip clip)
    {
        if (isSound && audio != null)
        {
            audio.clip = clip;
            audio.pitch = 1;
            audio.Play();
        }
    }

    public void ChangePitch(AudioSource source,float pitch,float duration) {
        DOTween.To(() => source.pitch, x => source.pitch = x, pitch, duration);
    }

    // public ObjectPool pool;

	// public void PoolAndPlay(GameObject soundObj, Transform t = null){

	// 	if (isSound) {

    //         GameObject g = pool.Pool(soundObj);

    //         if(t != null)
    //         {
    //             g.transform.position = t.position;
    //         }

    //         g.SetActive(true);
    //     }
	// }

    public void StopBG(AudioSource s) {
        //return;
        if(s != null)
            StartCoroutine(IE_StopBG(s));

    }

    IEnumerator IE_StopBG(AudioSource s) {
        float l = 0;

        while (l < 1.0f) {
            l += Time.deltaTime / .5f;
            s.pitch = Mathf.Lerp(1, 0,l);
            yield return null;
        }
        s.Stop();
    }

    public void HapticLight() {
//#if UNITY_IOS
//        if(MMVibrationManager.HapticsSupported())
//#endif
//        MMVibrationManager.Haptic(HapticTypes.LightImpact);
    }

    public void HapticMedium() {
//#if UNITY_IOS
//        if(MMVibrationManager.HapticsSupported())
//#endif
//        MMVibrationManager.Haptic(HapticTypes.MediumImpact);
    }

    public void HapticHeavy() {
//#if UNITY_IOS
//        if(MMVibrationManager.HapticsSupported())
//#endif
//        MMVibrationManager.Haptic(HapticTypes.HeavyImpact);
    }

    public void HapticFail() {
//#if UNITY_IOS
//        if(MMVibrationManager.HapticsSupported())
//#endif
//        MMVibrationManager.Haptic(HapticTypes.Failure);
    }

    public void HapticSuccess() {
//#if UNITY_IOS
//        if(MMVibrationManager.HapticsSupported())
//#endif
//        MMVibrationManager.Haptic(HapticTypes.Success);
    }
}
