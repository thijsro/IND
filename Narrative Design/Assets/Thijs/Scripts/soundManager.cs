using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class soundManager : MonoBehaviour
{
    AudioSource myAudioSource;
    // Start is called before the first frame update
    void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip SoundToPlay)
    {
        myAudioSource.PlayOneShot(SoundToPlay);
    }
}
