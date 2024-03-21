using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BreakerableWalls : MonoBehaviour, IDamageable
{
    [SerializeField]
    int health;
    
    [SerializeField]
    private AudioClip breakSound;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        // Assign the clip to the AudioSource (if you prefer it here)
        audioSource.clip = breakSound;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0) Breake();
    }

    private void Breake()
    {
        // Create a new temporary GameObject
        GameObject tempAudioSource = new GameObject("TempAudio");
        AudioSource audioSource = tempAudioSource.AddComponent<AudioSource>();
    
        // Copy your audio source settings if needed (e.g., volume, spatial blend, etc.)
        audioSource.volume = 0.1f;
        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.spatialBlend = this.audioSource.spatialBlend;

        // Assign the break sound and play it
        audioSource.clip = breakSound;
        audioSource.Play();

        // Destroy the temporary audio source after the clip finishes
        Destroy(tempAudioSource, audioSource.clip.length);

        // Now destroy the game object without worrying about stopping the sound
        Destroy(gameObject);
    }
}
