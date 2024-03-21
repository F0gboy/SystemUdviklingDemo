using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public interface IDamageable
{
    public void TakeDamage(int amount);
}

public class Enemy : MonoBehaviour, IDamageable
{
    protected Rigidbody2D rb;
    protected GameObject player;
    private NavMeshAgent agent;

    [SerializeField] public bool alive = true;
    [SerializeField] protected int health = 5;
    [SerializeField] protected int damage = 2;
    [SerializeField] protected int detectionRange = 10;
    public int Health { get => health; set { health = value; if (health < 0) health = 0; } } //Sets to 0 if it goes negative
    public int Damage { get => damage; set => damage = value; }

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
        player = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //Move towards player if it's within radius
        //Vector3 dir = player.transform.position - transform.position;
        //if (dir.magnitude < detectionRange)
        //    transform.position += dir.normalized * Time.deltaTime;

        //Update target direction
        if (alive == true && player.GetComponent<PlayerScript>().died == false)
            agent.SetDestination(player.transform.position);

    }

    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <= 0) Die();
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
