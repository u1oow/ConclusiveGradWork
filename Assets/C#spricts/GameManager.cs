using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [Header("現在のスコア")] public int score;
    [Header("現在のステージ番号")] public int stageNum;
    [Header("初期HP")] public int defaultHeartNum;
    [Header("プレイヤーのHP,HP変更するならこっち")] public int playerHp;
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
}
