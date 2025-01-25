using UnityEngine;

public class LivesLabel : MonoBehaviour
{
    public TMPro.TextMeshProUGUI Label;

    private void Update()
    {
        Label.text = "Lives: " + (PlayerState.Current.IsGodMode ? "\u221e" : PlayerState.Current.Lives.ToString("0"));
    }
}
