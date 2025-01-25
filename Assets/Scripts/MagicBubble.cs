using System;
using UnityEngine;

public class MagicBubble : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!other.gameObject.GetComponent<Player>())
            return;

        PlayerState.Current.Lives++;
        Destroy(gameObject);
        //TODO: visibly parent the bubble to the player
    }
}
