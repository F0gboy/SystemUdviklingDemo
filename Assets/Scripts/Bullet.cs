using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem Blood;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 4);
        

    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6) 
        { 
            Blood.Play(); 
            gameObject.GetComponent<AudioSource>().Play();
            Destroy(GetComponent<Rigidbody2D>()); 
            Destroy(GetComponent<SpriteRenderer>()); 
            Destroy(GetComponent<BoxCollider2D>());
            StupidZombie stupidZombie = collision.gameObject.GetComponent<StupidZombie>();
            stupidZombie.health--;
            Destroy(gameObject, 1); 
        }


        else
        {
            if (collision.gameObject.layer == 7) { }
            else
            {
                Destroy(gameObject);
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {


    }

  
}
