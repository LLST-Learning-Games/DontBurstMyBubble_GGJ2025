using System;
using UnityEngine;

public class WinCollider : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (!other.GetComponent<Player>())
			return;

		if (other.isTrigger)
			return;
		
		FindFirstObjectByType<WinController>().TriggerWin();
	}
}