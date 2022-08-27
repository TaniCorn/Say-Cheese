using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundJukebox : MonoBehaviour
{


    [SerializeField] private AudioSource UFODieSoundSource;
    [SerializeField] private AudioSource UFOFlySoundSource;
    private List<GameObject> activeUFOs;

    private bool isFlySoundPlaying;

    private void Start()
    {
        activeUFOs = new List<GameObject>();
        isFlySoundPlaying = false;
    }

    public void AddUFO(GameObject UFO)
    {
        activeUFOs.Add(UFO);
    }

    public void RemoveUFO(GameObject UFO)
    {
        activeUFOs.Remove(UFO);
    }
    
    private void Update()
    {
        if (activeUFOs.Count > 0 && isFlySoundPlaying == false)
        {
            isFlySoundPlaying = true;
            PlayFlySound();
        }

        if (activeUFOs.Count == 0 && isFlySoundPlaying == true)
        {
            isFlySoundPlaying = false;
            StopFlySound();
        }
    }

    public void PlayUFODeathSound()
    {
        UFODieSoundSource.Play();
    }

    public void PlayFlySound()
    {
        if(UFOFlySoundSource.isPlaying == false)
            UFOFlySoundSource.Play();
    }

    public void StopFlySound()
    {
        UFOFlySoundSource.Stop();
    }
    
}
