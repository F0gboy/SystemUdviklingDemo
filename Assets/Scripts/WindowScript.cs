using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowScript : MonoBehaviour
{
    [SerializeField] GameObject shard;
    [SerializeField] AudioSource breaking;
    int shards;
    
    // Start is called before the first frame update
    void Start()
    {
        breaking.pitch = breaking.pitch + (Random.Range(-0.30f, 0.31f));
    }



    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7) { StartCoroutine("Shards"); breaking.Play(); }
        
    }

    public IEnumerator Shards()
    {
        Destroy(gameObject.GetComponent<SpriteRenderer>());
        Destroy(gameObject.GetComponent<BoxCollider2D>());

        if (shards == 50)
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);

        }
        else 
        {
            shards++;
            GameObject shardEject = Instantiate(shard, new Vector3(gameObject.transform.position.x ,gameObject.transform.position.y + Random.Range(-1f, 1.1f) , gameObject.transform.position.z), gameObject.transform.rotation);
            Rigidbody2D rb2 = shardEject.GetComponent<Rigidbody2D>();
            rb2.AddForce(gameObject.transform.up * (Random.Range(-2.0f, 2.0f)), ForceMode2D.Impulse);
            rb2.AddForce(gameObject.transform.right * Random.Range(-2.0f, 2.0f), ForceMode2D.Impulse);
            rb2.AddTorque(180 + Random.Range(-10, 10), ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0);
        StartCoroutine("Shards");
    }

    }
