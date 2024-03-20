using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteChange : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;

    public Sprite newSprite;
    public Sprite oldSprite;

    bool toggle = true;

    void Start()
    {
        int coloraddition = Random.Range(-20, 51);
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(185 + coloraddition, 174 + coloraddition, 174 + coloraddition);
    }

    // Update is called once per frame
    void Update()
    {
        float Distance = Vector3.Distance(GameObject.Find("Player").transform.position, transform.position);

        if (Input.GetKeyDown(KeyCode.E) && Distance < 2F)
        {
            toggle = !toggle;
        }
        if (toggle == true)
        {
            spriteRenderer.sprite = oldSprite;

        }
        else
        {
            spriteRenderer.sprite = newSprite;
        }

    }

}
