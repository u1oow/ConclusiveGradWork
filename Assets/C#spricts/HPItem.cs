using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

public class HPItem : MonoBehaviour
{
    [Header("���Z����HP")] public int myGetHp;
    [Header("�v���C���[�̔���")] public PlayerTriggerCheck playerCheck;
    [Header("���C�t�A�C�R���\���I�u�W�F�N�g")] private GameObject lifeMakerObj;
    //private int beforeHP;

    //HP hpMakerScript;

    // Update is called once per frame
    void Update()
    {
        if (playerCheck.isOn)
        {
            //�v���C���[������ɓ�������
            if (GameManager.instance != null)
            {
                GameManager.instance.playerHp += myGetHp;

                if (GameManager.instance.playerHp > GameManager.instance.defaultHeartNum)
                {
                    GameManager.instance.playerHp = GameManager.instance.defaultHeartNum;//����HP��HP����������HP������HP�ɐݒ肷��
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
