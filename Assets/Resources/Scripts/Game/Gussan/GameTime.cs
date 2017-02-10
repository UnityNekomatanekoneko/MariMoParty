using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class GameTime : MonoBehaviour {
    Text timeText;
    double g_time;      //計算開始時点の時間を保持する
    double nowTime;     //現在時間を保持する
    double duration;    //初期時間と現在時間の差を保持しているというのだろうか...。

    Vector3 T_pos;      //タイムを表示する位置
    Vector3 T_scl;      //タイムの表示スケール

    public enum g_state        //ゲーム状況を把握するためのステート
    {
        None,           //特にこれといって役割りはない。だれもお前を愛さない。
        begin,          //ゲームが始まる...。
        playing,        //プレイ中。集中して連打するが良い
        gamefinish,     //ゲーム終了。お疲れ様です。
        gameafter
    }

    public g_state state;

//--------------------------------------------------------//スタート関数
	void Start () {
        timeText = GetComponentInChildren<Text>();  //get text component
        T_scl = transform.localScale;               //variable receives number of "localScale"
        T_pos = transform.localPosition;            //variable receives number of "localPosition"
        
        state = g_state.None;
	}
	
//-------------------------------------------------------//アップデート関数
    void Update() {
        switch (state)
        {
            case g_state.begin:                 //ゲームが始まるタイミングで一瞬だけ通る。このタイミングの時間を保守
                g_time = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 +
                         DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
                state = g_state.playing;
                break;
            case g_state.playing:               //プレイ中、計算。タイムに加算されていく
                nowTime = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 +
                   DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
                duration = (nowTime - g_time) / 1000;
                timeText.text = "Time : " + duration.ToString() + " sec";
                break;
            case g_state.gamefinish:            //ゲームがフィニッシュした時にタイムがグってなってドーンってなるやつ
                T_pos.x = 60.0f; T_pos.y = -60.0f; T_pos.z = 0.0f;
                T_scl.x = 3.0f; T_scl.y = 3.0f; T_scl.z = 0.0f;
                transform.localScale = T_scl;
                transform.localPosition = T_pos;
                state = g_state.gameafter;
                break;
            case g_state.gameafter:             //ゲームが終わった後。マウスクリックでタイトルへ
                if (Input.GetMouseButtonDown(0))
                {
                    SceneManager.LoadScene("Title");
                }
                if ((Input.GetKeyDown(KeyCode.R)))
                {
                    SceneManager.LoadScene("Scene/Game/Gussan_01");
                }
                break;
        }
    }


//-------------------------------------------------------//ステートを切り替えるパブリックな関数
    public void setState(g_state _state)
    {
        state = _state;
    }
}
