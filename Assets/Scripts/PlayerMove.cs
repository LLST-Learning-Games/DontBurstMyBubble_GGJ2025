using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    enum ControlSet
    {
        WASD,
        ArrowKeys
    }
    
    [SerializeField] ControlSet controlSet;
    private KeyCode[] controls;

    [Header("Specify in this order: Up, Left, Down, Right")]
    [SerializeField] private KeyCode[] controlsWasd = { KeyCode.W, KeyCode.A, KeyCode.S, KeyCode.D };
    [SerializeField] private KeyCode[] controlsArrowKeys = { KeyCode.UpArrow, KeyCode.LeftArrow, KeyCode.DownArrow, KeyCode.RightArrow};
    
    [SerializeField] GameObject player;

    [SerializeField] private float speed = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (controlSet == ControlSet.WASD)
            controls = controlsWasd;
        else if (controlSet == ControlSet.ArrowKeys)
            controls = controlsArrowKeys;
    }

    // Update is called once per frame
    void Update()
    {
        if (!player)
            return;
        
        if (Input.GetKey(controls[0]))//KeyCode.W))
            player.transform.position += Vector3.up * (speed * Time.deltaTime);
        
        if (Input.GetKey(controls[1]))
            player.transform.position += Vector3.left * (speed * Time.deltaTime);
        
        if (Input.GetKey(controls[2]))
            player.transform.position -= Vector3.up * (speed * Time.deltaTime);
        
        if (Input.GetKey(controls[3]))
            player.transform.position -= Vector3.left * (speed * Time.deltaTime);
    }
    
    
}
