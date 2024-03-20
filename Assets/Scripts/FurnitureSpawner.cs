using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FurnitureSpawner : MonoBehaviour
{
    [SerializeField] GameObject[] list;
    // Start is called before the first frame update
    void Start()
    {
        int number = Random.Range(0, list.Length);
        Debug.Log(number);
        Instantiate(list[number], gameObject.transform.position, gameObject.transform.rotation);
        
        Destroy(gameObject);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
