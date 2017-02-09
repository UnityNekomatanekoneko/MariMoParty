using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EventSystem : MonoBehaviour {

	public static string gameover = " ";

	// Use this for initialization
	void Start () {
		GetComponent<Text>().text = gameover;
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Text>().text = gameover;
	}
}
