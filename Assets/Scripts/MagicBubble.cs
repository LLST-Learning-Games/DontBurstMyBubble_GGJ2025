using System;
using UnityEngine;

public class MagicBubble : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!PhysicsUtility.IsPlayerOrAttachedTo(other.gameObject))
            return;

        PlayerState.Current.Lives++;
        Destroy(gameObject);
    }
}
