using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeFadeScript : MonoBehaviour
{
    [SerializeField]
    private Color fadeColor;
    [SerializeField]
    private float lerpTime;
    [SerializeField]
    private SpriteRenderer rend;
    private Color originalColor = new Color(1, 1, 1, 1);

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            StopAllCoroutines();
            StartCoroutine(Fade(fadeColor));
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 7)
        {
            StopAllCoroutines();
            StartCoroutine(Fade(originalColor));
        }
    }

    IEnumerator Fade(Color fadeColor)
    {
        while (rend.color != fadeColor)
        {
            rend.color = Color.Lerp(rend.color, fadeColor, lerpTime * Time.deltaTime);
            yield return new WaitForSeconds(0);
        }
    }
}
