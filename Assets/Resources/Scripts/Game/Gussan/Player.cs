using UnityEngine;
using System.Collections;
using System;

public class Player : MonoBehaviour {
    float moveSpeed = 0.01f;
    float count = 0;
    Animator _animator;
    private int startTime;      //スタートの瞬間の時間を記憶する
    private int now;            //現在の時間を記憶する
    private int duration;       //スタートと現在時間の差
    GameObject Child;
    GameObject g_Time;
    GameTime _gametime;

    public enum ButtonState
    {
        None,           //何も押されていない状態
        r_button,        //前回rightボタンが押された
        l_button         //前回leftボタンが押された
    }
    public ButtonState b_state = ButtonState.None;      //ステートをNoneで初期化

    // Use this for initialization
    void Start () {
        //子関係のカメラを探す
        Child = transform.FindChild("Main Camera").gameObject;

        //てきすと　おぶじぇくとを　てに　いれた。
        g_Time = GameObject.Find("Canvas/Text");

        //アニメーターをゲットだぜ
        _animator = GetComponent<Animator>();

        //やったぜ！所要時間わずか1ｆ未満で時間計測のスクリプトをゲットだ！
        _gametime = g_Time.GetComponent<GameTime>();

        //スタートタイム　時間　分　秒　をミリ秒に合わせる。
        startTime = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 +
            DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 Pos = transform.localPosition;      //プレイヤーのポジションを格納
        Quaternion Rot = transform.localRotation;   //プレイヤーの回転情報を格納
        
        switch (b_state)                        //連続でボタンを押しても反応しないようにする
        {
            case ButtonState.None:
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    moveSpeed += 0.03f;
                    b_state = ButtonState.r_button;
                    _gametime.setState(GameTime.g_state.begin);     
                }
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    moveSpeed += 0.03f;
                    b_state = ButtonState.l_button;
                    _gametime.setState(GameTime.g_state.begin);
                }
                
                break;
            case ButtonState.r_button:
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    moveSpeed += 0.05f;
                    b_state = ButtonState.l_button;
                }
                break;
            case ButtonState.l_button:
                if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    moveSpeed += 0.05f;
                    b_state = ButtonState.r_button;
                }
                break;
        }

        //ちょっとずつ減速していくフレンズなんだね。
        if (moveSpeed > 0.00f && count % 6 == 5)
        {
            moveSpeed -= 0.03f;
            count = 0;
			if (moveSpeed < 0.0f) {
				moveSpeed = 0.0f;

			}
        }
        count++;
        Pos.z -= moveSpeed;

        //ゴールした時にカメラの親子関係を切る
        if (transform.localPosition.z < -240)
        {
            if (Child.transform.parent)
            {
                now = DateTime.Now.Hour * 60 * 60 * 1000 + DateTime.Now.Minute * 60 * 1000 +
                DateTime.Now.Second * 1000 + DateTime.Now.Millisecond;
                Child.transform.SetParent(null);
                _gametime.setState(GameTime.g_state.gamefinish);
            }
        }

        //落下した後にコース上に戻ってくる。きっとくる。
        if(transform.localPosition.y < -30)
        {
			Pos.x = 1.73f;      Pos.y = 1.0f;       Pos.z = -210.0f;
            Rot.x = 0.0f;       Rot.y = 180.0f;     Rot.z = 0.0f;
			moveSpeed = 0.6f;
        }


        //移動速度でアニメーションを遷移させる。ゆうこかわいい
        if (moveSpeed >= 0.25f)
        {
            _animator.Play("Running@loop");
        }
        else if (moveSpeed > 0.00f)
        {
            _animator.Play("Walking@loop");
        }
        else if (moveSpeed == 0.00f)
        {
            _animator.Play("Standing@loop");
        }
        transform.localPosition = Pos;      //ポジションをプレイヤーに返す
        transform.localRotation = Rot;      //回転情報をプレイヤーに返す
    }

    //エラー回避
    void OnCallChangeFace()
    {

    }
}
