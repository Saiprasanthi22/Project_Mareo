using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
	private TMPro.TextMeshProUGUI text;
	private int score;
	private int total = 0;
	private Coins coins;

	// Use this for initialization
	void Start()
	{
		text = GetComponent<TMPro.TextMeshProUGUI>();
		coins = GameObject.Find("Coins").GetComponent<Coins>();
	}

	public void AddScore(int score)
	{
		
		this.score += score;
		
		text.text = "Mario\n" + this.score.ToString().PadLeft(6, '0');
	}

	public void FixedUpdate()
    {
		total = coins.GetCoins() * 50;
		text.text = "Mario\n" + total.ToString().PadLeft(6, '0');
	}
}
