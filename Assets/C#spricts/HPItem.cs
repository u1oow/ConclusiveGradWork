using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class HPItem : MonoBehaviour
{
    [Header("加算するHP")] public int myGetHp;
    [Header("プレイヤーの判定")] public PlayerTriggerCheck playerCheck;
    [Header("ライフアイコン表示オブジェクト")] private GameObject lifeMakerObj;
    //private int beforeHP;

    //HP hpMakerScript;

    // Update is called once per frame
    void Update()
    {
        if (playerCheck.isOn)
        {
            //プレイヤーが判定に入ったら
            if (GameManager.instance != null)
            {
                GameManager.instance.playerHp += myGetHp;

                if (GameManager.instance.playerHp > GameManager.instance.defaultHeartNum)
                {
                    GameManager.instance.playerHp = GameManager.instance.defaultHeartNum;//初期HPをHPが上回ったらHPを初期HPに設定する
                }
                //GameManager.instance.playerHp = beforeHP;
                //ReturnHPUpdate();
                Destroy(this.gameObject);
            }

        }

    }

    /*/
    public int ReturnHPUpdate()
    {
        lifeMakerObj = GameObject.Find("HP");
        hpMakerScript = lifeMakerObj.GetComponent<LifeControl>();
        isEnemyRayDamaged = playerScript.attackedEnemy;
    }
    /*/
}
