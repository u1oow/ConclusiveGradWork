using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LifeControl : MonoBehaviour
{
    [SerializeField, Header("HPアイコン")]
    #region//プライベート変数
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
        //アクティブ非アクティブではなく、アニメーションによるライフのテクスチャの切り替えを試みている（いったん保留）
    }

    /// <summary>
    /// プレイヤーHPに応じてライフアイコンを設置する
    /// </summary>
    /// <returns>ライフ接置</returns>
    private void CreateHPIcon()
    {
        for(int i = 0;i < player.GetHP(); i++)
        {
            GameObject playerHPObj = Instantiate(playerIcon);
            playerHPObj.transform.parent = transform;
            //生成したHPアイコンの親オブジェクトにプレイヤーHPが入っているゲームオブジェクトを指定します
            //この時点でハートのクローンの複製をすることが可能
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
    /// プレイヤーHPに応じたライフを表示する
    /// </summary>
    /// <returns>ライフ管理</returns>
    
    /*/
    private void ShowHPIcon()
    {
        if (beforeHP == player.GetHP()) return;

        Image[] icons = transform.GetComponentsInChildren<Image>();
        for(int i =0;i < icons.Length; i++)
        {
            icons[i].gameObject.SetActive(i < player.GetHP());
            //life = false;

            //iの番号に応じて、順番にハートを出してくる
        }
        beforeHP = player.GetHP();
    }
    /*/
    
}
