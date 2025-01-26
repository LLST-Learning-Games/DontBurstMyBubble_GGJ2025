using System;
using TMPro;
using UnityEngine;

public class DropOffZoneTimer : MonoBehaviour
{
    public Annihilator Annihilator;
    [SerializeField] private TextMeshProUGUI _label;

    private void Update()
    {
        if (!Annihilator)
            return;

        string prefix = $"Drop Off Zone #{Annihilator.Index + 1}\n";
        string suffix = null;
        
        if (Annihilator.IsFull)
        {
            suffix = "<color=#00FFFF>Full";
        }
        else if (Annihilator.TimeRemaining > TimeSpan.Zero)
        {
            suffix = $"<color=green>{Math.Floor(Annihilator.TimeRemaining.TotalMinutes):00}:{Annihilator.TimeRemaining.Seconds:00} remaining";
        }
        else
        {
            suffix = $"<color=red>locked";
        }

        _label.text = prefix + suffix;
    }
}
