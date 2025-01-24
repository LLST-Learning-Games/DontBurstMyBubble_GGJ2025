using System;
using UnityEngine;
using UnityEngine.Serialization;

public class BubblesManager : SingletonBase<BubblesManager>
{
    enum BuoyancySimulationMethod
    {
        Realistic,
        GravityScaling
    }
    
    [SerializeField] BuoyancySimulationMethod buoyancySimulationMethod = BuoyancySimulationMethod.Realistic;
    BuoyancySimulationMethod cachedMethod = BuoyancySimulationMethod.Realistic;

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (running && buoyancySimulationMethod != cachedMethod) // we've just changed something while in Play Mode
        {
            if (buoyancySimulationMethod == BuoyancySimulationMethod.Realistic)
            {
                var allBubbles = FindObjectsByType<BubbleBuoyancy>(FindObjectsSortMode.None);

                foreach (var bubble in allBubbles)
                    bubble.Initialize();
                
                Debug.Log("Initialized realistic buoyancy simulation");
            }
            
            cachedMethod = buoyancySimulationMethod;
        }
#endif
    }

    private bool running;
    private void Start()
    {
        running = true;
    }

    [field: SerializeField] public float StickyStrength { get; private set; } = 1;
    [field: SerializeField] public bool UseSizeInCalculations { get; private set; } = true;
    [Tooltip("How much size of other bubble's colliders affect this bubble's stickiness to them.")]
    [field: SerializeField] public float SizeFactor { get; private set; } = 1;
    [field: SerializeField] public bool UseDistanceInCalculations { get; private set; } = true;
    [field: SerializeField] public float DistanceFactor { get; private set; } = 1;

    public bool UseGravityScaling
    {
        get { return buoyancySimulationMethod == BuoyancySimulationMethod.GravityScaling; }
    }

    [field: SerializeField] public float ClusterGravityDelta { get; private set; } = -0.05f;
    public bool UseBuoyancy
    {
        get { return buoyancySimulationMethod == BuoyancySimulationMethod.Realistic; }
    }
    [field: SerializeField] public float DestroyAfterTime { get; private set; } = 1f;
    [field: SerializeField] public bool UseDestroyAfterTime { get; private set; } = true;
}
