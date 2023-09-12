using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    private AudioSource audioSource; // Reference to an AudioSource component

    private void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Assuming AudioManager has an AudioSource component

        EventManager.OnPlaySound.AddListener(PlaySound);
    }

    private void PlaySound(SoundData soundData)
    {
        if (soundData != null && soundData.audioClip != null)
        {
            // Set the AudioSource properties based on the soundData
            audioSource.clip = soundData.audioClip;
            audioSource.volume = soundData.volume;
            audioSource.pitch = soundData.pitch;

            // Play the sound
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Invalid soundData or AudioClip.");
        }
    }
}
