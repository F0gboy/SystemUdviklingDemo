using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject myWeapons;

    public GameObject weapon1;
    public GameObject weapon2;

    public RandomGun shooting1;
    public RandomGun shooting2;
        
    // Update is called once per frame
    void Update()
    {

        if (myWeapons.transform.childCount == 1)
        {
            weapon1 = myWeapons.transform.GetChild(0).gameObject;
            weapon1.SetActive(true);
            if (weapon1.GetComponent<RandomGun>())
            {
                shooting1 = weapon1.GetComponent<RandomGun>();
            }
        }
        if (myWeapons.transform.childCount > 1) 
        {
            weapon1 = myWeapons.transform.GetChild(0).gameObject;
            weapon2 = myWeapons.transform.GetChild(1).gameObject;
            if (weapon2.GetComponent<RandomGun>())
            {
                shooting2 = weapon2.GetComponent<RandomGun>();
            }
        }
        
        

        if (Input.GetKeyDown(KeyCode.Q) && myWeapons.transform.childCount > 1 && shooting1.reloading == false && shooting2.reloading == false)
        {
            weapon1.SetActive(!weapon1.activeInHierarchy);
            weapon2.SetActive(!weapon2.activeInHierarchy);
            Debug.Log("Switched Weapons");
        }

        if (Input.GetKeyDown(KeyCode.Z) && shooting1.reloading == false)
        {
            if (myWeapons.transform.childCount == 1)
            {
                shooting1.pickedUp = false;
                weapon1.transform.parent = null;
                Debug.Log("Dropped 1 Weapon");
            }
            if (myWeapons.transform.childCount > 1)
            {
                if (weapon2.activeInHierarchy == true)
                {   
                    shooting2.pickedUp = false;
                    weapon2.transform.parent = null;
                    Debug.Log("Dropped Weapon 2");

                }

                if (weapon1.activeInHierarchy == true)
                {
                    shooting1.pickedUp = false;
                    weapon1.transform.parent = null;
                    Debug.Log("Dropped Weapon 1");

                }



            }
        }

        if (myWeapons.transform.childCount == 1 && weapon1.activeInHierarchy == false)
        {
            Debug.Log("Last Weapon Made Active");
            weapon1.SetActive(true);
        }

        if (weapon1 && weapon2 && weapon1.activeInHierarchy == true && weapon2.activeInHierarchy == true & myWeapons.transform.childCount > 1)
        {
            weapon2.SetActive(false);
            Debug.Log("Made Second Weapon Inactive");
        }




    }
}   