using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItem : MonoBehaviour
{
    [Header("加算するHP")] public int myGetHp;
    [Header("プレイヤーの判定")] public PlayerTriggerCheck playerCheck;

    // Update is called once per frame
    void Update()
    {
        if (playerCheck.isOn)
        {
            //プレイヤーが判定に入ったら
            if (GameManager.instance != null)
            {
                GameManager.instance.playerHp += myGetHp;
                Destroy(this.gameObject);
            }

        }

    }
}
