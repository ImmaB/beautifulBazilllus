using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance { get; private set; }
    public static bool muted { get; private set; } = true;
    
    [SerializeField] private AudioSource atmosphereAudio;
    [SerializeField] private AudioSource[] slimeAudios;
    [SerializeField] private AudioSource[] trainShakeAudios;


    private void Start()
    {
        if (instance) Debug.LogError("There can only be one SoundManager per Scene");
        instance = this;
        muted = false;
        atmosphereAudio.Play();
    }

    internal static void PlaySlime()
    {
        if (muted) return;
        instance.slimeAudios.GetRandom().Play();
    }

    internal static void PlayTrainShake()
    {
        if (muted) return;
        AudioSource trainShake = instance.trainShakeAudios.GetRandom(); 
        trainShake.Play();
    }

    internal static void StopAll()
    {
        instance.atmosphereAudio.Stop();
        muted = true;
    }
}
