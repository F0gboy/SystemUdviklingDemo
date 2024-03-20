using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PickupText : MonoBehaviour
{
    [SerializeField]
    private bool playerIsInRange;

    [SerializeField]
    private TextMeshProUGUI pickupText;

    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject canvas;

    [Range(0.1f, 1)]
    [SerializeField]
    private float textLerpOffset;

    [Range(0.1f, 1)]
    [SerializeField]
    private float textMoveSpeed;

    private Color startColor;

    private Vector3 startPos;

    private void Start()
    {
        pickupText = canvas.gameObject.GetComponentInChildren<TextMeshProUGUI>();
        startColor = new Color(pickupText.color.r, pickupText.color.g, pickupText.color.b, 1);

        startPos = canvas.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        playerIsInRange = true;
        if (collision.gameObject.CompareTag("Player"))
        {
            player = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        playerIsInRange = false;
    }

    private void Update()
    {
        

        if (playerIsInRange == true)
        {
            pickupText.color = Color.Lerp(pickupText.color, startColor, 0.2f);

            Vector3 lerpPos = Vector3.Lerp(startPos, player.transform.position, textLerpOffset);

            canvas.transform.position = Vector3.Lerp(canvas.transform.position, lerpPos, textMoveSpeed);
        }
        else
        {
            pickupText.color = Color.Lerp(pickupText.color, new Color(pickupText.color.r, pickupText.color.g, pickupText.color.b, 0), 0.2f);

            if (pickupText.color.a <= 0.01f) ;
            {
                canvas.transform.position = startPos;
                pickupText.color = new Color(pickupText.color.r, pickupText.color.g, pickupText.color.b, 0);
            }
            
        }
    }
}
