using System.Linq;
using UnityEngine;

public static class PhysicsUtility
{
	public static bool IsPlayer(GameObject gameObject)
	{
		return gameObject.GetComponent<Player>();
	}

	public static bool IsPlayerOrAttachedTo(GameObject gameObject)
	{
		if (IsPlayer(gameObject))
			return true;
		
		Bubble bubble = gameObject.GetComponent<Bubble>();

		if (!bubble)
			return false;
		
		Player player = Object.FindFirstObjectByType<Player>();
		Bubble playerBubble = player.GetComponent<Bubble>();

		return playerBubble.AttachedBubbles.Contains(bubble);
	}
}