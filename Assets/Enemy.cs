using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public void TakeDamage(int amount);
}

public class Enemy : MonoBehaviour, IDamageable
{
    protected Rigidbody2D rb;
    protected GameObject player;

    [SerializeField] protected int health = 5;
    [SerializeField] protected int damage = 2;
    [SerializeField] protected int detectionRange = 10;
    public int Health { get => health; set { health = value; if (health < 0) health = 0; } } //Sets to 0 if it goes negative
    public int Damage { get => damage; set => damage = value; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if ((player.transform.position - transform.position).magnitude < 5)
    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0) Die();
    }

    private void Die()
    {

    }
}
