using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    [Header("�t�F�[�h")] public FadeImage fade;
    public bool firstPush = false;
    private bool goNextScene = false;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Game srart");
    }
    //�X�^�[�g��ʂŃ{�^�����������ƌĂ΂��
    public void PressStart()
    {
        Debug.Log("Press started");

        if (!firstPush)
        {
            Debug.Log("go next Scene");

            //���Ɉڂ閽�߂�����

            fade.StartFadeOut();

            SceneManager.LoadScene("stage1");

            firstPush = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!goNextScene && fade.IsFadeOutComplete()) 
        {
            SceneManager.LoadScene("stage1");
            goNextScene = true;
        }

    }

}
