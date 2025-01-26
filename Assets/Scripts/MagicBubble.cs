using System;
using System.Collections;
using UnityEngine;

public class MagicBubble : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private float _popDuration;
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (!PhysicsUtility.IsPlayerOrAttachedTo(other.gameObject))
            return;

        PlayerState.Current.Lives++;
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(Pop());
    }

    private IEnumerator Pop()
    {
        _animator.SetTrigger("Pop");
        yield return new WaitForSeconds(_popDuration);
        Destroy(gameObject);
    }
}
