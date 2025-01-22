using UnityEngine;

public class BubblesManager : SingletonBase<BubblesManager>
{
    [field: SerializeField] public float StickyStrength { get; private set; } = 1;
    [field: SerializeField] public bool UseSizeInCalculations { get; private set; } = true;
    [Tooltip("How much size of other bubble's colliders affect this bubble's stickiness to them.")]
    [field: SerializeField] public float SizeFactor { get; private set; } = 1;
    [field: SerializeField] public bool UseDistanceInCalculations { get; private set; } = true;
    [field: SerializeField] public float DistanceFactor { get; private set; } = 1;
    [field: SerializeField] public float ClusterGravityDelta { get; private set; } = -0.05f;
    [field: SerializeField] public float DestroyAfterTime { get; private set; } = 1f;
    [field: SerializeField] public bool UseDestroyAfterTime { get; private set; } = true;
}
