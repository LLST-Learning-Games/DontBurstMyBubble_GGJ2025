using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HighScoreController : MonoBehaviour
{
    public const int MAX_HIGH_SCORE_COUNT = 25;
    
    [SerializeField] private HighScoreEntry _highScoreEntryPrefab;
    [SerializeField] private RectTransform _highScoreContainer;
    [SerializeField] private TMP_InputField _nameInputField;
    [SerializeField] private Button _submitButton;

    private bool _hasSubmitted = false;
    private readonly List<HighScoreEntry> _entries = new();

    private void OnEnable()
    {
        SortAndInstantiate();
    }

    private void SortAndInstantiate()
    {
        foreach (HighScoreEntry highScoreEntry in _entries)
        {
            Destroy(highScoreEntry.gameObject);
        }
        
        _entries.Clear();
        GlobalState.Instance.Leaderboard.Sort();
        GlobalState.Instance.Leaderboard.ForEach(InstantiateEntry);
    }
    
    private void InstantiateEntry(ScoreRecord scoreRecord)
    {
        HighScoreEntry entry = Instantiate(_highScoreEntryPrefab, _highScoreContainer, false);
        entry.NameLabel.text = scoreRecord.Name;
        entry.ScoreLabel.text = scoreRecord.Score.ToString("0");
        _entries.Add(entry);
    }

    public void SubmitHighScore()
    {
        if (_nameInputField.text is not { Length: > 0 })
            return;

        _hasSubmitted = true;

        ScoreRecord scoreRecord = new()
        {
            Score = PlayerState.Current.Score,
            Name = _nameInputField.text,
        };
        
        GlobalState.Instance.Leaderboard.Add(scoreRecord);
        GlobalState.Instance.Leaderboard.Sort();

        while (GlobalState.Instance.Leaderboard.Count > MAX_HIGH_SCORE_COUNT)
        {
            GlobalState.Instance.Leaderboard.RemoveAt(GlobalState.Instance.Leaderboard.Count - 1);
        }
        
        GlobalState.Save();
        
        SortAndInstantiate();
        OnNameInputFieldChanged();
    }

    public void OnNameInputFieldChanged()
    {
        _submitButton.interactable = _nameInputField.text?.Length > 0 && !_hasSubmitted;
    }
}
