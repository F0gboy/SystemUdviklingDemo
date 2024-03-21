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
        
        audioSource.clip = breakSound; // Ensure the clip is assigned
        audioSource.pitch = Random.Range(0.9f, 1.1f); // Adjust these values as needed
        audioSource.Play();
        
        health -= amount;
        if (health <= 0) Breake();
    }

    private void Breake()
    {

        Console.WriteLine("PLAYED");
        
        Destroy(gameObject);
    }
}
