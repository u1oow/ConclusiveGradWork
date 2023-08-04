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
    private float jumpPos = 0.0f;
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
            //�A�j���[�V������K�p
            SetAnimation();

            //�ڒn������󂯎��
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
    /// Y�����ŕK�v�Ȍv�Z�����A���x��Ԃ�
    /// </summary>
    /// <returns>Y���̑���</returns>
    private float GetYSpeed()
    {
        float ySpeed = -gravity;
        float VerticalKey = Input.GetAxis("Vertical");
        //��{�^����������Ă���Ƃ��W�����v���[�V������K�p

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
        if (isJump)
        {
            ySpeed *= jumpCurve.Evaluate(jumpTime);
        }
        else
        {
            jumpTime = 0.0f;
        }
        Debug.Log(jumpTime);
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
        if (isGround)
        {
            xSpeed += dashSpeed; 
            //�n�ʂɐݒu���Ă���Ƃ��A�_�b�V�����x����������
        }
        xSpeed *= dashCurve.Evaluate(dashTime);
        if (isCrash)
        {
            dashTime = 0.1f;
        }
        return xSpeed;
    }

�@�@/// <summary>
  /// �A�j���[�V�����܂Ƃ�
  /// </summary>
    private void SetAnimation()
    {
        anim.SetBool("RunOrJump", isRun);
        anim.SetBool("ground", isGround);
    }

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
                /*/�S�T���W�G�O�O�̒n�_�Ŏ~�܂��Ă��邩��A�܂����x��낤�Ǝv��
                 * �Ƃ������A�P�P�F�Q�T����Ė������炨�x�݂��悤�Ǝv��
                 * ���Ⴀ�A��͂�낵���I�I�I�I�I�I�I�I�I�I�i���傢���˂̕����j
                /*/
                if (p.point.y < judgePos)
                {
                    //������x�͂˂�                    
                }
                else
                {
                    //�����Ƀ_���[�W��H��������̏����i�A�j���[�V�����j�������Ă�������
                    isDown = true;
                    Debug.Log("�_�E����Ԃ���I");
                }
            
            }    
            Debug.Log("�G�ƐڐG����");

        }
    }
    #endregion
}
