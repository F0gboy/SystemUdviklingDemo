using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BloodCircleScript : MonoBehaviour
{
    public bool alive = true;
    float size;
    Vector3 big;
    GameObject blood;
    bool expanding = false;
    // Start is called before the first frame update
    void Start()
    {
        size = Random.Range(1.1f, 1.6f);
        blood = gameObject;
        big = new Vector3(size, size, blood.transform.localScale.z);

            }

    // Update is called once per frame
    void Update()
    {   
        //When Zombie dies it changes blood position and size and starts expanding
        if (alive == false && expanding == false) 
        {
            blood.transform.localScale = new Vector3(0.75f, 0.75f, blood.transform.localScale.z);
            blood.transform.localPosition = new Vector3(Random.Range(-0.1f, 0.1f), Random.Range(-0.1f, 0.1f), 0);
            StartCoroutine(nameof(BloodExpand));
            expanding = true;
        }
    }
    //Blood expands
    public IEnumerator BloodExpand()
    {
        if (blood.transform.localScale.x < big.x  || blood.transform.localScale.y < big.y)
        {
        
            blood.transform.localScale = new Vector3(blood.transform.localScale.x + 0.01f, blood.transform.localScale.y + 0.01f, blood.transform.localScale.z);
            yield return new WaitForSeconds(0.5f);
            StartCoroutine(nameof(BloodExpand));
        }
        
        
    }
}
