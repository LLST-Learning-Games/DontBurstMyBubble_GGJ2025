
using UnityEngine;

public class BubblePopper
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        var bubble = other.gameObject.GetComponent<Bubble>();
        if (bubble)
        {
            bubble.PopBubble();
        }

    }
}
