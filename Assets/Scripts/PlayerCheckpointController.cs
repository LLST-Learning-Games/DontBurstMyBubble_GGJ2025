using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCheckpointController : MonoBehaviour
{
	private List<PlayerStartPoint> _playerStartPoints = new();
	
	private void Start()
	{
		FindStartPoints();
		MoveToStartPoint();
		Time.timeScale = 1.0f;
	}
	
	#if UNITY_EDITOR
	private void Update()
	{
		foreach (var playerStartPoint in _playerStartPoints)
		{
			if (playerStartPoint.StartPointID is < 0 or > 9)
				continue;

			if (Input.GetKeyDown(KeyCode.Alpha0 + playerStartPoint.StartPointID))
			{
				PlayerState.Current.LastStartPointID = playerStartPoint.StartPointID;
				PlayerState.LastCheckpoint = PlayerState.Current with { };
				MoveToStartPoint();
				return;
			}
		}
	}
	#endif

	public void MoveToStartPoint()
	{
		if (FindCurrentStartPoint() is not { } startPoint)
			throw new InvalidOperationException("Could not find start point");

		transform.position = startPoint.transform.position;
	}

	private void FindStartPoints()
	{
		var groups = FindObjectsByType<PlayerStartPoint>(FindObjectsSortMode.None)
			.GroupBy(psp => psp.StartPointID);
		_playerStartPoints.Clear();

		foreach (var group in groups)
		{
			if (group.Count() != 1)
			{
				Debug.LogError($"[{GetType().Name}] There are {group.Count()} copies of the {group.FirstOrDefault().StartPointID} start point!");
			}
			
			_playerStartPoints.AddRange(group);
		}
	}
	
	private PlayerStartPoint FindCurrentStartPoint()
	{
		return FindObjectsByType<PlayerStartPoint>(FindObjectsSortMode.None)
			.FirstOrDefault(psp => psp.StartPointID == PlayerState.Current.LastStartPointID);
	}
}