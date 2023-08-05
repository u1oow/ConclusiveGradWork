using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LifeControl : MonoBehaviour
{
    [SerializeField, Header("HP�A�C�R��")]
    private GameObject playerIcon;
    private Player player;
    private int beforeHP;

    private Animator anim = null;
    private bool life = true;
    
    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();
        beforeHP = player.GetHP();
        CreateIcon();

        anim = GetComponent<Animator>();
        anim.SetBool("LifeOn", true);
    }

    private void CreateIcon()
    {
        for(int i = 0;i < player.GetHP(); i++)
        {
            GameObject playerHPObj = Instantiate(playerIcon);
            playerHPObj.transform.parent = transform;
            //��������HP�A�C�R���̐e�I�u�W�F�N�g�Ƀv���C���[HP�������Ă���Q�[���I�u�W�F�N�g�Ɏw�肵�܂�
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(life)
        {
            anim.SetBool("LifeOn",true );
        }
        else
        {
            anim.SetBool("LifeOn",false);
        }
        
    }
     
    private void ShowHPIcon()
    {
        if (beforeHP == player.GetHP()) return;

        Image[] icons = transform.GetComponentsInChildren<Image>();
        for(int i =0;i < icons.Length; i++)
        {
            icons[i].gameObject.SetActive(i < player.GetHP());
            //i�̔ԍ��ɉ����āA���ԂɃn�[�g���o���Ă���
        }
        beforeHP = player.GetHP();
    }
}
