using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemToSpawn
{
    public GameObject item;
    public float spawnRate;
    [HideInInspector] public float minSpawnProb, maxSpawnProb;
}

public class LootSystem : MonoBehaviour
{
    public ItemToSpawn[] itemToSpawn;

    void Start()
    {

        for (int i = 0; i < itemToSpawn.Length; i++)
        {

            if (i == 0)
            {
                itemToSpawn[i].minSpawnProb = 0;
                itemToSpawn[i].maxSpawnProb = itemToSpawn[i].spawnRate - 1;
            }
            else
            {
                itemToSpawn[i].minSpawnProb = itemToSpawn[i - 1].maxSpawnProb + 1;
                itemToSpawn[i].maxSpawnProb = itemToSpawn[i].minSpawnProb + itemToSpawn[i].spawnRate - 1;
            }
        }
    }

    void Update()
    {
        float Distance = Vector3.Distance(GameObject.Find("Player").transform.position, transform.position);
        if (Input.GetKeyDown(KeyCode.E) && Distance < 3f)
        {
            Spawnner();
        }
    }

    void Spawnner()
    {
        float randomNum = Random.Range(0, 100);

        for (int i = 0; i < itemToSpawn.Length; i++)
        {
            if (randomNum >= itemToSpawn[i].minSpawnProb && randomNum <= itemToSpawn[i].maxSpawnProb)
            {
                Instantiate(itemToSpawn[i].item, transform.position, Quaternion.Euler(new Vector3(0, 0, 90)) );
                Destroy(this);
                break;
            }
        }
    }

}