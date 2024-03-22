using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomGun : MonoBehaviour
{
    #region Variables
    private float magX = 0.15f;
    private float magY = 0.1f;
    private Vector3 magSize;
    public bool reloading = false;
    private float barrelLenght;
    public bool pickedUp = false;
    int scopeNum;

    [Header("Gun Stats")]
    public int ammo;
    public int ammoMax;
    public float reloadTime;
    public float bulletForce = 10f;

    [Tooltip("How inaccurate the weapon is")]
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
    private SpriteRenderer baseRenderer;
    private SpriteRenderer barrelRenderer;
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        baseRenderer = baseRend.GetComponent<SpriteRenderer>();
        barrelRenderer = barrelRend.GetComponent<SpriteRenderer>();

        recoil = (recoil - (barrelLenght * 0.01f)) * 10;
        switch (ammoMax)
        {
            case int a when ammoMax > 9:
                magY += 0.1f;
                break;

            case int b when ammoMax > 14:
                magY += 0.2f;
                break;

            case int c when ammoMax > 19:
                magY += 0.3f;
                break;

            case int d when ammoMax > 24:
                magX += 0.05f;
                magY += 0.4f;
                break;

            case int e when ammoMax > 29:
                magX += 0.1f;
                magY += 0.5f;
                break;

            case int f when ammoMax > 34:
                magX += 0.15f;
                magY += 0.5f;
                break;

            case int g when ammoMax > 39:
                magX += 0.25f;
                magY += 0.5f;
                break;

            case int h when ammoMax > 44:
                magX += 0.35f;
                magY += 0.5f;
                break;

            case int i when ammoMax > 44:
                magX += 0.45f;
                break;
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
        if (pickedUp == false)
        {
            baseRenderer.sortingOrder = 0;
            barrelRenderer.sortingOrder = 0;
            return;
        }

        baseRenderer.sortingOrder = 1;
        barrelRenderer.sortingOrder = 1;

        if (reloading == false)
        {
            //Controls
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (ammo == 0)
                    StartCoroutine("Reload");
                else
                    Shoot(); ammo--;
            }

            if (Input.GetKeyDown(KeyCode.R))
                StartCoroutine("Reload");
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
