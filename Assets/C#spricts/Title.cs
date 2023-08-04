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
    //スタート画面でボタンが押されると呼ばれる
    public void PressStart()
    {
        Debug.Log("Press started");

        if (!firstPush)
        {
            Debug.Log("go next Scene");

            //次に移る命令を書く

            firstPush = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
