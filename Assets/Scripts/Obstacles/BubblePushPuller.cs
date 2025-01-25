using UnityEngine;

public class BubblePushPuller : MonoBehaviour
{
    [SerializeField] private Transform _pushSource;
    [SerializeField] private float _pushMagnitude;
    
    public void OnTriggerStay2D(Collider2D other)
    {
        if (other.isTrigger)
        {
            return;
        }
        var otherRb = other.gameObject.GetComponent<Rigidbody2D>();
        if (!otherRb)
        {
            return;
        }
        
        var pushVector = other.transform.position - _pushSource.position;

        pushVector *= _pushMagnitude;
        otherRb.AddForce(pushVector, ForceMode2D.Impulse);
    }
}
