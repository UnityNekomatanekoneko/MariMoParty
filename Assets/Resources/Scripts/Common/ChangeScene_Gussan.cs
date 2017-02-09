using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene_Gussan : MonoBehaviour {

	public new string name;
	public void Change()
	{
		name = "Scene/Game/Gussan_01";
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
