using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StupidZombie : MonoBehaviour
{
    public Transform destination;
    private Rigidbody2D rb;
    GameObject player;
    private Vector2 movement;
    public float moveSpeed = 5f;
    public float health = 5;
    public bool alive = true;
    public GameObject bloodPool;

    private NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        agent = GetComponent<NavMeshAgent>();
        agent.updateUpAxis = false;
        agent.updateRotation = false;
    }

    // Update is called once per frame
    void Update()
    {   
        if (health == 0 || health < 0)
        {
            alive = false;
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(61,92,60, 255);
            gameObject.GetComponent<Collider2D>().enabled = false;
            Destroy(GetComponent<Rigidbody2D>());
            bloodPool.GetComponent<BloodCircleScript>().alive = false;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = -3;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = new Color32(136, 219, 135, 255);
            gameObject.GetComponent<Collider2D>().enabled = true;
        }

        if (alive == true)
        {
            if (!player.GetComponent<PlayerScript>().died)
            {
                agent.SetDestination(player.transform.position);
            }
        }
    }

    private void FixedUpdate()
    {

    }
}
