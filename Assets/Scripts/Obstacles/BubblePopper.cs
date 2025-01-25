
using UnityEngine;

public class BubblePopper : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D other)
    {
        var bubble = other.gameObject.GetComponent<Bubble>();
        if (bubble)
        {
            bubble.PopBubble();
        } 

    }
}