using UnityEngine;

public class BubblesManager : SingletonBase<BubblesManager>
{
    [field: SerializeField] public float StickyStrength { get; private set; } = 1;
    [field: SerializeField] public bool UseSizeInCalculations { get; private set; } = true;
    [Tooltip("How much size of other bubble's colliders affect this bubble's stickiness to them.")]
    [field: SerializeField] public float SizeFactor { get; private set; } = 1;
    [field: SerializeField] public bool UseDistanceInCalculations { get; private set; } = true;
    [field: SerializeField] public float DistanceFactor { get; private set; } = 1;
}
