using System;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
	public float InvulnerabilityTimeSeconds = 2.0f;
	public PlayerCheckpointController CheckpointController;
	public UnityEvent OnGameOver;
	
	private float _lastCollisionTime;

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.G))
		{
			PlayerState.Current.IsGodMode = !PlayerState.Current.IsGodMode;
		}
	}

	public void Kill()
	{
		Debug.Log($"[{GetType().Name}] Player died");
		PlayerState.Current.Lives = 0;
		Time.timeScale = 0.0f;
		OnGameOver?.Invoke();
	}
	
	public bool TryDealDamage()
	{
		if (PlayerState.Current.IsGodMode)
			return false;
		
		float secondsSinceLastImpact = Time.time - _lastCollisionTime;

		if (secondsSinceLastImpact <= InvulnerabilityTimeSeconds)
			return false;

		_lastCollisionTime = Time.time;
		
		if (--PlayerState.Current.Lives <= 0)
		{
			Kill();
		}

		return true;
	}

	public void ReloadCheckpoint()
	{
		PlayerState.Current = (PlayerState.LastCheckpoint ?? new()) with { };
		CheckpointController.MoveToStartPoint();
		Time.timeScale = 1.0f;
	}
}