using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField, Header("�U����")] private int attackPower;
    [SerializeField, Header("���Z����X�R�A")] private int myScore;

    public void PlayerDamage(Player player)
    {
        player.Damage(attackPower);
        //�G�ڐG�̒ʒm��������A�����̍U���͂��v���[���[�ɓ`����
        
        /*/
        //�_���[�W��H�������A���̓G�i�����j�ɉ����ăX�R�A�����Z����
        if(GameManager.instance != null)
        {
            GameManager.instance.score += myScore;
        }
        /*/
    }
}
