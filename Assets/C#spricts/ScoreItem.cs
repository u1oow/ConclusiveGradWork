using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreItem : MonoBehaviour
{
    [Header("���Z����X�R�A")] public int myScore;
    [Header("�v���C���[�̔���")] public PlayerTriggerCheck playerCheck;

    // Update is called once per frame
    void Update()
    {
        if (playerCheck.isOn)
        {
            //�v���C���[������ɓ�������
            if (GameManager.instance != null)
            {
                GameManager.instance.score += myScore;
                Destroy(this.gameObject);
            }

        }
        
    }
}
