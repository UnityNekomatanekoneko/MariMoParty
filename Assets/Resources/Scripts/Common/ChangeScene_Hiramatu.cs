using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ChangeScene_Hiramatu: MonoBehaviour {

    public new string name;
    public void Change()
    {
		name = "Scene/Game/Hiramatsu_01";
		SceneManager.LoadScene(name);
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnMouseDown()
	{
		Change();
	}
}
