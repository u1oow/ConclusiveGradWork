using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    public bool firstPush = false;

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

            firstPush = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
