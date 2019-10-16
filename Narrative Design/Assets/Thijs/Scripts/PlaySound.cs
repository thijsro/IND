using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    [SerializeField] AudioClip soundToPlay;

    AudioSource myAudioSource;

    private void Start() 
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    public void UseSoundManager()
    {
        myAudioSource.PlayOneShot(soundToPlay);
    }
}
