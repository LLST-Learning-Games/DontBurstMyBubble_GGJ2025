using System;
using UnityEngine;

public class InverseScaler : MonoBehaviour
{
   

    [Header("Monitoring")]
    [SerializeField] private float fixedScale;
    [SerializeField] private float inverseScale;

    private void Awake()
    {
        fixedScale = transform.localScale.x; 
    }

    public void Scale(float newScale)
    {
        inverseScale = fixedScale / newScale;
        transform.localScale = new Vector3(inverseScale, inverseScale, inverseScale);
    }
    
}
