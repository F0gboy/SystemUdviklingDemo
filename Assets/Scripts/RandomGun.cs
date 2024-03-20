using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGun : MonoBehaviour
{
    private float magX = 0.15f;
    private float magY = 0.1f;
    private Vector3 magSize;
    public bool reloading = false;
    private float barrelLenght;
    public bool pickedUp = false;
    float color;
    int scopeNum;

    [Header("Gun Stats")]
    public int ammo;
    public int ammoMax;
    public float reloadTime;
    public float bulletForce = 10f;
    public float accuracy = 10;
    public float recoil = 0.1f;
    public float lightZeit = 0.3f;

    [Header("Gun Props")]
    public GameObject barrel;
    public GameObject shell;
    public GameObject caliber;
    public GameObject magazin;
    public ParticleSystem Flash;
    public GameObject[] scopes;
    public GameObject light;

    [Header("Gun Physics")]
    public Transform magPoint;
    public Transform firePoint;
    public Transform ejectPoint;
    public Quaternion ejectQuaternion;

    [Header("Gun Sounds")]  
    public AudioSource reloadSound;

    [Header("Gun Renderer")]
    public GameObject baseRend;
    public GameObject barrelRend;

    private float pitch = 1;
    private void Start()
    {
        //ammoMax = Random.Range(5, 50);
        //barrel.transform.localScale = new Vector3(1, Random.Range(0, 10), 1);
        //barrelLenght = barrel.transform.localScale.y;

        //color = Random.Range(0.196f, 0.784f);
        //baseRend.GetComponent<SpriteRenderer>().color =  new Color(color,color,color, 1);
        //barrelRend.GetComponent<SpriteRenderer>().color = new Color(color, color, color, 1);

        //ammo = ammoMax;
        //accuracy = (accuracy / ((barrelLenght / accuracy) + 1)) + (ammoMax / accuracy) / accuracy; ;
        //bulletForce = 20 * ((barrelLenght / 10) + 1);
        //reloadTime = 2 + (ammoMax / 10) * ((barrelLenght / 10) + 1);


        recoil = (recoil - (barrelLenght * 0.01f)) * 10;


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

        magSize = new Vector3(magX, magY, 1);

        pitch = 1 + (Random.Range(-0.30f, 0.31f));

        scopeNum = Random.Range(1, 6);

        if (scopeNum == 1)
        {

        }
        if (scopeNum == 2)
        {
            scopes[0].SetActive(true);
        }
        if (scopeNum == 3)
        {
            scopes[1].SetActive(true);
        }
        if (scopeNum == 4)
        {
            scopes[2].SetActive(true);
        }
        if (scopeNum == 5)
        {
            scopes[3].SetActive(true);
        }

    }


    // Update is called once per frame
    void Update()
    {
        //Controls
        if (Input.GetKeyDown(KeyCode.Mouse0) && reloading == false && pickedUp == true) { if (ammo == 0) { StartCoroutine("Reload"); } else { Shoot(); ammo--; } };
        if (Input.GetKeyDown(KeyCode.R) && reloading == false && pickedUp == true) { StartCoroutine("Reload"); }


        if (pickedUp == true)
        {
            baseRend.GetComponent<SpriteRenderer>().sortingOrder = 1;
            barrelRend.GetComponent<SpriteRenderer>().sortingOrder = 1;
        }
        else
        {
            baseRend.GetComponent<SpriteRenderer>().sortingOrder = 0;
            barrelRend.GetComponent<SpriteRenderer>().sortingOrder = 0;
        }
        



    }

    #region Shooting
    void Shoot()
    {

        //Bullet Shooting
        StartCoroutine("LightUp");
        GameObject bullet = Instantiate(caliber, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.up * bulletForce, ForceMode2D.Impulse);
        rb.AddForce(firePoint.right * Random.Range(-accuracy, accuracy), ForceMode2D.Impulse);
        ejectQuaternion = ejectPoint.rotation;
        ejectQuaternion.z = ejectQuaternion.z + Random.Range(-2.0f, 2.0f);

        Flash.Play();
        


        //Casing Ejection
        GameObject casing = Instantiate(shell, ejectPoint.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
        Rigidbody2D rb2 = casing.GetComponent<Rigidbody2D>();
        AudioSource shotSound = casing.GetComponent<AudioSource>();
        shotSound.pitch = pitch;
        rb2.AddForce(ejectPoint.up * (bulletForce / 6.5f + Random.Range(-2.0f, 2.0f)), ForceMode2D.Impulse);
        rb2.AddForce(ejectPoint.right * Random.Range(-1.0f, 1.0f), ForceMode2D.Impulse);
        rb2.AddTorque(180 + Random.Range(-10, 10), ForceMode2D.Impulse);
        Debug.Log(light.activeSelf);
        
    }

    #endregion

    #region Reloading

    public IEnumerator LightUp()
    {
        light.SetActive(true);
        yield return new WaitForSeconds(lightZeit);
        light.SetActive(false);
    }
    public IEnumerator Reload()
    {
        //Reload1
        reloading = true;
        reloadSound.Play();

        //Magazin
        GameObject mag = Instantiate(magazin, magPoint.position, Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360))));
        mag.transform.localScale = magSize;

        //Reload2
        yield return new WaitForSeconds(reloadTime);
        ammo = ammoMax;
        reloadSound.Stop();
        reloading = false;

        



    }
    #endregion


}
