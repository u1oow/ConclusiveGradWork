using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region//インスペクターで設定
    [Header("速度")] public float speed;//
    [Header("ダッシュ中の加速")] public float dashSpeed;//
    [Header("ジャンプ速度")] public float jumpSpeed;//
    [Header("ジャンプの高さ制限")] public float jumpHeight;//
    [Header("ジャンプ制限時間")] public float jumpLimitTime;//
    [Header("踏みつけ判定の高さの割合")] public float stepOnRate;//
    //[Header("プレイヤーHP")] public int hp = 5;
    [Header("接地しているか")] public GroundCheck ground;//
    [Header("頭をぶつけた判定")] public GroundCheck head;//
    [Header("目の前に障害物があるかの判定")] public GroundCheck crash;//
    [Header("重力")] public float gravity;//
    [Header("ダッシュの加速表現")] public AnimationCurve dashCurve;
    [Header("ジャンプの加速表現")] public AnimationCurve jumpCurve;
    public bool attackedEnemy = false;
    [Header("コースアウトしたかどうか")] public bool caurseOut;
    #endregion

    #region//プライベート変数
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private CapsuleCollider2D capcol = null;
    private SpriteRenderer sr = null;
    private bool isGround = false;
    private bool isHead = false;
    private bool isCrash = false;
    private bool isJump = false;
    private bool isRun = false;
    //private bool isDown = false;
    private bool isOtherJump = false;
    private bool alreadyDamagedFall = false;//落下からリスポーンまでの間にダメージを食らったか
    private bool WheatherAttackedEnemy = false;
    private bool isContinue = false;
    private float continueTime = 0.0f;
    private float blinkTime = 0.0f;
    private float jumpPos = 0.0f;
    private float otherJumpHeight = 0.0f;
    private float jumpTime = 0.0f;
    private float dashTime = 0.0f;
    //private int getHp = 0;

    private string respawnTag = "RespawnPoint";
    private string enemyTag = "Enemy";
    #endregion


    //public GameObject respawnPoint;
    private Vector3 startTrans;
    //private bool fall = false;
    private GameObject respawnPointer;//プレイヤーオブジェクトを取得する準備
    RespawnPoint respawnScript;

    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.playerHp =GameManager.instance.defaultHeartNum;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capcol = GetComponent<CapsuleCollider2D>();
        sr = GetComponent<SpriteRenderer>();

        /*/
        respawnPointer = GameObject.Find("respawnPointer");
        respawnScript = respawnPointer.GetComponent<RespawnPoint>();
        startTrans = respawnScript.trans;
        /*/

        //なんだかこの代入がうまくいっていないのかな？
        //↳多分うまくいっている


        //trans = respawnPoint.transform.position;

        Debug.Log("Start");
        //caurseOut = false//コースアウトしたかどうかを判定する変数。本来はRespawnPointから代入されるが、なぜかできていない。
    }

    private void Update()
    {
        if (WheatherAttackedEnemy)
        {
            isContinue = true;
        }

        if (isContinue)
        {
            //明滅ついている時の戻る
            if (blinkTime > 0.2f)
            {
                sr.enabled = true;
                blinkTime = 0.0f;
            }
            //明滅消えている時
            else if (blinkTime > 0.1f)
            {
                sr.enabled = false;
            }
            //明滅ついている時
            else
            {
                sr.enabled = true;
            }

            if (continueTime > 1.0f)
            {
                isContinue = false;
                blinkTime = 0.0f;
                continueTime = 0.0f;
                sr.enabled = true;
            }
            else
            {
                blinkTime += Time.deltaTime;
                continueTime += Time.deltaTime;
            }
        }


    }
    void FixedUpdate()
    {
        if (!GameManager.instance.isGameOver)
        {
            
             //GameManager.instance.playerHp += getHp;
            
            //アニメーションを適用
            SetAnimation();

            //接地判定を受け取る
            isGround = ground.IsGround();
            isHead = head.IsGround();
            isCrash = crash.IsGround();

            float ySpeed = GetYSpeed();
            float xSpeed = GetXSpeed();

            rb.velocity = new Vector2(xSpeed, ySpeed);

            //Debug.Log(attackedEnemy);
            attackedEnemy = WheatherAttackedEnemy;//今現在攻撃されているか
            WheatherAttackedEnemy = false;//一旦攻撃されているかどうかリセット

            //Debug.Log(ySpeed);
            //Debug.Log(xSpeed);
        }
        else
        {
            rb.velocity = new Vector2(0, -gravity); 
        }


    }
    
    /// <summary>
    /// Y成分で必要な計算をし、速度を返す
    /// </summary>
    /// <returns>Y軸の速さ</returns>
    private float GetYSpeed()
    {
        float ySpeed = -gravity;
        float VerticalKey = Input.GetAxis("Vertical");
        //上ボタンが押されているときジャンプモーションを適用

        if (isOtherJump)
        {
            //現在の高さが飛べる高さより下か
            bool canHeight = jumpPos + otherJumpHeight > transform.position.y;
            //ジャンプ時間が長くなりすぎてないか
            bool canTime = jumpLimitTime > jumpTime;

            if (canTime && canHeight && !isHead)
            {   
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                //Debug.Log("ジャンピングできません");
                isOtherJump = false;
                jumpTime = 0;
            }

        }
        else if (isJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }
        else
        {
            //Debug.Log("現在地面判定にあります");
            jumpTime = 0.0f;
        }
        if (isGround)
        {
            if (VerticalKey > 0)
            {
                isRun = false;
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y;//ジャンプした位置を記録する
                isJump = true;
                jumpTime = 0.0f;
            }
            else
            {
                dashTime += Time.deltaTime;
                isRun = true;
                isJump = false;
            }
        }
        else if (isJump)
        {
            //上方向キーをおしているか
            bool pushUpKey = VerticalKey > 0;
            //現在の高さが飛べる高さより下か
            bool canHeight = jumpPos + jumpHeight > transform.position.y;
            //ジャンプ時間が長くなりすぎてないか
            bool canTime = jumpLimitTime > jumpTime;

            if (canTime && canHeight && !isHead)
            {
                if (pushUpKey)
                {
                    ySpeed = jumpSpeed;
                    jumpTime += Time.deltaTime;
                }
                else if( jumpTime < 0.58 )
                {
                    ySpeed = jumpSpeed;
                    jumpTime += Time.deltaTime;
                }
                else
                {
                    Debug.Log("ジャンピングできません");
                    isJump = false;
                }
            }
            else
            {
                //Debug.Log("ジャンピングできません");
                isJump = false;
            }

        }
        if (isJump || isOtherJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }
        else
        {
            //Debug.Log("現在地面判定にあります");
            jumpTime = 0.0f;
        }
        //Debug.Log(jumpTime);
        return ySpeed;
    }

    /// <summary>
    /// X成分で必要な計算をし、速度を返す
    /// </summary>
    /// <returns>X軸の速さ</returns>
    private float GetXSpeed()
    {
        float xSpeed = 0.0f;
        //アニメーションカーブを速度に適用
        xSpeed = speed;
        //Debug.Log(isGround);
        if (isGround &&! isCrash)
        {
            xSpeed += dashSpeed;
            //地面に設置しているとき、ダッシュ速度が増加する
            
        }
        else if (isCrash)
        {
            dashTime = 0.1f;
        }
        xSpeed *= dashCurve.Evaluate(dashTime);
        return xSpeed;
    }

　　/// <summary>
  /// アニメーションまとめ
  /// </summary>
    private void SetAnimation()
    {
        anim.SetBool("RunOrJump", isRun &&! isOtherJump);
        anim.SetBool("ground", isGround);
    }


    #region//敵との接触、落下した際の接触（似たもの同士まとめ）
    #region//敵との接触
    private void HitEnemy(GameObject Enemy)//副産物として、接触した敵と一度離れた状態にならないとダメージを再び食らわない仕様に
    { 
        WheatherAttackedEnemy = true;//敵にあたった
        //if (GetComponent<Enemy>())
        //{
            Enemy.GetComponent<Enemy>().PlayerDamage(this);
        //}
        //if (GetComponent<EnemyRay>())
        //{
            Enemy.GetComponent<EnemyRay>().ResearchWhoAttacked(this);
        //}

        //ゲットコンポーネントで（落下等で求めたコンポーネントがないとき）Nullreferenceとかいうエラーが発生してしまう。

        //敵に当たったらその敵にあたったことを通知

        IsPlayerDown();

    }
    #endregion

    #region//落下した際の接触
    private void HitEnemyFall(GameObject Enemy)//副産物として、接触した敵と一度離れた状態にならないとダメージを再び食らわない仕様に
    {
        alreadyDamagedFall = true;
        WheatherAttackedEnemy = true;//敵にあたった
        Debug.Log(GetComponent<Enemy>());
        //if (GetComponent<Enemy>())
        //{
        Enemy.GetComponent<Enemy>().PlayerDamage(this);
        //}
        Enemy.GetComponent<RespawnPoint>().FallPlayerWarper(this);

        //思考したこと：ゲットコンポーネントで（落下等で求めたコンポーネントがないとき）Nullreferenceとかいうエラーが発生してしまう。
        //→ゲームオブジェクトを宣言して、そこでアタッチすれば案外簡単に行けそうだけどね。

        IsPlayerDown();
    }
    #endregion
    private void IsPlayerDown()
    {
        if (GameManager.instance.playerHp <= 0)//プレイヤーがダウン判定になるのは、敵からダメージを受けた直後のみ。
        {
            GameManager.instance.isGameOver = true;
            Debug.Log("ダウン状態だよ！");
        }
    }
    #endregion

    #region//接触判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (alreadyDamagedFall && isGround)
        {
            alreadyDamagedFall = false;//地面に触れ（落下から復帰し）たらまた落下ダメージを受けるようにする
        }
         
        if (collision.collider.tag == respawnTag)
        {
            //Debug.Log("落下ダメージ受けたみたい");

            foreach (ContactPoint2D p in collision.contacts)
            {
                Debug.Log("落下ダメージ受けたみたい");
                if (!alreadyDamagedFall)//2重で落下ダメを食らうバグがあった。→リスポーンまでに1回だけダメージ食らうように
                {
                    HitEnemyFall(collision.gameObject);//落下した際に、ダメージ判定を呼ぶ
                }

                //ここにダメージを食らった時の処理（アニメーション）を書く
                break;
            }


        }
        else if (collision.collider.tag == enemyTag)
        {
            Debug.Log("敵に攻撃されたみたい");
            //踏みつけ判定になる高さ
            float stepOnHeight = (capcol.size.y * (stepOnRate / 100f));

            //踏みつけ判定のワールド座標
            float judgePos = transform.position.y - (capcol.size.y / 2f) + stepOnHeight;

            foreach (ContactPoint2D p in collision.contacts)
            {
                if (p.point.y < judgePos)
                {
                    //もう一度はねる
                    ObjectCollision o = collision.gameObject.GetComponent<ObjectCollision> (); 
                    if(o != null) 
                    {
                        otherJumpHeight = o.boundHeight;//踏んづけたものからはねる高さを取得する
                        o.playerStepOn = true;//踏んづけたものに対して踏んづけたかどうか？
                        jumpPos = transform.position.y;//ジャンプした位置を記録する
                        isOtherJump = true;
                        isJump = false;
                        jumpTime = 0.0f;
                    }
                }
                else
                {
                    HitEnemy(collision.gameObject);//敵と接触した際に、ダメージ判定を呼ぶ

                    //ここにダメージを食らった時の処理（アニメーション）を書く
                    break;
                }
            
            }    
            

        }
        
    }
    #endregion

    /// <summary>
    /// プレイヤーへのダメージで必要な計算をし、速度を返す
    /// </summary>
    /// <returns>プレイヤーダメージ</returns>
     
    public void Damage(int damage)
    {
        GameManager.instance.playerHp = Mathf.Max(GameManager.instance.playerHp - damage, 0);
        //接触してきた敵から攻撃力を受け取る
    }


    /// <summary>
    /// プレイヤーのHPで必要な計算をし、値を返す
    /// </summary>
    /// <returns>プレーヤーHP</returns>

    /*/
    void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == respawnTag)
        {
            Debug.Log("落下してしまいました");
            Debug.Log(startTrans);
            this.gameObject.transform.position = startTrans;


        }
            
    }
    /*/
}