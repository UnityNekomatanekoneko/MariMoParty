using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class GameTime : MonoBehaviour {
    Text timeText;
    double g_time;
    double nowTime;
    double duration;
    bool flg = false;


	// Use this for initialization
	void Start () {
        timeText = GetComponentInChildren<Text>();  //UIのテキストの取得
        g_time = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 +
            DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
	}
	
	// Update is called once per frame
    void Update() {
        if (flg == true)
        {
            nowTime = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 +
                   DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
            duration = (nowTime - g_time) / 1000;
            timeText.text = "Time : " + duration.ToString();
        }
    }

    public void SetFlg(bool _flg)
    {
        flg = _flg;
    }
}
