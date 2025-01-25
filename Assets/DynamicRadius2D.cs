using System;
using UnityEngine;

public class DynamicRadius2D : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    
    public float speedMultiplier = 1.5f; // Adjust scale factor
    public float minRadius = 1f; // Minimum radius
    public float maxRadius = 20f; // Maximum radius

    [Header("Monitoring")] [SerializeField]
    private float originalRadius;
    private float currentRadius;
    private CircleCollider2D circleCollider;

    void Start()
    {
        circleCollider = GetComponent<CircleCollider2D>();
        originalRadius = circleCollider.radius;
    }

    private void FixedUpdate()
    {
        var magnitude =rigidBody.linearVelocity.magnitude;
        UpdateRadius(magnitude);
    }

    public void UpdateRadius(float velocityMagnitude)
    {
        currentRadius = Mathf.Clamp(velocityMagnitude * speedMultiplier * originalRadius, minRadius, maxRadius);

        if (circleCollider)
        {
            circleCollider.radius = currentRadius;
        }
    }
}