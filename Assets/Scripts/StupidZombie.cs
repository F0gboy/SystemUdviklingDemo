using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StupidZombie : MonoBehaviour
{
    public Transform playerTransform;
    private Rigidbody2D rb;
    GameObject player;
    private Vector2 movement;
    public float moveSpeed = 5f;
    public float health = 5;
    public bool alive = true;
    public GameObject bloodPool;

    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerTransform = player.transform;
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
            //Direction and Movement
            Vector3 direction = playerTransform.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            direction.Normalize();
            movement = direction;
            rb.rotation = angle;
        }



    }

    //Movement
    private void FixedUpdate()
    {
        if (alive == true && player.GetComponent<PlayerScript>().died == false )
        {
            moveCharacter(movement);
        }
    }

    void moveCharacter(Vector2 direction) 
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

}
