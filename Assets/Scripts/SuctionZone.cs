using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class SuctionZone : MonoBehaviour
{
    public float suctionForce = 10f; // Strength of the suction
    public float minDistance = 0.5f; // Stop suction when close enough
    public bool useAcceleration = false; // Whether force increases over time

    public List<GameObject> bubbles;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger || !other.CompareTag("Bubble")) // we don't want to run on trigger zones.
            return;

        if (other.GetComponent<Player>() != null)
            return;
        
        bubbles.Add(other.gameObject);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.isTrigger || !other.CompareTag("Bubble")) // we don't want to run on trigger zones.
            return;

        if (other.GetComponent<Player>() != null)
            return;
        
        bubbles.Remove(other.gameObject);
    }
        
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.isTrigger || !other.CompareTag("Bubble")) // we don't want to run on trigger zones.
            return;

        if (other.GetComponent<Player>() != null)
            return;
        //
        // if (PhysicsUtility.IsPlayerOrAttachedTo(other.gameObject))
        //     return;
        
        Rigidbody2D rb = other.GetComponent<Rigidbody2D>();
        if (rb == null) return; // Only affect objects with Rigidbody2D

        Vector2 direction = (transform.position - other.transform.position).normalized;
        float distance = Vector2.Distance(transform.position, other.transform.position);

        if (distance > minDistance) // Only pull if not too close
        {
            float force = useAcceleration ? suctionForce / distance : suctionForce;
            rb.AddForce(direction * force);
        }
    }
}
