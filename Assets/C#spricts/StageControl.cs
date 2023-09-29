using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageControl : MonoBehaviour
{
    [Header("ゲームオーバー")] public GameObject gameOverObj;
    [Header("コンティニュー位置")] public GameObject continuePoint;
    [Header("プレイヤーゲームオブジェクト")] public GameObject playerObj;
    [Header("フェード")] public FadeImage fade;

    private Player p;
    private int nextStageNum;
    private bool startFade = false;
    private bool doGameOver = false;
    private bool retryGame = false;
    private bool doSceneCharge = false;


    // Start is called before the first frame update
    void Start()
    {
        if(playerObj != null && continuePoint != null && gameOverObj != null && fade != null)
        {
            gameOverObj.SetActive(false);

            playerObj.transform.position = continuePoint.transform.position;
            p = playerObj.GetComponent <Player>();
            if(p == null)
            {
                Debug.Log("プレイヤーじゃないものがアタッチされているよ");
            }
        }
        else
        {
            Debug.Log("ステージの設定が足りていないよ");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ゲームオーバーの時の処理
        if(GameManager.instance.isGameOver && !doGameOver)
        {
            gameOverObj.SetActive(true);
            doGameOver = true;
        }

        if (fade != null&& startFade && !doSceneCharge)
        {
            if (fade.IsFadeOutComplete())
            {
                if (retryGame)
                {
                    GameManager.instance.RetryGame();
                }
                else
                {
                    GameManager.instance.stageNum = nextStageNum;
                }
            }

            SceneManager.LoadScene("stage" + nextStageNum);//シーン移動はここからやってもろて
            doSceneCharge = true;
        }
    }

    /// <summary>
    /// 最初から始める
    /// </summary>
    public void Retry()
    {
        ChangeScene(1);
        retryGame = true;

    }

    public void ChangeScene(int num)
    {
        if(fade != null)
        {
            nextStageNum = num;
            fade.StartFadeOut();
            startFade = true;
        }
        else
        {
            Debug.Log("フェードつけ忘れているかもしれん");
        }

    }
}
