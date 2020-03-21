using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager instance;
    
    [SerializeField] private AudioSource atmosphereAudio;
    [SerializeField] private AudioSource[] slimeAudios;
    [SerializeField] private AudioSource[] trainShakeAudios;

    private void Start()
    {
        if (instance) Debug.LogError("There can only be one SoundManager per Scene");
        instance = this;
        atmosphereAudio.Play();
    }

    internal static void PlaySlime()
    {
        instance.slimeAudios[Rand.Int(instance.slimeAudios.Length)].Play();
    }

    internal static void PlayTrainShake()
    {
        instance.trainShakeAudios[Rand.Int(instance.trainShakeAudios.Length)].Play();
    }
}
