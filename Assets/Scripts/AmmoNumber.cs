using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AmmoNumber : MonoBehaviour
{
    public Image bulletImage;
    public GameObject gun;
    public GameObject inventory;
    float bullets;
    float maxbullets;
    float percentage;
    TextMeshProUGUI textBox;
    // Start is called before the first frame update
    void Start()
    {
        textBox = gameObject.GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inventory.transform.childCount == 1)
        {
            gun = inventory.GetComponent<Inventory>().weapon1;
        }
        if (inventory.transform.childCount > 1)
        {
            if (inventory.GetComponent<Inventory>().weapon1.activeInHierarchy == true)
            {
                gun = inventory.GetComponent<Inventory>().weapon1;
            }

            if (inventory.GetComponent<Inventory>().weapon2.activeInHierarchy == true)
            {
                gun = inventory.GetComponent<Inventory>().weapon2;
            }
        }

        if (inventory.transform.childCount > 0)
        {
            maxbullets = gun.GetComponent<RandomGun>().ammoMax;
            percentage = maxbullets / 100;

            bullets = gun.GetComponent<RandomGun>().ammo;
            textBox.text = bullets.ToString();
            bulletImage.fillAmount = (bullets / percentage) / 100;
        }
        if (inventory.transform.childCount == 0)
        {
            textBox.text = "0";
            bulletImage.fillAmount = 0;

        }


    }
}
        //Debug.Log((bullets / percentage) / 100 + " Bullet Percent");
        //Debug.Log(percentage + " Percent");
        //Debug.Log(bullets + " bullets");