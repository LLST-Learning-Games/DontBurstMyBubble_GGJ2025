using System;
using UnityEngine;

public record PlayerState
{
	public static PlayerState Current = new();
	public static PlayerState LastCheckpoint;

	public bool IsGodMode;
	public int Lives = 1;
	public int Score = 0;
	public int LastStartPointID = 0;
	public DateTime LevelStartTime = DateTime.Now;
}