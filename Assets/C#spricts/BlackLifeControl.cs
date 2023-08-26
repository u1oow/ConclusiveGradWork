using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackLifeControl : MonoBehaviour
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
        for (int i = 0; i < player.GetHP(); i++)
        {
            GameObject playerHPObj = Instantiate(playerIcon);
            playerHPObj.transform.parent = transform;
            //生成したHPアイコンの親オブジェクトにプレイヤーHPが入っているゲームオブジェクトを指定します
            //この時点でハートのクローンの複製をすることが可能
        }
    }
}
