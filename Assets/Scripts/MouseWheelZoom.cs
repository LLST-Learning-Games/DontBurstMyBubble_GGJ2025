using UnityEngine;

public class MouseWheelZoom : MonoBehaviour
{
    [SerializeField] private float scrollMultiplier = 0.1f;
    [SerializeField] private Camera mainCamera;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!mainCamera)
            mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        DoMouseWheel();
    }
    
    void DoMouseWheel()
    {
        var mouseWheelInput = Input.GetAxis("Mouse ScrollWheel");

        if (mouseWheelInput == 0)
            return;

        if (mouseWheelInput > 0)//Input.GetAxis(input.IsNewMouseScrollUp())                
            MultiplyZoom(1 - scrollMultiplier);
        else if (mouseWheelInput < 0)//(input.IsNewMouseScrollDown())
            MultiplyZoom(1 + scrollMultiplier);
        
        void MultiplyZoom(float multiplier)
        {
            if (mainCamera.orthographic)
                mainCamera.orthographicSize *= multiplier;
        }
    }
}
