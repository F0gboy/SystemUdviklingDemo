using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class TextFade : MonoBehaviour
{
    public GameObject Player;
    
    private void Start()
    {
        Player = GameObject.Find("Player");
    }


    void Update()
    {
        float fadeOut = Vector3.Distance(Player.transform.position, transform.position);
        
        if (fadeOut < 10F) 
        {
            if (fadeOut < 4F) 
            {
                this.gameObject.GetComponent<TextMeshPro>().alpha = 1.0F;
            }
            else
            {
                this.gameObject.GetComponent<TextMeshPro>().alpha = 1-(fadeOut / 10F); 
            }
        }
        else
        {
            this.gameObject.GetComponent<TextMeshPro>().alpha = 0.0F; 
        }
    }
}