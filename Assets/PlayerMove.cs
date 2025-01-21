using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField] GameObject player;

    [SerializeField] private float speed = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (player == null)
            return;
        
        if (Input.GetKey(KeyCode.W))
            player.transform.position += Vector3.up * (speed * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.S))
            player.transform.position -= Vector3.up * (speed * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.A))
            player.transform.position += Vector3.left * (speed * Time.deltaTime);
        
        if (Input.GetKey(KeyCode.D))
            player.transform.position -= Vector3.left * (speed * Time.deltaTime);
    }
}
