using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Coins : MonoBehaviour
{
	private TMPro.TextMeshProUGUI text;
	private int coins;

	// Use this for initialization
	void Start()
	{
		text = GetComponent<TMPro.TextMeshProUGUI> ();
		coins = 0;
	}

	public void AddCoins(int coins)
	{
		this.coins += coins;
		text.text = "x" + this.coins.ToString();
	}

	public int GetCoins()
    {
		return this.coins;
    }
}
