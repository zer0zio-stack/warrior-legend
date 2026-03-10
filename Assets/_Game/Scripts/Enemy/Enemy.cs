using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("参数")] public float normalSpeed = 100f;

    public float runSpeed = 260f;
    public float currentSpeed;
    public float hurtForce;

    [Header("计时")] public float waitTimeCount;

    public float waitTime;
    public bool isWait;
    public bool isHurt;

    [Header("组件")] public Transform _AttackTransform;
    
    protected Animator Anim;
    private PhysicsCheck PhysicsCheck;

    protected Rigidbody2D Rb;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        PhysicsCheck = GetComponent<PhysicsCheck>();
        currentSpeed = normalSpeed;
        waitTimeCount = waitTime;
    }

    private void Update()
    {
        Wait();
    }

    private void FixedUpdate()
    {
        if(!isHurt)
            Move();
    }

    public virtual void Move()
    {
        float localScaleX = transform.localScale.x;
        Rb.linearVelocityX = -localScaleX * normalSpeed * Time.deltaTime;                  
        if ((PhysicsCheck._nearRightWall && localScaleX==-1f) || (PhysicsCheck._nearLeftWall && localScaleX==1f)) 
            isWait = true;
    }

    private void Wait()
    {
        if (isWait)
        {
            waitTimeCount -= Time.deltaTime;
            if (waitTimeCount <= 0)
            {
                isWait = false;
                transform.localScale = new Vector3(-transform.localScale.x, 1, 1);
                waitTimeCount = waitTime;
            }
        }
    }

    public void OnHurt()
    {
        //转身
        transform.localScale = new Vector3(_AttackTransform.position.x > transform.position.x ? -1 : 1, 1, 1);
        //播放动画
        isHurt=true;
        Anim.SetTrigger("isHurt");
        //后退
        StartCoroutine(hurt(new Vector2(transform.localScale.x, 0)));
    }
    
    //协程，等待一段时间后恢复行走，要不然刚揣它一脚，还没开始飞呢就开始行走了。
    private IEnumerator hurt(Vector2 dir)
    {
        Rb.AddForce(dir * hurtForce, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
    }


}