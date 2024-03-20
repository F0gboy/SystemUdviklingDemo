using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    private float magX = 0.15f;
    private float magY = 0.1f;
    private Vector3 magSize;
    public bool reloading = false;
    private float accuracy = 10;
    private float recoil;
    private float barrelLenght;

    public bool pickedUp = false;

    [Header("Gun Stats")]
    public int ammo;
    public int ammoMax;
    public float reloadTime;
    public float bulletForce = 10f;

    [Header("Gun Props")]
    public GameObject barrel;
    public GameObject shell;
    public GameObject caliber;
    public GameObject magazin;

    [Header("Gun Physics")]
    public Transform magPoint;
    public Transform firePoint;
    public Transform ejectPoint;
    public Quaternion ejectQuaternion;

    private void Start()
    {
        

        #region Lad ikke Eskil eller Blom Se dette
        #region Jeg mener det

        if (ammoMax > 9)
        {
            magX = magX + 0f;
            magY = magY + 0.1f;
            if (ammoMax > 14)
            {
                magX = magX + 0f;
                magY = magY + 0.1f;
                if (ammoMax > 19)
                {
                    magX = magX + 0f;
                    magY = magY + 0.1f;
                    if (ammoMax > 24)
                    {
                        magX = magX + 0.05f;
                        magY = magY + 0.1f;
                        if (ammoMax > 29)
                        {
                            magX = magX + 0.05f;
                            magY = magY - 0.1f;
                            if (ammoMax > 34)
                            {
                                magX = magX + 0.15f;
                                magY = magY + 0f;
                                if (ammoMax > 39)
                                {
                                    magX = magX + 0.1f;
                                    magY = magY + 0f;
                                    if (ammoMax > 44)
                                    {
                                        magX = magX + 0.1f;
                                        magY = magY + 0f;
                                        if (ammoMax > 49)
                                        {
                                            magX = magX + 0f;
                                            magY = magY + 0.1f;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }
        #endregion
        #endregion
        magSize = new Vector3(magX, magY, 1);
    }


    // Update is called once per frame
    void Update()
    {
        //Controls
        if (Input.GetKeyDown(KeyCode.Mouse0) && reloading == false && pickedUp == true) { if (ammo == 0) { StartCoroutine("Reload"); } else { Shoot(); ammo--; } };
        if (Input.GetKeyDown(KeyCode.R) && reloading == false && pickedUp == true) { StartCoroutine("Reload"); }



    }

    #region Shooting
    void Shoot()
    {

        //Bullet Shooting
        GameObject bullet = Instantiate(caliber, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        ejectQuaternion = ejectPoint.rotation;
        ejectQuaternion.z = ejectQuaternion.z + Random.Range(-2.0f, 2.0f);

        //Casing Ejection
        GameObject casing = Instantiate(shell, ejectPoint.position, ejectPoint.rotation);
        Rigidbody2D rb2 = casing.GetComponent<Rigidbody2D>();
        rb2.AddForce(ejectPoint.up * (bulletForce / 6.5f + Random.Range(-2.0f, 2.0f)), ForceMode2D.Impulse);
        rb2.AddForce(ejectPoint.right * Random.Range(-1.0f, 1.0f), ForceMode2D.Impulse);
        rb2.AddTorque(180 + Random.Range(-10, 10), ForceMode2D.Impulse);

    }

    #endregion

    #region Reloading
    public IEnumerator Reload()
    {
        //Reload
        reloading = true;
        yield return new WaitForSeconds(reloadTime);
        ammo = ammoMax;
        reloading = false;

        //Magazin
        GameObject mag = Instantiate(magazin, magPoint.position, magPoint.rotation);
        mag.transform.localScale = magSize;
        Rigidbody2D rb3 = magazin.GetComponent<Rigidbody2D>();
        rb3.AddTorque(180 + Random.Range(-30, 30), ForceMode2D.Impulse);


    }
    #endregion


}
