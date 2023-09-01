using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCheck : MonoBehaviour
{

    private bool isDamaged = true;
    private string playerTag = "Player";
    private bool isDamagedEnter, isDamagedStay, isDamagedExit;
    public bool IsDamaged()
    {
        isDamaged = false;
        if (isDamagedEnter || isDamagedStay)
        {
            isDamaged = true;
        }
        else if (isDamagedExit)
        {
            isDamaged = false;
        }

        isDamagedEnter = false;
        isDamagedStay = false;
        isDamagedExit = false;
        return isDamaged;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if (collision.tag == playerTag)
        {
            isDamagedEnter = true;
            Debug.Log("�v���C���[�ƐڐG���܂���");
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == playerTag)
        {
            isDamagedStay = true;
            Debug.Log("P������ɓ��葱���Ă��܂�");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == playerTag)
        {
            isDamagedExit = true;
            Debug.Log("P�����肩��o�܂���");

        }
    }
}
