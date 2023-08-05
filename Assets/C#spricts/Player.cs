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
    [Header("踏みつけ判定の高さの割合")]public float stepOnRate;//
    [Header("接地しているか")] public GroundCheck ground;//
    [Header("頭をぶつけた判定")] public GroundCheck head;//
    [Header("目の前に障害物があるかの判定")] public GroundCheck crash;//
    [Header("重力")] public float gravity;//
    [Header("ダッシュの加速表現")] public AnimationCurve dashCurve;
    [Header("ジャンプの加速表現")] public AnimationCurve jumpCurve;
    #endregion

    #region//プライベート変数
    private Animator anim = null;
    private Rigidbody2D rb = null;
    private CapsuleCollider2D capcol = null;
    private bool isGround = false;
    private bool isHead = false;
    private bool isCrash = false;
    private bool isJump = false;
    private bool isRun = false;
    private bool isDown = false;
    private bool isOtherJump = false;
    private float jumpPos = 0.0f;
    private float otherJumpHeight = 0.0f;
    private float jumpTime = 0.0f;
    private float dashTime = 0.0f;
    private string enemyTag = "Enemy";
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capcol = GetComponent<CapsuleCollider2D>();
        Debug.Log("Start");
    }

    // Update is caled once per frame
    void FixedUpdate()
    {
        if (!isDown)
        {
            //アニメーションを適用
            SetAnimation();

            //接地判定を受け取る
            isGround = ground.IsGround();
            isHead = head.IsGround();
            isCrash = crash.IsGround();

            float ySpeed = GetYSpeed();
            float xSpeed = GetXSpeed();

            rb.velocity = new Vector2(xSpeed, ySpeed);
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


    #region//接触判定
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == enemyTag)
        {
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
                    //ここにダメージを食らった時の処理（アニメーション）を書いてください
                    //もしライフが０だったときは↓、そうでないときはライフ残数を減らすように
                    isDown = true;
                    Debug.Log("ダウン状態だよ！");
                    break;
                }
            
            }    
            Debug.Log("敵と接触した");

        }
    }
    #endregion


    public int GetHP()
    {
        return hp ;
    }
}
