using UnityEngine;

public class PlayerFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = _target.position;
    }
}
