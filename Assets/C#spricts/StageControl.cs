using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StageControl : MonoBehaviour
{
    [Header("�Q�[���I�[�o�[")] public GameObject gameOverObj;
    [Header("�R���e�B�j���[�ʒu")] public GameObject continuePoint;
    [Header("�v���C���[�Q�[���I�u�W�F�N�g")] public GameObject playerObj;
    [Header("�t�F�[�h")] public FadeImage fade;

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
                Debug.Log("�v���C���[����Ȃ����̂��A�^�b�`����Ă����");
            }
        }
        else
        {
            Debug.Log("�X�e�[�W�̐ݒ肪����Ă��Ȃ���");
        }
    }

    // Update is called once per frame
    void Update()
    {
        //�Q�[���I�[�o�[�̎��̏���
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

            SceneManager.LoadScene("stage" + nextStageNum);//�V�[���ړ��͂����������Ă����
            doSceneCharge = true;
        }
    }

    /// <summary>
    /// �ŏ�����n�߂�
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
            Debug.Log("�t�F�[�h���Y��Ă��邩�������");
        }

    }
}
