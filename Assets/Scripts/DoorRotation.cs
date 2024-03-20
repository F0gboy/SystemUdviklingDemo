using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorRotation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.Rotate(0.0f, 0.0f, Random.Range(-9f ,9.1f), Space.Self);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
