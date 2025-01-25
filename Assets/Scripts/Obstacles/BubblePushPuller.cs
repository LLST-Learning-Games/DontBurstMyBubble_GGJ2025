using System;
using UnityEngine;

public class BubblePushPuller : MonoBehaviour
{
    [SerializeField] private float _pushMagnitude;
    [SerializeField] private float _targetSpeed;
    
    private bool _isPushing;

    // public void OnTriggerEnter2D(Collider2D other)
    // {
    //     if (other.isTrigger)
    //     {
    //         return;
    //     }
    //     _isPushing = true;
    //     var otherRb = other.gameObject.GetComponent<Rigidbody2D>();
    //     if (!otherRb)
    //     {
    //         return;
    //     }
    //     otherRb.
    // }

    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            return;
        }
        _isPushing = true;
        var otherRb = other.gameObject.GetComponent<Rigidbody2D>();
        if (!otherRb)
        {
            return;
        }
        
        Vector2 targetVelocity = transform.rotation * Vector2.up * _targetSpeed;
        Vector2 force = (targetVelocity - otherRb.linearVelocity) * _pushMagnitude;
        otherRb.AddForce(force);
    }
        
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            _isPushing = true;
        }
        
    }
}
