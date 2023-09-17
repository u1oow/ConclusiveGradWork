using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    public int score;
    public int stageNum;
    public int continueNum;

    //�f�[�^�ɃA�N�Z�X����R�[�h�FGameManager.instance.score += ...;

    private void Awake()
    {
        if (instance == null)//�V���O���g��
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
