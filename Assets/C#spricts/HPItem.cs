using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPItem : MonoBehaviour
{
    [Header("���Z����HP")] public int myGetHp;
    [Header("�v���C���[�̔���")] public PlayerTriggerCheck playerCheck;

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

                Destroy(this.gameObject);
            }

        }

    }
}
