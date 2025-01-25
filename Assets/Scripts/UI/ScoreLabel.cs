using UnityEngine;

public class ScoreLabel : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Label;

    private void Update()
    {
        Label.text = $"Score: {PlayerState.Current.Score:0}";
    }
}
