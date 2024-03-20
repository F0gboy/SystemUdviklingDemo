using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
    public GameObject doorFixed;
    public GameObject doorDestroyed;
    public GameObject doorOther;


    int doorHealth;

    // Start is called before the first frame update
    void Start()
    {
        doorHealth = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7) { doorHealth--; }
        if (doorHealth <= 0) { doorDestroyed.SetActive(true); doorDestroyed.GetComponent<AudioSource>().Play();  doorFixed.SetActive(false); doorOther.SetActive(false);  gameObject.GetComponent<BoxCollider2D>().enabled = false; }
    }
}
