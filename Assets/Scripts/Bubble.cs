using System;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _stickyStrength;

    private List<GameObject> _otherBubbles = new List<GameObject>();

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Bubble"))
        {
            return;
        }

        _otherBubbles.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _otherBubbles.Remove(other.gameObject);
    }

    private void FixedUpdate()
    {
        if (_otherBubbles.Count == 0)
        {
            return;
        }
        
        foreach(var bubble in _otherBubbles)
        {
            Vector3 direction = (bubble.transform.position - transform.position).normalized;
            _rigidbody.AddForce(direction * _stickyStrength);
        }
    }
}
