using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LifeControl : MonoBehaviour
{
    [SerializeField, Header("HPアイコン")]
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
            //生成したHPアイコンの親オブジェクトにプレイヤーHPが入っているゲームオブジェクトに指定します
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
            //iの番号に応じて、順番にハートを出してくる
        }
        beforeHP = player.GetHP();
    }
}
