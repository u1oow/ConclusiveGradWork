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
                Destroy(this.gameObject);
            }

        }

    }
}
