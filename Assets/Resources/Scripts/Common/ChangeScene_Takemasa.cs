using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene_Takemasa : MonoBehaviour {

	public new string name;
	public void Change()
	{
		name = "Scene/Game/Takemasa_01";
		SceneManager.LoadScene(name);
	}

	// Use this for initialization
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	void OnMouseDown()
	{
		Change();
	}
}
