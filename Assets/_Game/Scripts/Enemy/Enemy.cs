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

    [Header("组件")] public Transform _AttackTransform;

    private SpriteRenderer _renderer;
    protected Animator Anim;
    private PhysicsCheck PhysicsCheck;

    protected Rigidbody2D Rb;

    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        PhysicsCheck = GetComponent<PhysicsCheck>();
        _renderer = GetComponent<SpriteRenderer>();
        currentSpeed = normalSpeed;
        waitTimeCount = waitTime;
    }

    private void Update()
    {
        Wait();
    }

    private void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        Rb.linearVelocityX = _renderer.flipX ? 1 : -1 * normalSpeed * Time.deltaTime;
        if ((PhysicsCheck._nearRightWall && _renderer.flipX) ||
            (PhysicsCheck._nearLeftWall && !_renderer.flipX)) isWait = true;
    }

    private void Wait()
    {
        if (isWait)
        {
            waitTimeCount -= Time.deltaTime;
            if (waitTimeCount <= 0)
            {
                isWait = false;
                _renderer.flipX = !_renderer.flipX;
                waitTimeCount = waitTime;
            }
        }
    }

    public void OnHurt()
    {
        //转身
        _renderer.flipX = _AttackTransform.position.x > transform.position.x ? true : false;
        //播放动画
        Anim.SetTrigger("isHurt");
        //后退
        Rb.AddForce(new Vector2(_renderer.flipX ? 1 : -1, 0) * hurtForce, ForceMode2D.Impulse);
    }
}