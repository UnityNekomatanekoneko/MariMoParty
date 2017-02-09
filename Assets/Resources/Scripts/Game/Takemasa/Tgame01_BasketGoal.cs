using UnityEngine;
using System.Collections;

public class Tgame01_BasketGoal : MonoBehaviour {

	Tgame01_Score score;

	// Use this for initialization
	void Start () {
		score = GetComponent<Tgame01_Score>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnCollisionEnter2D(Collision2D collision)
	{
		Tgame01_Score.score += 10;
	}
}
