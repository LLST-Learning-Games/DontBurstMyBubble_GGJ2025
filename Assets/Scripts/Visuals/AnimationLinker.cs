
using UnityEngine;
using UnityEngine.Events;

public class AnimationLinker : MonoBehaviour
{
    [SerializeField] private UnityEvent _onAnimationTrigger;
    [SerializeField] private UnityEvent _onAnimationTriggerSecond;

    public void OnAnimationTrigger()
    {
        _onAnimationTrigger?.Invoke();
    }
    
    public void OnAnimationTriggerSecond()
    {
        _onAnimationTriggerSecond?.Invoke();
    }
}