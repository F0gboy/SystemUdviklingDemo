using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public ParticleSystem Blood;
    private AudioSource audioSource;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable hitObject = collision.gameObject.GetComponent<IDamageable>();
        if (hitObject != null)
        {
            Blood.Play();
            hitObject.TakeDamage(10);
        }

        audioSource.Play();

        //Disable all components
        Destroy(GetComponent<Rigidbody2D>());
        Destroy(GetComponent<SpriteRenderer>());
        Destroy(GetComponent<BoxCollider2D>());

        //Destroy rest later so audio and particles can play out
        Destroy(gameObject, 1);
    }
}
