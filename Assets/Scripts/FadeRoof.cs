using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FadeRoof : MonoBehaviour
{
    private GameObject Player;
    private MeshRenderer Roof;
    private Collider RoofCollider; // Add a reference to the roof's collider

    private void Start()
    {
        Player = GameObject.Find("Player");
        Roof = GetComponent<MeshRenderer>();
        RoofCollider = GetComponent<Collider>(); // Initialize the roof collider
    }

    void Update()
    {
        // Use ClosestPoint to find the nearest point on the roof's collider to the player
        Vector3 closestPoint = RoofCollider.ClosestPoint(Player.transform.position);
        float distance = Vector3.Distance(Player.transform.position, closestPoint);
        
        // Define the minimum and maximum distances
        float minDistance = 2f;
        float maxDistance = 3f;

        // Normalize the distance to a 0-1 range
        float normalizedDistance = Mathf.Clamp01((distance - minDistance) / (maxDistance - minDistance));
        float alpha = normalizedDistance;

        // Update the roof's material color with the new alpha
        Roof.material.color = new Color(Roof.material.color.r, Roof.material.color.g, Roof.material.color.b, alpha);
    }
}