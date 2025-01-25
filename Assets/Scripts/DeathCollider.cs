using System;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();
        
        if (!player)
        {
            Destroy(other.gameObject);
            return;
        }

        if (other.isTrigger)
            return;
        
        player.Kill();
    }
}