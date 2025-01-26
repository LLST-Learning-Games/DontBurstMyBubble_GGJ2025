using System;
using UnityEngine;

public class DeathCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.isTrigger)
            return;
        
        Player player = other.GetComponent<Player>();
        
        if (!player)
        {
            Destroy(other.gameObject);
            return;
        }


        
        player.Kill();
    }
}