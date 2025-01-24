using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class BubbleBuoyancy : MonoBehaviour
{
    [SerializeField] private CircleCollider2D collider;
    
    public float physicsBaseRadius = 0.01f;  // Bubble radius in meters. This is used for Physics calculations, separate from display calculations!
    public float fluidDensity = 1000f;  // Density of water (kg/m³)
    public float gasDensity = 1.225f;  // Density of air (kg/m³)
    public float wakeInfluence = 0.2f; // Multiplier for wake acceleration
    public float swarmFactor = 0.05f;  // Gas fraction in swarm
    public float riseSpeedFactor = 1.5f; // Speed boost from swarm
    
    [Header("Monitoring")]
    
    [SerializeField] int bubbleCount = 0;
    
    private Rigidbody2D rb;
    [SerializeField] private float volume;
    [SerializeField] private float baseBuoyantForce;

    void Start()
    {
        if (BubblesManager.Instance && !BubblesManager.Instance.UseBuoyancy)
            return;
        
        Initialize();
    }


    [SerializeField] float scaledPhysicsRadius;
    [SerializeField] private float displayRadius;
    float ScaledPhysicsRadius
    {
        get
        {
            scaledPhysicsRadius = physicsBaseRadius * transform.localScale.x;
            return scaledPhysicsRadius;
        }
    }

    float DisplayRadius
    {
        get
        {
            displayRadius = transform.localScale.x * collider.radius;
            return displayRadius;
        }
    }
    
    public void Initialize()
    {
        //baseRadius *= gameObject.transform.lossyScale.x;
        if (!rb)
            rb = GetComponent<Rigidbody2D>();
        
        rb.gravityScale = 0;//.useGravity = false;

        if (ScaledPhysicsRadius > 0)
        {
            // Compute base buoyancy using Archimedes' principle
            volume = (4f / 3f) * Mathf.PI * Mathf.Pow(ScaledPhysicsRadius, 3);
            baseBuoyantForce = (fluidDensity - gasDensity) * volume * Physics.gravity.magnitude;
            //Debug.Log(baseBuoyantForce);
        }
        else
        {
            volume = -((4f / 3f) * Mathf.PI * Mathf.Pow(-ScaledPhysicsRadius, 3));
            baseBuoyantForce = (fluidDensity - gasDensity) * volume * Physics.gravity.magnitude;
            //Debug.Log("Does this run?");
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
        rb.AddForce(Vector3.up * baseBuoyantForce, ForceMode2D.Force);
    }

    void ApplySwarmEffects()
    {
        Collider2D[] nearbyBubbles = Physics2D.OverlapCircleAll(transform.position, DisplayRadius * 3); // THIS IS NOT WORKING. radius doesn't correlate to in-game distances. Even when I set it huge, it doesn't find anything.
        bubbleCount = 0;

        foreach (Collider2D col in nearbyBubbles)
        {
            //Debug.Log("This is running");
            if (col.gameObject != gameObject && col.CompareTag("Bubble"))
            {
                bubbleCount++;
                Vector3 toOther = (col.transform.position - transform.position).normalized;
                rb.AddForce(toOther * wakeInfluence * baseBuoyantForce, ForceMode2D.Force);
                //Debug.Log(bubbleCount);
            }
        }

        // Adjust rise speed based on swarm density
        float densityFactor = Mathf.Pow(1 - swarmFactor, -3);
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, Mathf.Clamp(rb.linearVelocity.y * densityFactor * riseSpeedFactor, 0, 5f));//, rb.linearVelocity.z);
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

