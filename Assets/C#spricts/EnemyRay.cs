using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRay : MonoBehaviour
{
    #region//インスペクターで設定する
    [Header("移動速度")] public float speed;
    [Header("重力")] public float gravity;
    [Header("画面外でも行動するか")] public bool nonVisible;
    #endregion

    #region//プライベート関数
    private Rigidbody2D rb = null;
    private bool rightTleftF = false;
    private SpriteRenderer sr = null;

    private bool isHit = false;//すでにプレイヤーにあったか
    private bool isDamaged = false;//現在プレイヤーにあたっているか
    private CircleCollider2D col = null;
    private ObjectCollision oc = null;

    private string PlayerTag = "Player";

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        oc = GetComponent<ObjectCollision>();
        col = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!isDamaged && !isHit)
        {
            if (sr.isVisible || nonVisible)
            {
                int xVector = -1;
                if (rightTleftF)
                {
                    xVector = 1;
                    transform.localScale = new Vector3(-1, 1, 1);//ボールの大きさが変更されたときはここをいじってね。
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1);//ボールの大きさが変更されたときはここをいじってね。
                }
                rb.velocity = new Vector2(xVector * speed, -gravity);
            }
            else
            {
                rb.Sleep();
            }
        }
        else if(!isHit)//もし初めてダメージを与えたら
        {
            isHit = true;
            isDamaged = false;
            col.enabled = false;
            //Debug.Log("コライダーの設定をなくしました");
            Destroy(gameObject,3f);//3秒後、このゲームオブジェクトを消す
        }
    }

    /*/
     * プレイヤーがボールに若干押し返されてしまう。できれば当たり判定のみをつけ、互いに物理的に干渉しないようにしたい
     * ↳「トリガーにする」を設定に追加すると何とかなった。
     * ↳そうしたら今度はHPの減少がなくなってしまった。色々試してみたけど、行き詰まり。
    /*/
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.tag);
        if (collision.tag == PlayerTag)
        {
            isDamaged = true;
            Debug.Log("プレイヤーと接触しました");
        }
    }
    
}
