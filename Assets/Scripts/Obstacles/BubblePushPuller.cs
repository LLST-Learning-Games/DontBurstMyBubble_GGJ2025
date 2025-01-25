using System;
using UnityEngine;

public class BubblePushPuller : MonoBehaviour
{
    [SerializeField] private Transform _pushSource;
    [SerializeField] private Transform _pushDirection;
    [SerializeField] private float _pushMagnitude;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            return;
        }
        
    }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            return;
        }
        var otherRb = other.gameObject.GetComponent<Rigidbody2D>();
        if (!otherRb)
        {
            return;
        }
        
        var pushVector = _pushDirection.localPosition - _pushSource.localPosition;

        pushVector *= _pushMagnitude;
        otherRb.AddForce(pushVector, ForceMode2D.Force);
        
        // Vector3 targetVelocity = transform.rotation * Vector3.forward * targetSpeed;
        // Vector3 force = (targetVelocity - rb.velocity) * forceMult;
        // rb.AddForce(force);
    }
}
