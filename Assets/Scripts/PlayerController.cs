using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerController : MonoBehaviour
{
    [Header("Movement settings")]
    public float sneakSpeed; 
    public float walkSpeed, runSpeed;
    public State state;
    private float speed;
    Rigidbody2D rb;

    [Header("Health and gun settings")]
    public float maxHealth;
    public float gunCorrectionValue = .65f;
    private float health;
    //public GameObject gun;
    [SerializeField] GameObject inventory;

    [Header("Key Inputs")]
    public bool toggleKey; //Set key to be toggled if true, else you have to hold the key down
    public KeyCode sneakKey;
    public KeyCode runKey;

    [Header("Particle settings")]
    public bool turnOnParticles;
    public GameObject walkingParticlesPrefab;
    private GameObject lastParticle;
    public float rgbOffset, particleIntencitySneaking, particleIntencityWalking, particleIntencityRunning;
    public LayerMask floorHitLayer;
    
    [Header("Audio Settings")]
    public AudioClip[] footstepSounds; // Assign two footstep sounds in the inspector
    private AudioSource audioSource;
    public float footstepDelayWalk = 0.5f; // Delay between steps while walking
    public float footstepDelayRun = 0.3f; // Delay between steps while running
    public float footstepDelaySneak = 0.7f; // Delay between steps while sneaking
    private float nextFootstepTime = 0f;
    private int lastFootstepIndex = 0; // To track the last played footstep sound

    private void Start()
    {
        //Creates a new formatting for text
        #region Text formatting
        System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
        customCulture.NumberFormat.NumberDecimalSeparator = ".";

        System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        #endregion

        // Your existing start setup
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
        speed = walkSpeed;
        state = State.walking;

        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        //if (inventory.transform.childCount == 1)
        //{
        //    gun = inventory.GetComponent<Inventory>().weapon1;
        //}
        //if (inventory.transform.childCount > 1)
        //{
        //    if (inventory.GetComponent<Inventory>().weapon1.activeInHierarchy == true)
        //    {
        //        gun = inventory.GetComponent<Inventory>().weapon1;
        //    }

        //    if (inventory.GetComponent<Inventory>().weapon2.activeInHierarchy == true)
        //    {
        //        gun = inventory.GetComponent<Inventory>().weapon2;
        //    }
        //}
        //Checks if the player is dead and stops the time
        if (health <= 0)
        {
            Debug.Log("Dead");
            Time.timeScale = 0;
        }

        if (turnOnParticles)
        {
            //Creates new particles and updates their relative color and burst
            #region Walking particles control
            if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0)
            {
                if (!lastParticle || transform.position != lastParticle.transform.position)
                {
                    GameObject newParticleSystem = Instantiate(walkingParticlesPrefab, transform.position, Quaternion.identity);
                    lastParticle = newParticleSystem;
                    UpdateParticleColors(lastParticle.GetComponent<ParticleSystem>());
                    UpdateParticleBurst(lastParticle.GetComponent<ParticleSystem>());
                }
            }
            #endregion
        }

        //Sets the movementState of the player and changes the speed
        #region Movementstate control
        if (toggleKey)
        {
            if (state != State.sneaking && Input.GetKeyDown(sneakKey))
            {
                state = State.sneaking;
                speed = sneakSpeed;
            }
            else if (state == State.sneaking && Input.GetKeyDown(sneakKey))
            {
                state = State.walking;
                speed = walkSpeed;
            }
            else if (state != State.running && Input.GetKeyDown(runKey))
            {
                state = State.running;
                speed = runSpeed;
            }
            else if (state == State.running && Input.GetKeyDown(runKey))
            {
                state = State.walking;
                speed = walkSpeed;
            }
        }
        else
        {
            if (state != State.running && Input.GetKey(sneakKey))
            {
                state = State.sneaking;
                speed = sneakSpeed;
            }
            else if (state != State.sneaking && Input.GetKey(runKey))
            {
                state = State.running;
                speed = runSpeed;
            }
            else
            {
                state = State.walking;
                speed = walkSpeed;
            }
        }
        #endregion

        //Gets mouse position and rotates the player and gun towards the mouse
        #region Rotation controls
        //Gets mouse position
        Vector3 mousePos = Input.mousePosition;
        mousePos += Camera.main.transform.forward * -10f;
        var aim = Camera.main.ScreenToWorldPoint(mousePos);
        //Rotates the player and the gun towards the mouse position
        Rotate(gameObject, aim, -90);
        //if (Vector2.Distance(transform.position, aim) > gunCorrectionValue)
        //{
        //    Rotate(gun, aim, -90);
        //}
        #endregion
    }

    private void FixedUpdate()
    {
        // Your existing movement code
        float step = speed * Time.fixedDeltaTime;
        Vector2 movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")).normalized;
        rb.velocity = movement * step;

        // Play footstep sounds if moving
        if (movement.magnitude > 0 && Time.time >= nextFootstepTime)
        {
            PlayFootstepSound();
        }
    }

    //Updates the burst settings of the relative particlesystem
    void UpdateParticleBurst(ParticleSystem systemToUpdate)
    {
        //Creates new burst setting and switches it to the correct settings according to the current movement state
        ParticleSystem.Burst newBurst = systemToUpdate.emission.GetBurst(0);
        switch (state)
        {
            case State.sneaking:
                newBurst.probability = particleIntencitySneaking;
                break;
            case State.walking:
                newBurst.probability = particleIntencityWalking;
                break;
            case State.running:
                newBurst.probability = particleIntencityRunning;
                break;
            default:
                newBurst.probability = particleIntencitySneaking;
                break;
        }
        //Applys the new burst setting
        lastParticle.GetComponent<ParticleSystem>().emission.SetBurst(0, newBurst);
    }

    //Updates the color of the relative particlesystem
    void UpdateParticleColors(ParticleSystem systemToUpdate)
    {
        //Finds walkable layer underneath player
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector3.forward, Mathf.Infinity, floorHitLayer);
        if (hit)
        {
            GameObject tileObj = hit.collider.gameObject;
            if (tileObj.layer == 3)
            {
                //Sets colors to average
                Color32 color = GetAverageColor(tileObj.GetComponent<SpriteRenderer>().sprite.texture);

                //Applys the color to the particlesystem
                Gradient grad = new Gradient();
                grad.SetKeys(new GradientColorKey[] { new GradientColorKey(color, 0.0f), new GradientColorKey(color, 1.0f) }, new GradientAlphaKey[] { new GradientAlphaKey(1.0f, 0.0f), new GradientAlphaKey(0.0f, 1.0f) });
                var col = systemToUpdate.GetComponent<ParticleSystem>().colorOverLifetime;
                col.color = grad;
            }
        }
    }

    //Gets average rgb value from texture
    Color32 GetAverageColor(Texture2D texture)
    {
        //Finds all rgb values
        Color32[] textureColors = texture.GetPixels32();
        int length = textureColors.Length;
        int emptyPixelCount = 0;
        float r = 0, g = 0, b = 0;
        for (int i = 0; i < length; i++)
        {
            if (textureColors[i].r == 0 && textureColors[i].g == 0 && textureColors[i].b == 0 && textureColors[i].a == 0)
            {
                emptyPixelCount++;
            }

            r += textureColors[i].r;

            g += textureColors[i].g;

            b += textureColors[i].b;
        }

        //Returns the average rgb value
        return new Color32((byte)((r / (length - emptyPixelCount)) + rgbOffset), (byte)((g / (length - emptyPixelCount)) + rgbOffset), (byte)((b / (length - emptyPixelCount)) + rgbOffset), 255);
    }

    //Rotates an object towards another object
    void Rotate(GameObject objectToRotate, Vector3 objectToRotateTowards, float angleOffset)
    {
        //Finds the relative angle between two objects/positions and converts it to quaternion and applys offset
        var relativePos = objectToRotateTowards - objectToRotate.transform.position;
        var angle = Mathf.Atan2(relativePos.y, relativePos.x) * Mathf.Rad2Deg;
        var rotation = Quaternion.AngleAxis(angle + angleOffset, Vector3.forward);
        objectToRotate.transform.rotation = rotation;
    }
    
    void PlayFootstepSound()
    {
        float delay = footstepDelayWalk; // Default to walking speed
        switch (state)
        {
            case State.sneaking:
                delay = footstepDelaySneak;
                break;
            case State.running:
                delay = footstepDelayRun;
                break;
        }

        // Alternate between the two footstep sounds
        lastFootstepIndex = 1 - lastFootstepIndex; // Alternates between 0 and 1
        audioSource.pitch = Random.Range(0.85f, 1.15f); // Add slight random pitch variation
        audioSource.PlayOneShot(footstepSounds[lastFootstepIndex]);

        nextFootstepTime = Time.time + delay;
    }


    public void TakeDamage(float amount)
    {
        //Reduces the health of the player
        health -= amount;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            //collision.gameObject
        }
    }
}
