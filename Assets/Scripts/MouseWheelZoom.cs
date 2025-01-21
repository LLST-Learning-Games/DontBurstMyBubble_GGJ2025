using UnityEngine;

public class MouseWheelZoom : MonoBehaviour
{
    [SerializeField] private float scrollMultiplier = 0.1f;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private bool zoomInTowardsCursor = true;
    [SerializeField] private bool zoomOutTowardsCursor = true;
    
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

        Vector3 mouseWorldBeforeZoom = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        if (mouseWheelInput > 0) //Input.GetAxis(input.IsNewMouseScrollUp())
        {
            MultiplyZoom(1 - scrollMultiplier);
            
            if (zoomInTowardsCursor)
                ShiftCameraTowardsCursor(false);
        }
        else if (mouseWheelInput < 0) //(input.IsNewMouseScrollDown())
        {
            MultiplyZoom(1 + scrollMultiplier);
            
            if (zoomOutTowardsCursor)
                ShiftCameraTowardsCursor(true);
        }
        
        void MultiplyZoom(float multiplier)
        {
            if (mainCamera.orthographic)
                mainCamera.orthographicSize *= multiplier;
        }

        void ShiftCameraTowardsCursor(bool invert = false)
        {
            Vector3 mouseWorldAfterZoom = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var shift = mouseWorldBeforeZoom - mouseWorldAfterZoom;
            
            if (invert)
                shift = -shift;
            
            // Adjust camera position to maintain focus on mouse cursor
            mainCamera.transform.position += shift;
        }
    }
}
