using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [Header("現在のスコア")] public int score;
    [Header("現在のステージ番号")] public int stageNum = 1;
    [Header("初期HP,HP変更するならこっち")] public int defaultHeartNum;
    [Header("プレイヤーのHP")] public int playerHp;
    //[Header("")] public int continueNum;
    [HideInInspector] public bool isGameOver;

    //データにアクセスするコード：GameManager.instance.playerHp += ...;

    private void Awake()
    {
        if (instance == null)//シングルトン
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

    }

    public void RetryGame()
    {
        isGameOver = false;
        playerHp = defaultHeartNum;
        score = 0;
        //stageNum = 1;
        
    }
}
