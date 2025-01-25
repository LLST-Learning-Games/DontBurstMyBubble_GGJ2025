
using System;
using UnityEngine;

public class BubblePopper : MonoBehaviour
{
    public void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.GetComponent<Player>() is { } player)
        {
            player.TryDealDamage();
            return;
        }

        if (other.gameObject.GetComponent<Bubble>() is { } bubble)
        {
            bubble.PopBubble();
            return;
        }
    }

    private void OnCollisionStay(Collision other)
    {
        if (other.gameObject.GetComponent<Player>() is { } player)
        {
            player.TryDealDamage();
            return;
        }
    }
}