using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [Header("���݂̃X�R�A")] public int score;
    [Header("���݂̃X�e�[�W�ԍ�")] public int stageNum = 1;
    [Header("����HP,HP�ύX����Ȃ炱����")] public int defaultHeartNum;
    [Header("�v���C���[��HP")] public int playerHp;
    //[Header("")] public int continueNum;
    [HideInInspector] public bool isGameOver;

    //�f�[�^�ɃA�N�Z�X����R�[�h�FGameManager.instance.playerHp += ...;

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

    public void RetryGame()
    {
        isGameOver = false;
        playerHp = defaultHeartNum;
        score = 0;
        //stageNum = 1;
        
    }
}
