using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    private bool isGround = true;
    private string groundTag =  "Ground";
    private bool isGroundEnter, isGroundStay, isGroundExit;
    
    public bool IsGround()
    {
        isGround = false;
        if (isGroundEnter || isGroundStay) 
        {
            isGround = true;
        }
        else if (isGroundExit)
        {
            isGround = false;
        }

        isGroundEnter = false;
        isGroundStay = false;
        isGroundExit = false;
        return isGround;



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {



        if (collision.tag == groundTag)
        {
            isGroundEnter = true;
            //Debug.Log("�n�ʂ�����ɓ���܂���");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        
        if (collision.tag == groundTag)
        {
            isGroundStay = true;
            //Debug.Log("�n�ʂ�����ɓ��葱���Ă��܂�");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        if (collision.tag == groundTag)
        {
            isGroundExit = true;
            //Debug.Log("�n�ʂ����肩��o�܂���");

        }
    }

    //���X�|�[������ƂȂ�������OnTrriger�B�������Ȃ��Ȃ��Ă��܂��B�B�B


}
