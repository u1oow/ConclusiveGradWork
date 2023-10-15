using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

        if (playerObj != null && continuePoint != null && gameOverObj != null && fade != null)
        {
            gameOverObj.SetActive(false);
            playerObj.SetActive(true);

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
        if (GameManager.instance.isGameOver && !doGameOver)
        {

            playerObj.SetActive(false);//ゲームオーバーでぱっと消えるのも変かもしれん
            gameOverObj.SetActive(true);
            doGameOver = true;

        }

        if (fade != null&& startFade && !doSceneCharge)
        {
            Debug.Log("フェード");
            Debug.Log(retryGame);

            if (fade.IsFadeOutComplete())
            {
                Debug.Log("フェード完了");

                if (retryGame)
                {
                    GameManager.instance.RetryGame();
                }
                else
                {
                    GameManager.instance.stageNum = nextStageNum;
                }
                Debug.Log("stage" + nextStageNum);
                SceneManager.LoadScene("stage" + nextStageNum);//シーン移動はここからやってもろて"stage" + nextStageNum
                doSceneCharge = true;
            }
            else
            {
                Debug.Log("フェード失敗");
            }
        }
    }

    /// <summary>
    /// 最初から始める
    /// </summary>
    /// 

    public void RetryButton()
    {
        GameManager.instance.isGameOver = true;
    }
    public void Retry()
    {
        ChangeScene(GameManager.instance.stageNum);
        retryGame = true;
        Debug.Log(GameManager.instance.stageNum);
        Debug.Log(fade != null && startFade && !doSceneCharge);
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
