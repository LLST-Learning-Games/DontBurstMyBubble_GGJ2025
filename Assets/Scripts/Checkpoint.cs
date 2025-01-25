using System;
using UnityEngine;

namespace DefaultNamespace
{
	public class Checkpoint : PlayerStartPoint
	{
		private void OnTriggerEnter2D(Collider2D other)
		{
			//BRD: Ideally I would use a unique tag for the player, but the player needs to have the "Bubble" tag
			if (!other.gameObject.GetComponent<PlayerCheckpointController>())
				return;

			if (PlayerState.Current.LastStartPointID >= StartPointID)
				return;

			PlayerState.Current.LastStartPointID = StartPointID;
			PlayerState.LastCheckpoint = PlayerState.Current with { };
			Debug.Log($"[{GetType().Name}] Player reached checkpoint {StartPointID}");
		}
	}
}