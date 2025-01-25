using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int ScoreBonus = 100;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.GetComponent<Player>())
            return;

        PlayerState.Current.Score += ScoreBonus;
        Destroy(gameObject);
    }
}
