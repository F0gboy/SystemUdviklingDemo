using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakerableWalls : MonoBehaviour, IDamageable
{
    [SerializeField]
    int health;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0) Breake();
    }

    private void Breake()
    {
        Destroy(gameObject);
    }
}
