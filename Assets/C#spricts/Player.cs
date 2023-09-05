using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region//�C���X�y�N�^�[�Őݒ�
    [Header("���x")] public float speed;//
    [Header("�_�b�V�����̉���")] public float dashSpeed;//
    [Header("�W�����v���x")] public float jumpSpeed;//
    [Header("�W�����v�̍�������")] public float jumpHeight;//
    [Header("�W�����v��������")] public float jumpLimitTime;//
    [Header("���݂�����̍����̊���")]public float stepOnRate;//
    [Header("�ڒn���Ă��邩")] public GroundCheck ground;//
    [Header("�����Ԃ�������")] public GroundCheck head;//
    [Header("�ڂ̑O�ɏ�Q�������邩�̔���")] public GroundCheck crash;//
    [Header("�d��")] public float gravity;//
    [Header("�_�b�V���̉����\��")] public AnimationCurve dashCurve;
    [Header("�W�����v�̉����\��")] public AnimationCurve jumpCurve;
    public bool attackedEnemy = false;
    [Header("�R�[�X�A�E�g�������ǂ���")] public bool caurseOut;
    #endregion

    #region//�v���C�x�[�g�ϐ�
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

    private bool WheatherAttackedEnemy = false;

    private float jumpPos = 0.0f;
    private float otherJumpHeight = 0.0f;
    private float jumpTime = 0.0f;
    private float dashTime = 0.0f;

    public int hp = 5;

    private string enemyTag = "Enemy";
    #endregion
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        capcol = GetComponent<CapsuleCollider2D>();
        Debug.Log("Start");
        //caurseOut = false//�R�[�X�A�E�g�������ǂ����𔻒肷��ϐ��B�{����RespawnPoint����������邪�A�Ȃ����ł��Ă��Ȃ��B
    }

    // Update is caled once per frame

    private void Update()
    {
        //Debug.Log(isCrash);
        //Debug.Log(hp);
        //Debug.Log(attackedEnemy);

    }
    void FixedUpdate()
    {
        if (!isDown)
        {
            //�A�j���[�V������K�p
            SetAnimation();

            //�ڒn������󂯎��
            isGround = ground.IsGround();
            isHead = head.IsGround();
            isCrash = crash.IsGround();

            float ySpeed = GetYSpeed();
            float xSpeed = GetXSpeed();

            rb.velocity = new Vector2(xSpeed, ySpeed);

            //Debug.Log(attackedEnemy);
            attackedEnemy = WheatherAttackedEnemy;//�����ݍU������Ă��邩
            WheatherAttackedEnemy = false;//��U�U������Ă��邩�ǂ������Z�b�g

            //Debug.Log(ySpeed);
            //Debug.Log(xSpeed);
        }
        else
        {
            rb.velocity = new Vector2(0, -gravity); 
        }


    }
    
    /// <summary>
    /// Y�����ŕK�v�Ȍv�Z�����A���x��Ԃ�
    /// </summary>
    /// <returns>Y���̑���</returns>
    private float GetYSpeed()
    {
        float ySpeed = -gravity;
        float VerticalKey = Input.GetAxis("Vertical");
        //��{�^����������Ă���Ƃ��W�����v���[�V������K�p

        if (isOtherJump)
        {
            //���݂̍�������ׂ鍂����艺��
            bool canHeight = jumpPos + otherJumpHeight > transform.position.y;
            //�W�����v���Ԃ������Ȃ肷���ĂȂ���
            bool canTime = jumpLimitTime > jumpTime;

            if (canTime && canHeight && !isHead)
            {   
                ySpeed = jumpSpeed;
                jumpTime += Time.deltaTime;
            }
            else
            {
                //Debug.Log("�W�����s���O�ł��܂���");
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
            //Debug.Log("���ݒn�ʔ���ɂ���܂�");
            jumpTime = 0.0f;
        }
        if (isGround)
        {
            if (VerticalKey > 0)
            {
                isRun = false;
                ySpeed = jumpSpeed;
                jumpPos = transform.position.y;//�W�����v�����ʒu���L�^����
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
            //������L�[�������Ă��邩
            bool pushUpKey = VerticalKey > 0;
            //���݂̍�������ׂ鍂����艺��
            bool canHeight = jumpPos + jumpHeight > transform.position.y;
            //�W�����v���Ԃ������Ȃ肷���ĂȂ���
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
                    Debug.Log("�W�����s���O�ł��܂���");
                    isJump = false;
                }
            }
            else
            {
                //Debug.Log("�W�����s���O�ł��܂���");
                isJump = false;
            }

        }
        if (isJump || isOtherJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }
        else
        {
            //Debug.Log("���ݒn�ʔ���ɂ���܂�");
            jumpTime = 0.0f;
        }
        //Debug.Log(jumpTime);
        return ySpeed;
    }

    /// <summary>
    /// X�����ŕK�v�Ȍv�Z�����A���x��Ԃ�
    /// </summary>
    /// <returns>X���̑���</returns>
    private float GetXSpeed()
    {
        float xSpeed = 0.0f;
        //�A�j���[�V�����J�[�u�𑬓x�ɓK�p
        xSpeed = speed;
        //Debug.Log(isGround);
        if (isGround &&! isCrash)
        {
            xSpeed += dashSpeed;
            //�n�ʂɐݒu���Ă���Ƃ��A�_�b�V�����x����������
            
        }
        else if (isCrash)
        {
            dashTime = 0.1f;
        }
        xSpeed *= dashCurve.Evaluate(dashTime);
        return xSpeed;
    }

�@�@/// <summary>
  /// �A�j���[�V�����܂Ƃ�
  /// </summary>
    private void SetAnimation()
    {
        anim.SetBool("RunOrJump", isRun &&! isOtherJump);
        anim.SetBool("ground", isGround);
    }


    #region//�G�Ƃ̐ڐG
    private void HitEnemy(GameObject Enemy)//���Y���Ƃ��āA�ڐG�����G�ƈ�x���ꂽ��ԂɂȂ�Ȃ��ƃ_���[�W���ĂѐH���Ȃ��d�l��
    { 
        WheatherAttackedEnemy = true;//�G�ɂ�������
            Debug.Log("�G�ɐڐG���܂���");
            Enemy.GetComponent<Enemy>().PlayerDamage(this);
        �@�@Enemy.GetComponent<EnemyRay>().ResearchWhoAttacked(this);
        //�G�ɓ��������炻�̓G�ɂ����������Ƃ�ʒm
    }
    #endregion


    #region//�ڐG����
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == enemyTag)
        {
            //���݂�����ɂȂ鍂��
            float stepOnHeight = (capcol.size.y * (stepOnRate / 100f));

            //���݂�����̃��[���h���W
            float judgePos = transform.position.y - (capcol.size.y / 2f) + stepOnHeight;

            foreach (ContactPoint2D p in collision.contacts)
            {
                if (p.point.y < judgePos)
                {
                    //������x�͂˂�
                    ObjectCollision o = collision.gameObject.GetComponent<ObjectCollision> (); 
                    if(o != null) 
                    {
                        otherJumpHeight = o.boundHeight;//����Â������̂���͂˂鍂�����擾����
                        o.playerStepOn = true;//����Â������̂ɑ΂��ē���Â������ǂ����H
                        jumpPos = transform.position.y;//�W�����v�����ʒu���L�^����
                        isOtherJump = true;
                        isJump = false;
                        jumpTime = 0.0f;
                    }
                }
                else
                {
                    HitEnemy(collision.gameObject);//�G�ƐڐG�����ۂɁA�_���[�W������Ă�
                    
                    if (hp <= 0)//�v���C���[���_�E������ɂȂ�̂́A�G����_���[�W���󂯂�����̂݁B
                    {
                        isDown = true;
                        Debug.Log("�_�E����Ԃ���I");
                    }

                    //�����Ƀ_���[�W��H��������̏����i�A�j���[�V�����j������
                    break;
                }
            
            }    
            

        }
    }
    #endregion

    /// <summary>
    /// �v���C���[�ւ̃_���[�W�ŕK�v�Ȍv�Z�����A���x��Ԃ�
    /// </summary>
    /// <returns>�v���C���[�_���[�W</returns>
     
    public void Damage(int damage)
    {
        hp = Mathf.Max(hp - damage, 0);
        //�ڐG���Ă����G����U���͂��󂯎��
    }


    /// <summary>
    /// �v���C���[��HP�ŕK�v�Ȍv�Z�����A�l��Ԃ�
    /// </summary>
    /// <returns>�v���[���[HP</returns>
    public int GetHP()
    {
        return hp ;
    }
}