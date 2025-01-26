using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _popTime = 0.1f;
    [SerializeField] private bool attractedToOtherBubbles = true;
    [SerializeField] private Player _player;
    [SerializeField] private Animator _animator;

    private bool _isPlayer => _player;
    
    // todo - these are currently only used for the player bubble. may want to bust these out into a derived class or component.
    [SerializeField] private Vector2 _scaleBounds = new Vector2(0.1f, 3f);
    
    [field: SerializeField] public Collider2D Collider { get; private set; }
    
    private List<Bubble> _otherBubbles = new();

    private BubbleBuoyancy _buoyancy;
    private bool _isPopped = false;

    public float NormalizedVolume => (transform.localScale.x - _scaleBounds.x) / (_scaleBounds.y - _scaleBounds.x);

    private void Start()
    {
        if (BubblesManager.Instance.UseDestroyAfterTime)
        {
            StartCoroutine(DestroyAfterTimeCoroutine());
        }
    }

    private IEnumerator DestroyAfterTimeCoroutine()
    {
        yield return new WaitForSeconds(BubblesManager.Instance.DestroyAfterTime);
        yield return StartCoroutine(PopBubbleCoroutine());
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
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (_isPopped)
        {
            return;
        }
        
        _otherBubbles.Remove(other.GetComponent<Bubble>());
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
        if (_isPlayer)
            return;

        StartCoroutine(PopBubbleCoroutine());
    }

    private IEnumerator PopBubbleCoroutine()
    {
        // todo - trigger cute pop animation or something
        _otherBubbles.Clear();
        _animator.SetTrigger("OnPop");
        Collider.enabled = false;
        
        // _animator.Update(0);
        // var clips = _animator.GetCurrentAnimatorClipInfo(0);
        // int delayTime = (int)(clips[0].clip.length * 1000);
        //yield return new WaitForSeconds(delayTime);
        
        yield return new WaitForSeconds(_popTime);
        Destroy(gameObject);
    }

    public void Scale(float scaleDelta)
    {
        float newScale = transform.localScale.x + scaleDelta;
        newScale = Mathf.Clamp(newScale, _scaleBounds.x, _scaleBounds.y);
        gameObject.transform.localScale = new Vector3(newScale, newScale, 1f);
        
        float scaleSign = Mathf.Sign(gameObject.transform.lossyScale.x);

        if (BubblesManager.Instance)
        {
            if (BubblesManager.Instance.UseBuoyancy)
            {
                _buoyancy ??= GetComponent<BubbleBuoyancy>();
                _buoyancy.Initialize();
            }
        }

        _spriteRenderer.gameObject.SetActive(scaleSign > 0);
    }
}
