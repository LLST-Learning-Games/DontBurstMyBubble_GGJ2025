using System;
using UnityEngine;
using UnityEngine.Serialization;

public class RetryButton : MonoBehaviour
{
    [SerializeField] private GameObject _gameOverRoot;

    private Player _player = null;
    
    private void Start()
    {
        _player = FindFirstObjectByType<Player>();
        _player.OnGameOver.AddListener(OnGameOver);
    }

    private void OnGameOver()
    {
        _gameOverRoot.SetActive(true);
    }

    public void OnClick()
    {
        _player.ReloadCheckpoint();
        _gameOverRoot.SetActive(false);
    }
}
