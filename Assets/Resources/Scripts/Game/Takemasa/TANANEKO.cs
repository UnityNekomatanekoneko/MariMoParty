using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Rigidbody))]

public class TANANEKO : MonoBehaviour {

	public Vector3 force = Vector3.zero;
	public int limit = 10;
	
	List<GameObject> TargetPositions = new List<GameObject>();
	Material material;


	Rigidbody2D rigidbody2D;
	bool isShoot = false;		//TAMA猫がシュートされている状態の時true
	float spinTAMA;		//回るTAMA猫の値
	GameObject ShootTarget;		//シュートターゲット

	float RndForce = 2.0f; //ランダムちから

	bool isGameOver = false;

	// Use this for initialization
	void Start () {

		rigidbody2D = GetComponent<Rigidbody2D>();
		spinTAMA    = 200.0f;
		ShootTarget = GameObject.FindWithTag("ShootTarget");

		material = Resources.Load("Resources\\Materials\\Game\\Takemasa\\ShootTarget") as Material;
		GetComponent<Rigidbody>().useGravity = false;

		RndForce = Random.Range(2.0f,3.0f);
	}
	
	// Update is called once per frame
	void Update () {

		if(isGameOver)
		{
			EventSystem.gameover = "　お　わ　り";
			return;
		}

		if (isShoot)
		{
			return;
		}

		//左マウスボタン押し続けて飛距離を伸ばす
		if(Input.GetMouseButton(0) && force.x <= 500.0f)
		{
			force.x += RndForce;
			
			//軌道オブジェクト生成
			CreateTargetPositions();
		}

		//左マウスボタン離してTAMA猫を飛ばす
		if (Input.GetMouseButtonUp(0) || force.x > 500.0f)
		{
			//吹っ飛ばす
			rigidbody2D.AddForce(force);

			//回転処理
			rigidbody2D.freezeRotation = false;	//物理演算による回転の影響を受ける
			rigidbody2D.AddTorque(spinTAMA);	//トルク(回転速度)を加える

			//TAMANEKOをシュート状態に変更
			isShoot = true;

			//軌道オブジェクト生成
			CreateTargetPositions();
		}
	}

	void Clear(){
		foreach (GameObject obj in TargetPositions)
		{
			Destroy(obj);
		}
		TargetPositions.Clear();
	}

	void CreateTargetPositions()
	{
		Clear();

		for (int i = 0; i < limit; i++)
		{
			Vector3 pos = CalcPosition(0.1f * i);
			GameObject sp = GameObject.CreatePrimitive(PrimitiveType.Sphere);
			sp.transform.position = pos;
			sp.transform.parent = transform.parent;
			sp.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
			Destroy(sp.GetComponent<SphereCollider>());
			sp.GetComponent<Renderer>().material = material;

			TargetPositions.Add(sp);
		}
	}

	Vector3 CalcPosition(float time)
	{
		Vector3 start = transform.position;
		Vector3 gravity = Physics2D.gravity;
		float mass = GetComponent<Rigidbody2D>().mass;

		Vector3 speed = (force / mass) * Time.fixedDeltaTime;
		Vector3 gravitySpeed = gravity * 0.5f * Mathf.Pow(time, 2);

		return start + (speed * time) + gravitySpeed;

	}


	// " 2 D の " オブジェクトが衝突したとき実行され関数
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (isGameOver)
		{
			return;
		}

		if (transform.position.y <= 3.0f && isShoot)
		{
			isGameOver = true;
			return;
		}

		//シュート状態から地面に衝突したTAMA猫をシュートされる前に戻す
		if (isShoot)
		{

			Vector2 pos; pos.x = -7.0f; pos.y = 2.5f;
			rigidbody2D.MovePosition(pos);
			rigidbody2D.velocity = Vector3.zero;	//加えた力を初期化

			//物理演算による回転の影響を受けない
			rigidbody2D.freezeRotation = true;

			//TAMANEKOのシュート状態を解除
			isShoot = false;

			//X軸のforceをリセット
			force.x = 0.0f;

			//軌道オブジェクト削除
			Clear();

			//軌道オブジェクト数　減
			if (limit - 3 > 3)
			{
				limit += -3;
			}

			//ランダムちから
			RndForce = Random.Range(0.5f, 4.0f);
		}
	}
 
}
