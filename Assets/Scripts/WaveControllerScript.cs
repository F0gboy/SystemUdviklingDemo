using NavMeshPlus.Components;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaveControllerScript : MonoBehaviour
{
    private NavMeshSurface surface;
    public GameObject Zombie;
    public float timer;
    [SerializeField] bool obstructed = false;

    public float nearestSpawnRadius = 1f;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(AutoWave));
    }

    // Update is called once per frame
    void Update()
    {
        //if(Input.GetKeyDown(KeyCode.I))
        //{
        //    spawn = !spawn;
        //}

        //if (Input.GetKeyDown(KeyCode.O))
        //{
        //    Instantiate(Zombie, gameObject.transform);
        //}

        //if (Input.GetKeyDown(KeyCode.P))
        //{
        //    Instantiate(Zombie, gameObject.transform);
        //    Instantiate(Zombie, gameObject.transform);
        //    Instantiate(Zombie, gameObject.transform);
        //    Instantiate(Zombie, gameObject.transform);
        //    Instantiate(Zombie, gameObject.transform);
        //}



    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        obstructed = true;
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        obstructed = false;

    }
    public IEnumerator AutoWave()
    {
        if (!obstructed && NavMesh.SamplePosition(gameObject.transform.position, out NavMeshHit hit, nearestSpawnRadius, NavMesh.AllAreas))
        {
            Instantiate(Zombie, hit.position, Quaternion.identity, gameObject.transform);
        }

        gameObject.transform.DetachChildren();
        float timer2 = Random.Range(timer - 2.5f, timer + 2.6f);
        yield return new WaitForSeconds(timer2);
        StartCoroutine(nameof(AutoWave));
    }
}
