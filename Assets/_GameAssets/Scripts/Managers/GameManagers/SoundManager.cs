using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioClip[] sounds;
    public static SoundManager instance;
    private void Start()
    {
        if (instance != null) { return; }
        else
            instance = this;
    }

    public void PlayFx(int index,AudioSource source, float volume=0.5f)
    {
        source.PlayOneShot(sounds[index],volume);
    }
}
