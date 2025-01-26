using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BuoyancyDisabler : MonoBehaviour
{
    // public UnityEvent onBubbleAdded = new UnityEvent();
    // public UnityEvent onBubbleRemoved = new UnityEvent();
    
    [SerializeField] BubbleController bubbleController;

    //[SerializeField] private float effectRadius = 3;
    [Tooltip("Amount to increase player's move force by per bubble")]
    [SerializeField] private float moveForceModifier = 30;
    
    [Header("Monitoring")]
    [SerializeField] private int numBubbles;

    //[SerializeField] private List<BubbleBuoyancy> otherBubbleBuoyancies = new ();
    // private void FixedUpdate()
    // {
    //     UndoLastFrame();
    //     DoThisFrame();
    // }
    //
    // void UndoLastFrame()
    // {
    //     foreach (BubbleBuoyancy bubble in otherBubbleBuoyancies)
    //         bubble.enabled = true;
    //     
    //     bubbleController.ChangeMoveSpeed(-moveForceModifier * otherBubbleBuoyancies.Count);
    //
    //     otherBubbleBuoyancies.Clear();
    // }
    //
    // void DoThisFrame()
    // {
    //     Collider2D[] nearbyBubbles = Physics2D.OverlapCircleAll(transform.position, effectRadius);
    //
    //     foreach (Collider2D nearbyBubble in nearbyBubbles)
    //     {
    //         if (nearbyBubble.isTrigger || nearbyBubble.gameObject == gameObject)
    //             break;
    //         
    //         var otherBuoyancy = nearbyBubble.GetComponent<BubbleBuoyancy>();
    //
    //         if (otherBuoyancy)
    //         {
    //             otherBuoyancy.enabled = false;
    //             otherBubbleBuoyancies.Add(otherBuoyancy);
    //         }
    //     }
    //     
    //     bubbleController.ChangeMoveSpeed(moveForceModifier * otherBubbleBuoyancies.Count);
    // }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherBuoyancy = other.GetComponent<BubbleBuoyancy>();
    
        if (otherBuoyancy)
        {
            otherBuoyancy.enabled = false;
            numBubbles++;
            //onBubbleAdded.Invoke();
            bubbleController.ChangeMoveSpeed(moveForceModifier);
            otherBuoyancy.GetComponent<Bubble>().StopAllCoroutines();//Coroutine("DestroyAfterTimeCoroutine");
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
