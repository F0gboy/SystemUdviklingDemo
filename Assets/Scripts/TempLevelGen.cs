using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempLevelGen : MonoBehaviour
{
    public int width, height;
    public Sprite[] sprites;
    public GameObject tilePrefab;

    private void Start()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                GameObject newObj = Instantiate(tilePrefab, new Vector2(x - width / 2, y - height / 2), Quaternion.identity);
                newObj.GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0, sprites.Length)];
            }
        }
    }
}
