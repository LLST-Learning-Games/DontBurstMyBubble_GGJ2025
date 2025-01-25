using System;
using UnityEngine;
using UnityEngine.Events;

public class BuoyancyDisabler : MonoBehaviour
{
    // public UnityEvent onBubbleAdded = new UnityEvent();
    // public UnityEvent onBubbleRemoved = new UnityEvent();
    
    [SerializeField] BubbleController bubbleController;

    [Tooltip("Amount to increase player's move force by per bubble")]
    [SerializeField] private float moveForceModifier = 30;
    
    [Header("Monitoring")]
    [SerializeField] private int numBubbles;
    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherBuoyancy = other.GetComponent<BubbleBuoyancy>();

        if (otherBuoyancy)
        {
            otherBuoyancy.enabled = false;
            numBubbles++;
            //onBubbleAdded.Invoke();
            bubbleController.ChangeMoveSpeed(moveForceModifier);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var otherBuoyancy = other.GetComponent<BubbleBuoyancy>();

        if (otherBuoyancy)
        {
            otherBuoyancy.enabled = true;
            numBubbles--;
            //onBubbleRemoved.Invoke();
            bubbleController.ChangeMoveSpeed(-moveForceModifier);
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
