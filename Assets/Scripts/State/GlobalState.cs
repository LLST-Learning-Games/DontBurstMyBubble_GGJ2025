using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class GlobalState
{
	private const string PLAYER_PREFS_KEY = "GlobalState";
	
	private static GlobalState _instance = null;

	public static GlobalState Instance
	{
		get
		{
			if (_instance is null)
			{
				Load();
			}

			_instance ??= new();
			return _instance;
		}
	}
	
	public readonly List<ScoreRecord> Leaderboard = new();

	public static void Save()
	{
		string json = JsonConvert.SerializeObject(Instance);
		PlayerPrefs.SetString(PLAYER_PREFS_KEY, json);
	}

	public static void Load()
	{
		string json = PlayerPrefs.GetString(PLAYER_PREFS_KEY);

		if (json is not { Length: > 0 })
			return;

		_instance = JsonConvert.DeserializeObject<GlobalState>(json);
	}
}

public struct ScoreRecord : IComparable<ScoreRecord>
{
	public int Score;
	public string Name;
	
	public int CompareTo(ScoreRecord other)
	{
		int scoreComparison = Score.CompareTo(other.Score);
		
		if (Score.CompareTo(other.Score) != 0)
			return -scoreComparison;
		
		return string.Compare(Name, other.Name, StringComparison.Ordinal);
	}
}