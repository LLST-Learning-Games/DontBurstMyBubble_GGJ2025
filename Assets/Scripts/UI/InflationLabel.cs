using System.Text;
using UnityEngine;

public class InflationLabel : MonoBehaviour
{
	public TMPro.TextMeshProUGUI Label;
	public Bubble Player;

	private float _lastVolume = -1;
	
	private void Update()
	{
		if (!Player)
			return;

		if (Mathf.Approximately(_lastVolume, Player.NormalizedVolume))
			return;

		StringBuilder builder = new(Player.NormalizedVolume >= 0.8f ? "Size: Dummy Thi" : "Size: Thi");

		for (float f = 0.0f; f <= Player.NormalizedVolume; f += 0.1f)
		{
			builder.Append("c");
		}

		Label.text = builder.ToString();
	}
}