using System;
using UnityEngine;

public class WinCollider : MonoBehaviour
{
	public const int pointsPerBubble = 100;
	
	private void OnTriggerEnter2D(Collider2D other)
	{
		Player player = other.GetComponent<Player>();
		Bubble bubble = other.GetComponent<Bubble>();
		
		if (!player)
			return;

		if (other.isTrigger)
			return;

		int objectiveScore = CalculateScore(
			bubble.GetComponentInChildren<SuctionZone>().bubbles.Count,
			//bubble.AttachedBubbles.Count,
			PlayerState.Current.Lives,
			DateTime.Now - PlayerState.Current.LevelStartTime
		);

		PlayerState.Current.Score += objectiveScore;
		
		FindFirstObjectByType<WinController>().TriggerWin();
	}

	public static int CalculateScore(int numberOfBubbles, int numberOfLives, TimeSpan duration)
	{
		return (int)((numberOfBubbles * pointsPerBubble)
			//+ (numberOfLives * 10_000)
			+ (duration.TotalSeconds * 0.0));
	}
}