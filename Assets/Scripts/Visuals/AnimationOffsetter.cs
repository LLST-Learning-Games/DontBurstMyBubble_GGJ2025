using UnityEngine;

public class AnimationOffsetter : MonoBehaviour
{
    [SerializeField] private string _stateName;
    [SerializeField] private Animator _animator;

    public void OnEnable()
    {
        _animator.Play(_stateName, -1, Random.Range(0f, 1f));
    }
    
}