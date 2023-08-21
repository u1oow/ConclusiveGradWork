using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LifeControl : MonoBehaviour
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
        for(int i = 0;i < player.GetHP(); i++)
        {
            GameObject playerHPObj = Instantiate(playerIcon);
            playerHPObj.transform.parent = transform;
            //��������HP�A�C�R���̐e�I�u�W�F�N�g�Ƀv���C���[HP�������Ă���Q�[���I�u�W�F�N�g���w�肵�܂�
            //���̎��_�Ńn�[�g�̃N���[���̕��������邱�Ƃ��\
        }
    }

    // Update is called once per frame
    void Update()
    {
       //ShowHPIcon();
        /*/
        if(life)
        {
            //anim.SetBool("LifeOn",true );
        }
        else
        {
            //anim.SetBool("LifeOn",false);
        }
        /*/
    }

    /// <summary>
    /// �v���C���[HP�ɉ��������C�t��\������
    /// </summary>
    /// <returns>���C�t�Ǘ�</returns>
    
    /*/
    private void ShowHPIcon()
    {
        if (beforeHP == player.GetHP()) return;

        Image[] icons = transform.GetComponentsInChildren<Image>();
        for(int i =0;i < icons.Length; i++)
        {
            icons[i].gameObject.SetActive(i < player.GetHP());
            //life = false;

            //i�̔ԍ��ɉ����āA���ԂɃn�[�g���o���Ă���
        }
        beforeHP = player.GetHP();
    }
    
    /*/
}
