using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("参数")] public float normalSpeed = 100f;

    public float runSpeed = 260f;
    public float currentSpeed;

    [Header("计时")] public float waitTimeCount;

    public float waitTime;
    public bool isWait;
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
        if ((PhysicsCheck._nearRightWall && _renderer.flipX) || (PhysicsCheck._nearLeftWall && !_renderer.flipX))
        {
            isWait = true;
        }
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
}