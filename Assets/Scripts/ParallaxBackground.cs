using System;
using System.Collections;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour
{
    public float Speed;

    private Player _player;
    private Vector3 _lastPosition;

    private IEnumerator Start()
    {
        _player = FindFirstObjectByType<Player>();
        yield return null;
        _lastPosition = _player.transform.position;
    }

    private void LateUpdate()
    {
        if (!_player)
            return;

        Vector3 delta = _player.transform.position - _lastPosition;
        _lastPosition = _player.transform.position;
        
        transform.Translate(delta * Speed);
    }
}
