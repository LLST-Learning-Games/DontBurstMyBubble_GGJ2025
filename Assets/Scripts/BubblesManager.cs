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

    [Header("Buoyancy Settings")]
    public float physicsBaseRadius = 0.01f;
    public float fluidDensity = 1000f;
    public float gasDensity = 1.225f;
    public float wakeInfluence = 0.2f;
    public float swarmFactor = 0.05f;
    public float riseSpeedFactor = 1.5f;

    [Header("Other Settings")] 
    [Tooltip("Unity doesn't display the header if the next field is a backing field so here's a stupid workaround.")]
    [SerializeField] private bool dummyHeaderSpacer;
    [field: SerializeField] public float StickyStrength { get; private set; } = 1;
    [field: SerializeField] public bool UseSizeInCalculations { get; private set; } = true;
    [Tooltip("How much size of other bubble's colliders affect this bubble's stickiness to them.")]
    [field: SerializeField] public float SizeFactor { get; private set; } = 1;
    [field: SerializeField] public bool UseDistanceInCalculations { get; private set; } = true;
    [field: SerializeField] public float DistanceFactor { get; private set; } = 1;

    
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
