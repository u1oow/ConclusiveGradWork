using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackLifeControl : MonoBehaviour
{
    [SerializeField, Header("HP�A�C�R��")]
    #region//�v���C�x�[�g�ϐ�
    private GameObject playerIcon;
    private Player player;
    private int beforeHP;
    //private Animator anim = null;
    //private bool life = true;
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        beforeHP = player.GetHP();
        CreateHPIcon();

        //anim = GetComponent<Animator>();
        //anim.SetBool("LifeOn", true);
        //�A�N�e�B�u��A�N�e�B�u�ł͂Ȃ��A�A�j���[�V�����ɂ�郉�C�t�̃e�N�X�`���̐؂�ւ������݂Ă���i��������ۗ��j
    }

    /// <summary>
    /// �v���C���[HP�ɉ����ă��C�t�A�C�R����ݒu����
    /// </summary>
    /// <returns>���C�t�ڒu</returns>
    private void CreateHPIcon()
    {
        for (int i = 0; i < player.GetHP(); i++)
        {
            GameObject playerHPObj = Instantiate(playerIcon);
            playerHPObj.transform.parent = transform;
            //��������HP�A�C�R���̐e�I�u�W�F�N�g�Ƀv���C���[HP�������Ă���Q�[���I�u�W�F�N�g���w�肵�܂�
            //���̎��_�Ńn�[�g�̃N���[���̕��������邱�Ƃ��\
        }
    }
}
