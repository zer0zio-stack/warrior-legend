using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Rigidbody2D), typeof(PhysicsCheck))]
public class Enemy : MonoBehaviour
{
    [Header("速度")] public float normalSpeed = 100f;

    public float runSpeed = 260f;

    [Header("击退力量")] public float hurtForce;

    [Header("障碍等待时间")] public float waitTime;

    [HideInInspector] public float waitTimeCount;

    [Header("检测参数")] public Vector2 offset;

    public Vector2 size;
    public float distance;
    public LayerMask enemyLayer;

    [Header("miss时间")] public float missTime;

    [HideInInspector] public float missTimeCount;


    [Header("状态")] 
    public bool isWait;
    public bool isHurt;
    public bool isDead;

    [Header("组件")] [HideInInspector] public Transform _AttackTransform;

    [HideInInspector] public Animator Anim;
    [HideInInspector] public PhysicsCheck PhysicsCheck;
    [HideInInspector] public Rigidbody2D Rb;
    
    [HideInInspector] public BaseState ChaseState;
    [HideInInspector] public BaseState CurrentState;
    [HideInInspector] public BaseState PatrolState;

    protected virtual void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        PhysicsCheck = GetComponent<PhysicsCheck>();
    }

    private void Update()
    {
        CurrentState.LogicUpdate();
    }

    private void FixedUpdate()
    {
        CurrentState.PhysicsUpdate();
    }

    public void OnEnable()
    {
        CurrentState = PatrolState;
        CurrentState.OnEnter(this);
    }

    private void OnDisable()
    {
        CurrentState.OnExit();
    }

    //绘制检测敌人的范围
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blueViolet;
        Gizmos.DrawWireCube(transform.position + (Vector3)offset + new Vector3(-transform.localScale.x * distance, 0),
            size);
    }
    //检测敌人是否进入攻击视野
    public bool LookedPlayer()
    {
        return Physics2D.BoxCast(transform.position + (Vector3)offset, size, 0, new Vector2(-transform.localScale.x, 0),
            distance, enemyLayer);
    } 


    #region 受伤逻辑 

    public void OnHurt()
    {
        //转身
        transform.localScale = new Vector3(_AttackTransform.position.x > transform.position.x ? -1 : 1, 1, 1);
        //播放动画
        isHurt = true;
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

    #endregion



    #region die 逻辑

    public void OnDie()
    {
        //这里把野猪流放到第二图层，第二图层已经被禁止和palyer层的碰撞
        gameObject.layer = 2;
        isDead = true;
        Anim.SetBool("isDead", true);
    }

    //这个方法被死亡动画的结束帧事件调用
    public void DestroyOnAnimation()
    {
        Destroy(gameObject);
    }

    #endregion
    
}