using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Tgame01_Score : MonoBehaviour 
{

	public static int score = 0;


	// Use this for initialization
	void Start () {
		GetComponent<Text>().text = "SCORE:" + score.ToString();
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text>().text = "SCORE:" + score.ToString();
	}

	//Score加算
	public void ScoreUp(int point)
	{
		score += point;
		GetComponent<Text>().text = "SCORE:" + score.ToString();
	}

}
