using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundJukebox : MonoBehaviour
{


    [SerializeField] private AudioSource UFODieSoundSource;


    public void PlayUFODeathSound()
    {
        UFODieSoundSource.Play();
    }
}
