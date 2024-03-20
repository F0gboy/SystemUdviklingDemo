using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeRoof : MonoBehaviour
{
    private GameObject Player;
    private MeshRenderer Roof;

    private void Start()
    {
        Player = GameObject.Find("Player");
        Roof = GetComponent<MeshRenderer>();
    }

    void Update()
    {
        float distance = Vector3.Distance(Player.transform.position, transform.position);
        
        // Define the minimum and maximum distances
        float minDistance = 6f;
        float maxDistance = 8f;

        // Normalize the distance to a 0-1 range where 0 is at minDistance and 1 is at maxDistance
        float normalizedDistance = Mathf.Clamp01((distance - minDistance) / (maxDistance - minDistance));

        // Directly use the normalized distance to calculate the alpha
        float alpha = normalizedDistance;

        // Update the roof's material color with the new alpha
        Roof.material.color = new Color(Roof.material.color.r, Roof.material.color.g, Roof.material.color.b, alpha);
    }
}