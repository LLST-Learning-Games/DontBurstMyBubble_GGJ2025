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
			Debug.Log($"[{GetType().Name}] Player died");
			Time.timeScale = 0.0f;
			OnGameOver?.Invoke();
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