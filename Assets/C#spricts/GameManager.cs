using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int score;
    public int stageNum;
    public int continueNum;

    //データにアクセスするコード：GameManager.instance.score += ...;

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
