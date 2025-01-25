using System;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace
{
	public class PlayerCheckpointController : MonoBehaviour
	{
		private void Start()
		{
			ValidateStartPoints();
			MoveToStartPoint();
		}
		
		#if UNITY_EDITOR
		private void Update()
		{
			if (Input.GetKeyDown(KeyCode.R))
			{
				MoveToStartPoint();
			}
		}
		#endif

		public void MoveToStartPoint()
		{
			if (FindCurrentStartPoint() is not { } startPoint)
				throw new InvalidOperationException("Could not find start point");

			transform.position = startPoint.transform.position;
		}

		private void ValidateStartPoints()
		{
			var groups = FindObjectsByType<PlayerStartPoint>(FindObjectsSortMode.None)
				.GroupBy(psp => psp.StartPointID);

			foreach (var group in groups)
			{
				if (group.Count() != 1)
				{
					Debug.LogError($"[{GetType().Name}] There are {group.Count()} copies of the {group.FirstOrDefault().StartPointID} start point!");
				}
			}
		}
		
		private PlayerStartPoint FindCurrentStartPoint()
		{
			return FindObjectsByType<PlayerStartPoint>(FindObjectsSortMode.None)
				.FirstOrDefault(psp => psp.StartPointID == PlayerState.Current.LastStartPointID);
		}
	}
}