using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private bool _isPlayer = false;
    
    [field: SerializeField] public Collider2D Collider { get; private set; }
   

    private List<Bubble> _otherBubbles = new();

    private void Start()
    {
        if (!_isPlayer && BubblesManager.Instance && BubblesManager.Instance.UseDestroyAfterTime)
        {
            StartCoroutine(DestroyAfterSeconds());
        }
    }

    // Temporary cleanup operation while we play with spawning
    private IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(BubblesManager.Instance.DestroyAfterTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer != LayerMask.NameToLayer("Bubble"))
        {
            return;
        }

        var otherBubble = other.GetComponent<Bubble>();

        if (otherBubble)
        {
            _otherBubbles.Add(otherBubble);
            if (_rigidbody)
            {
                _rigidbody.gravityScale += GetGravityDelta();
            }
        }
    }
    
    private float GetGravityDelta()
    {
        if (BubblesManager.Instance)
            return BubblesManager.Instance.ClusterGravityDelta;
        else
            return -0.05f;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _otherBubbles.Remove(other.GetComponent<Bubble>());
        if(_rigidbody)
        {
            _rigidbody.gravityScale -= GetGravityDelta();
        }
    }

    private void FixedUpdate()
    {
        if (_otherBubbles.Count == 0)
        {
            return;
        }
        
        foreach(var bubble in _otherBubbles)
        {
            var _stickyStrength = GetStickyStrength();
            Vector3 direction = (bubble.transform.position - transform.position).normalized;
            float sizeMultiplier = GetSizeMultiplier(bubble);
            float distanceMultiplier = GetDistanceMultiplier(bubble);
            
            if (_rigidbody)
                _rigidbody.AddForce(direction * (_stickyStrength * sizeMultiplier * distanceMultiplier));
        }

        float GetStickyStrength()
        {
            if (BubblesManager.Instance)
                return BubblesManager.Instance.StickyStrength;
            else
                return 1;
        }
        
        float GetSizeMultiplier(Bubble bubble)
        {
            if (BubblesManager.Instance && BubblesManager.Instance.UseSizeInCalculations)    
                return bubble.Collider.bounds.size.magnitude * BubblesManager.Instance.SizeFactor;//GetNonTriggerCollider(bubble).bounds.size.magnitude;
            else
                return 1;
        }

        float GetDistanceMultiplier(Bubble bubble)
        {
            if (BubblesManager.Instance && BubblesManager.Instance.UseDistanceInCalculations)
                return BubblesManager.Instance.DistanceFactor / Vector2.Distance(bubble.transform.position, transform.position);
            else
                return 1;
        }
    }
}
