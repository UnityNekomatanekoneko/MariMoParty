using UnityEngine;
using System.Collections;

public class CoinManager : MonoBehaviour
{
    public GameObject Coin;

    float CoinEmissionTime = 2.0f; //コインを排出する時間。秒
    float LocalTime; //ローカルタイム
    int CoinMax = 20; //コイン排出最大数
    public int CoinNum; //現在あるコインの数

    float CoinEmissionRange = 9.5f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        CoinEmission();
    }

    void CoinEmission()
    {
        if (LocalTime >= CoinEmissionTime)
        {
            if (CoinMax > CoinNum)
            {
                Vector3 EmissionPos = new Vector3();
                EmissionPos.x = Random.Range(-CoinEmissionRange, CoinEmissionRange);
                EmissionPos.z = Random.Range(-CoinEmissionRange, CoinEmissionRange);
                EmissionPos.y = 3;
                GameObject coin = Instantiate(Coin, EmissionPos, Coin.transform.rotation) as GameObject;
                coin.transform.parent = transform;
                CoinNum++;
            }
            LocalTime = 0;
        }
        LocalTime += Time.deltaTime;
    }
}
