using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build;
using UnityEngine;

public class RespawnPoint : MonoBehaviour
{

    public GameObject respawnPoint;
    public Vector3 trans;
    public GameObject playerObject;//�v���C���[�I�u�W�F�N�g���擾���鏀��
    //private string objName;

    void Start()
    {
        trans = respawnPoint.transform.position;
    }
    public void FallPlayerWarper(Player player)
    {
        Debug.Log("�������Ă��܂��܂���");
        playerObject.gameObject.transform.position = trans;
        return;
        /*/objName = other.gameObject.name;
        //if�֐��̒���string���g�����@�͒m���Ă��邯�ǁA�v�͂Ȃ����ȁB
         if (objName.Equals("player"))
         {
             Debug.Log("�������Ă��܂��܂���");
             other.gameObject.transform.position = trans;    
         }
         /*/
    }
    

    //�����������ǂ�����ϐ��Ŕ��肵�悤�Ƃ�����ށ����Ǘ��������Ƃ��Ƀ��]�b�g���Ăяo�����ق����y���ƋC�t��
    /*/
    void FixedUpdate()
    {
        if (fall)
        {
            fall = false;
            //Debug.Log("�������Ă��܂��܂���");
            Player playerScript;
            GameObject obj = GameObject.Find("Player");
            playerScript = obj.GetComponent<Player>();
            playerScript.caurseOut = true;
        }

    }
    //�ǂ����Ă����ɍs���Ă��Ȃ��|�C���g�B
    //�����̖l�Ȃ猾���邯�ǁA.publicGameCbject player player;�ŖY�ꂸ�A�^�b�`����΂��������ȋC������
    /*/
}
