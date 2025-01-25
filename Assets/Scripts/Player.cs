using System;
using DefaultNamespace;
using UnityEngine;

public class Player : MonoBehaviour
{
	public float InvulnerabilityTimeSeconds = 2.0f;
	public PlayerCheckpointController CheckpointController;
	
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
			
			PlayerState.Current = (PlayerState.LastCheckpoint ?? new()) with { };
			CheckpointController.MoveToStartPoint();
		}

		return true;
	}
}