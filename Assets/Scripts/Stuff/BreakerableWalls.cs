using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BreakerableWalls : MonoBehaviour, IDamageable
{
    [SerializeField]
    int health;
    private AudioSource audioSource;

    public void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0) Breake();
    }

    private void Breake()
    {
        audioSource.pitch = Random.Range(0.9f, 1.1f); // Adjust these values as needed
        audioSource.Play();
        
        Destroy(gameObject);
    }
}
