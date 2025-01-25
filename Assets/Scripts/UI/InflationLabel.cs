using System.Text;
using UnityEngine;

public class InflationLabel : MonoBehaviour
{
	public TMPro.TextMeshProUGUI Label;
	private Bubble _player;

	private float _lastVolume = -1;

	private void Start()
	{
		_player = FindFirstObjectByType<Player>().GetComponent<Bubble>();
	}
	
	private void Update()
	{
		if (!_player)
			return;

		if (Mathf.Approximately(_lastVolume, _player.NormalizedVolume))
			return;

		StringBuilder builder = new(_player.NormalizedVolume >= 0.8f ? "Size: Dummy Thi" : "Size: Thi");

		for (float f = 0.0f; f <= _player.NormalizedVolume; f += 0.1f)
		{
			builder.Append("c");
		}

		Label.text = builder.ToString();
	}
}