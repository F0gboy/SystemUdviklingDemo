using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GameObject hud;
    public GameObject gameOver;
    public GameObject gun;
    public GameObject inventory;
    public GameObject blood;
    public int Health = 100;
    public bool died = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Health <= 0 && died == false)
        {
            died = true;
            Destroy(gameObject.GetComponent<PlayerController>());
            hud.SetActive(false);
            gameOver.SetActive(true);
            gameObject.GetComponent<Rigidbody2D>().angularDrag = 3;
            gameObject.GetComponent<Rigidbody2D>().drag = 3;
            Destroy(gameObject.GetComponent<CircleCollider2D>());
            gameObject.GetComponent<SpriteRenderer>().color = new Color(0.743f, 0.743f, 0.743f, 1);
            blood.GetComponent<BloodCircleScript>().alive = false;
            gameObject.GetComponent<SpriteRenderer>().sortingOrder = -3;


            //if (inventory.transform.childCount == 1)
            //{
            //    if (inventory.GetComponent<Inventory>().weapon1.activeInHierarchy == true)
            //    {
            //        gun = inventory.GetComponent<Inventory>().weapon1;
            //        Destroy(gun.GetComponent<RandomGun>());
            //        gun.AddComponent<Rigidbody2D>();
            //        gun.GetComponent<Rigidbody2D>().gravityScale = 0;
            //        gun.transform.SetParent(null);
            //        gun.GetComponent<Rigidbody2D>().AddTorque(10);
            //        gun.GetComponent<Rigidbody2D>().angularDrag = 3;

            //    }
            //}

            //if (inventory.transform.childCount > 1)
            //{
                if (inventory.GetComponent<Inventory>().weapon1.activeInHierarchy == true)
                {
                    gun = inventory.GetComponent<Inventory>().weapon1;
                    Destroy(gun.GetComponent<RandomGun>());
                    gun.AddComponent<Rigidbody2D>();
                    gun.GetComponent<Rigidbody2D>().gravityScale = 0;
                    gun.transform.SetParent(null);
                    gun.GetComponent<Rigidbody2D>().AddTorque(10);
                    gun.GetComponent<Rigidbody2D>().angularDrag = 3;

                }

                if (inventory.GetComponent<Inventory>().weapon2.activeInHierarchy == true)
                {
                    Destroy(gun.GetComponent<RandomGun>());
                    gun = inventory.GetComponent<Inventory>().weapon2;
                    gun.AddComponent<Rigidbody2D>();
                    gun.GetComponent<Rigidbody2D>().gravityScale = 0;
                    gun.transform.SetParent(null);
                    gun.GetComponent<Rigidbody2D>().AddTorque(10);
                    gun.GetComponent<Rigidbody2D>().angularDrag = 3;

                }
            //}

            

        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            Health -= 20;
        }
    }
}
