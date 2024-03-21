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
    private float cooldown;
    protected Rigidbody2D rb;
    protected GameObject player;
    private NavMeshAgent agent;
    
    public AudioClip[] hitSounds; // Array of hit sounds
    private AudioSource audioSource;

    [SerializeField] public bool alive = true;
    [SerializeField] protected int health = 5;
    [SerializeField] protected int damage = 2;
    [SerializeField] protected float damageRate = 0.5f;
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
        
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        if (alive == false) return;

        //Update target if player is within radius
        Vector3 dir = player.transform.position - transform.position;
        if (dir.magnitude < detectionRange)
            agent.SetDestination(player.transform.position);

        //Exists so enemy can't deal damage every frame
        if (cooldown > 0) cooldown -= Time.deltaTime;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == player && cooldown <= 0)
        {
            Debug.Log("Enemy dealt damage to player");
            player.GetComponent<PlayerScript>().Health -= Damage;
            cooldown = damageRate;
        }
    }

    public void TakeDamage(int amount)
    {
        OnHit();
        
        Health -= amount;
        if (Health <= 0) Die();
    }
    
    public void OnHit()
    {
        if (hitSounds.Length > 0)
        {
            int index = Random.Range(0, hitSounds.Length);
            
            audioSource.pitch = Random.Range(0.9f, 1.1f); // Adjust these values as needed
            audioSource.PlayOneShot(hitSounds[index]);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
