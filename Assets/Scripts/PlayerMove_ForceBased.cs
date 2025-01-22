using UnityEngine;

public class PlayerMove_ForceBased : MonoBehaviour
{
    [SerializeField] Rigidbody2D _playerRb;

    [SerializeField] private float _movementImpuseStrength = 1;


    // Update is called once per frame
    void Update()
    {
        if (!_playerRb)
            return;

        Vector3 force = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
            force += Vector3.up;
        
        if (Input.GetKey(KeyCode.S))
            force -= Vector3.up;
        
        if (Input.GetKey(KeyCode.A))
            force += Vector3.left;
        
        if (Input.GetKey(KeyCode.D))
            force -= Vector3.left;
        
        force.Normalize();
        force *= _movementImpuseStrength * Time.deltaTime;
        
        _playerRb.AddForce(force);
    }
}
