using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("速度")] public float normalSpeed = 100f;

    public float runSpeed = 260f;
    public float currentSpeed;

    [Header("击退力量")] public float hurtForce;

    [Header("障碍等待时间")] public float waitTime;

    [HideInInspector] public float waitTimeCount;

    [Header("状态")] public bool isWait;

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
        currentSpeed = normalSpeed;
        waitTimeCount = waitTime;
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

    public void OnDie()
    {
        //这里把野猪流放到第二图层，第二图层已经被禁止和palyer层的碰撞
        gameObject.layer = 2;

        isDead = true;
        Anim.SetBool("isDead", true);
    }

    public void DestroyOnAnimation()
    {
        Destroy(gameObject);
    }
}