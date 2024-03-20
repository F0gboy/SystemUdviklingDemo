using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    RandomGun shooting;

    public GameObject myPlayer;

    public GameObject myWeapons;


    // Start is called before the first frame update
    void Start()
    {
        shooting = GetComponent<RandomGun>();
        myPlayer = GameObject.FindWithTag("Player");
        myWeapons = GameObject.FindWithTag("Weapons");

    }

    // Update is called once per frame
    void Update()
    {
        float Distance = Vector3.Distance(myPlayer.transform.position, this.transform.position);

        if (Input.GetKeyDown(KeyCode.E) && Distance < 1f && myWeapons.transform.childCount < 2)
        {       
            shooting.pickedUp = true;
            this.gameObject.transform.parent = myWeapons.transform;
            gameObject.transform.localPosition = new Vector3(0.35f, 0.45f, 0);
            gameObject.transform.localRotation = new Quaternion(0, 0, 0, 0);
            Debug.Log(myWeapons.transform.childCount + " guns");

        }
    }
}
