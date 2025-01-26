
using UnityEngine;
using UnityEngine.Events;

public class AnimationLinker : MonoBehaviour
{
    [SerializeField] private UnityEvent _onAnimationTrigger;

    public void OnAnimationTrigger()
    {
        _onAnimationTrigger?.Invoke();
    }
}