using UnityEngine;

public class ScoreLabel : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Label;
    public bool ShowCalcualtion;

    private void Update()
    {
        if (ShowCalcualtion)
        {
            Label.text = $"Score: {PlayerState.Current.Score:G}\n<size=80%>({PlayerState.Current.Score / 100:0} Bubbles x 100)";
            return;
        }
        Label.text = $"Score: {PlayerState.Current.Score:G}";
    }
}
