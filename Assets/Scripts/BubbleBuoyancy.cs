using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class BubbleBuoyancy : MonoBehaviour
{
    [SerializeField] private CircleCollider2D collider;
    
    private Rigidbody2D rb;
    
    [Header("Settings")] 
    [SerializeField] private float bouyancyOffset = 0f;
    [SerializeField] private float maxBuoyancySwarmForce = 5f;

    [Header("Monitoring")] 
    [SerializeField] private int bubbleCount;
    [SerializeField] private float volume;
    [SerializeField] private float baseBuoyantForce;

    void Start()
    {
        if (BubblesManager.Instance && !BubblesManager.Instance.UseBuoyancy)
            return;
        
        Initialize();
    }

    float ScaledPhysicsRadius => BubblesManager.Instance.physicsBaseRadius * transform.localScale.x;
    float DisplayRadius => transform.localScale.x * collider.radius;
    
    public void Initialize()
    {
        if (!rb)
            rb = GetComponent<Rigidbody2D>();
        
        //rb.gravityScale = 0;

        if (ScaledPhysicsRadius > 0)
        {
            // Compute base buoyancy using Archimedes' principle
            volume = (4f / 3f) * Mathf.PI * Mathf.Pow(ScaledPhysicsRadius, 3);
            baseBuoyantForce = (BubblesManager.Instance.fluidDensity - BubblesManager.Instance.gasDensity) * volume * Physics.gravity.magnitude;
        }
        else
        {
            volume = -((4f / 3f) * Mathf.PI * Mathf.Pow(-ScaledPhysicsRadius, 3));
            baseBuoyantForce = (BubblesManager.Instance.fluidDensity - BubblesManager.Instance.gasDensity) * volume * Physics.gravity.magnitude;
        }
    }

    void FixedUpdate()
    {
        if (BubblesManager.Instance && !BubblesManager.Instance.UseBuoyancy)
            return;
        
        ApplyBuoyancy();
        ApplySwarmEffects();
    }

    void ApplyBuoyancy()
    {
        rb.AddForce(Vector3.up * (baseBuoyantForce + bouyancyOffset), ForceMode2D.Force);
    }

    void ApplySwarmEffects()
    {
        Collider2D[] nearbyBubbles = Physics2D.OverlapCircleAll(transform.position, DisplayRadius * 3);
        bubbleCount = 0;

        foreach (Collider2D col in nearbyBubbles)
        {
            if (col.gameObject != gameObject && col.CompareTag("Bubble"))
            {
                bubbleCount++;
                Vector3 toOther = (col.transform.position - transform.position).normalized;
                rb.AddForce(toOther * BubblesManager.Instance.wakeInfluence * baseBuoyantForce, ForceMode2D.Force);
            }
        }
        if (rb.linearVelocity.y < maxBuoyancySwarmForce)
        {
            float densityFactor = Mathf.Pow(1 - BubblesManager.Instance.swarmFactor, -3) * BubblesManager.Instance.riseSpeedFactor;
            rb.AddForce(Vector3.up *  densityFactor, ForceMode2D.Force);
        }
    }

    // private void OnTriggerEnter(Collider other)
    // {
    //     if (other.gameObject.CompareTag("Bubble"))
    //     {
    //         MergeBubble(other.gameObject);
    //     }
    // }
    //
    // void MergeBubble(GameObject otherBubble)
    // {
    //     float newRadius = Mathf.Pow(Mathf.Pow(radius, 3) + Mathf.Pow(otherBubble.GetComponent<BubbleBuoyancy>().radius, 3), 1f / 3f);
    //     radius = newRadius;
    //     transform.localScale = Vector3.one * radius * 2;
    //
    //     baseBuoyantForce = (fluidDensity - gasDensity) * ((4f / 3f) * Mathf.PI * Mathf.Pow(radius, 3)) * Physics.gravity.magnitude;
    //     Destroy(otherBubble);
    // }
}

