using UnityEngine;
using UnityEngine.UI;

public class WorldTime : MonoBehaviour
{
	private TMPro.TextMeshProUGUI text;
	private float clock;
	public bool finishlevel;
	public bool stopClock;
	private Score score;

	// Use this for initialization
	void Start()
	{
		text = GetComponent<TMPro.TextMeshProUGUI>();
		score = GameObject.Find("Score").GetComponent<Score>();
		clock = 400;
	}

	// Update is called once per frame
	void FixedUpdate()
	{
		if (finishlevel)
		{
			clock = Mathf.Ceil(clock);
			if (clock > 0)
			{
				clock--;
				score.AddScore(50);
				
			}
			text.text = "Time\n" + Mathf.Ceil(clock);
		}
		else if (!stopClock)
		{
			clock -= (1 / 60f) * 2.408f;
			text.text = "Time\n" + Mathf.Ceil(clock);
		}
	}
}
