using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private bool _isPlayer = false;
    [SerializeField] private float _popTime = 0.1f;
    [SerializeField] private bool attractedToOtherBubbles = true;
    
    [field: SerializeField] public Collider2D Collider { get; private set; }
    
    private List<Bubble> _otherBubbles = new();
    
    private bool _isPopped = false;
    
    private void Start()
    {
        if (!_isPlayer && BubblesManager.Instance && BubblesManager.Instance.UseDestroyAfterTime)
        {
            StartCoroutine(DestroyAfterSeconds());
        }

        if (BubblesManager.Instance && BubblesManager.Instance.UseGravityScaling)
            _rigidbody.gravityScale = CalculateGravityScale(gameObject.transform.lossyScale.magnitude);
    }

    // Temporary cleanup operation while we play with spawning
    private IEnumerator DestroyAfterSeconds()
    {
        yield return new WaitForSeconds(BubblesManager.Instance.DestroyAfterTime);
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isPopped || other.gameObject.layer != LayerMask.NameToLayer("Bubble"))
        {
            return;
        }

        var otherBubble = other.GetComponent<Bubble>();

        if (otherBubble)
        {
            _otherBubbles.Add(otherBubble);
            if (_rigidbody)
            {
                if (BubblesManager.Instance && BubblesManager.Instance.UseGravityScaling)
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
        if (_isPopped)
        {
            return;
        }
        
        _otherBubbles.Remove(other.GetComponent<Bubble>());
        if(_rigidbody)
        {
            if (BubblesManager.Instance && BubblesManager.Instance.UseGravityScaling)
                _rigidbody.gravityScale -= GetGravityDelta();
        }
    }

    private void FixedUpdate()
    {
        if (_otherBubbles.Count == 0 || !attractedToOtherBubbles)
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
                _rigidbody.AddForce(direction * Math.Clamp(_stickyStrength * sizeMultiplier * distanceMultiplier, 0, 1));
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

    public void PopBondedBubbles()
    {
        if(_otherBubbles.Count == 0)
        {
            return;
        }
        
        foreach (var bubble in _otherBubbles)
        {
            bubble.PopBubble();
        }
    }

    public void PopBubble()
    {
        StartCoroutine(PopBubbleCoroutine());
    }

    private IEnumerator PopBubbleCoroutine()
    {
        // todo - trigger cute pop animation or something
        _otherBubbles.Clear();
        float time = 0f;
        Color originalColor = _spriteRenderer.color;
        while (time < _popTime)
        {
            time += Time.deltaTime;
            _spriteRenderer.color = Color.Lerp(originalColor, Color.clear, time / _popTime);
            yield return null;
        }
        Destroy(gameObject);
    }

    public void Scale(float scaleDelta)
    {
        gameObject.transform.localScale += new Vector3(scaleDelta, scaleDelta);
        
        float scaleSign = Mathf.Sign(gameObject.transform.lossyScale.x);
        float preservedScaleMagnitude = gameObject.transform.lossyScale.magnitude * scaleSign;

        if (BubblesManager.Instance)
        {
            if (BubblesManager.Instance.UseGravityScaling)
                _rigidbody.gravityScale = CalculateGravityScale(preservedScaleMagnitude);

            if (BubblesManager.Instance.UseBuoyancy)
                GetComponent<BubbleBuoyancy>().Initialize();
        }

        _spriteRenderer.gameObject.SetActive(scaleSign > 0);
    }

    private float CalculateGravityScale(float sizeScale)
    {
        return GetGravityDelta() * DiameterToVolume(sizeScale) + GetGravityEffectOfOtherBubbles();
    }
        
    
    float DiameterToVolume(float diameter)
    {
        return (float)(Math.PI * Math.Pow(diameter, 3) / 6);
    }

    float GetGravityEffectOfOtherBubbles()
    {
        return GetGravityDelta() * _otherBubbles.Count;
    }
}
