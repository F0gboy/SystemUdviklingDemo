using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeRoof : MonoBehaviour
{
    private GameObject Player;
    private SpriteRenderer Roof;
    
    private void Start()
    {
        Player = GameObject.Find("Player");
        Roof = GetComponent<SpriteRenderer>();
    }
    
    void Update()
    {
        float fadeOut = Vector3.Distance(Player.transform.position, transform.position);


        if (fadeOut < 14.5F)
        {
            Roof.color = Color.Lerp(new Color(0, 0, 0, 0), new Color(1, 1, 1, 1), (((fadeOut-4.5f) / 10)));
        }

        //    if (fadeOut < 8F)
        //    {
        //         Roof.color = new Color(Roof.color.r, Roof.color.g, Roof.color.b, 0f); 
        //    }
        //    else
        //    {
        //        Roof.color = new Color(Roof.color.r, Roof.color.g, Roof.color.b, 0 +(fadeOut / 10f)); 
        //    }
        //}
        //else
        //{
        //    Roof.color = new Color(Roof.color.r, Roof.color.g, Roof.color.b, 1f); 
        //}
    }
}