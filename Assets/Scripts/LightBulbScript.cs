using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBulbScript : MonoBehaviour
{
    [SerializeField] GameObject shard;
    [SerializeField] GameObject light;

    int shards = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 7) { StartCoroutine("BulbBlow"); gameObject.GetComponent<AudioSource>().Play(); }

    }

    public IEnumerator BulbBlow()
    {
        Destroy(gameObject.GetComponent<CircleCollider2D>());
        Destroy(light);

        if (shards == 25)
        {
            yield return new WaitForSeconds(1f);
            Destroy(gameObject);

        }
        else
        {
            shards++;
            GameObject shardEject = Instantiate(shard, new Vector3(gameObject.transform.position.x, gameObject.transform.position.y, gameObject.transform.position.z), gameObject.transform.rotation);
            Rigidbody2D rb2 = shardEject.GetComponent<Rigidbody2D>();
            rb2.AddForce(gameObject.transform.up * (Random.Range(-1.0f, 1.0f)), ForceMode2D.Impulse);
            rb2.AddForce(gameObject.transform.right * Random.Range(-1.0f, 1.0f), ForceMode2D.Impulse);
            rb2.AddTorque(180 + Random.Range(-10, 10), ForceMode2D.Impulse);
        }
        yield return new WaitForSeconds(0);
        StartCoroutine("BulbBlow");
    }
}
