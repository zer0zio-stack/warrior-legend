using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("参数")]
    public float normalSpeed = 100f;

    public float runSpeed=260f;
    public float currentSpeed;
    

    protected Rigidbody2D Rb;
    protected Animator Anim;
    private PhysicsCheck PhysicsCheck;
    private SpriteRenderer _renderer;
    
    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        PhysicsCheck = GetComponent<PhysicsCheck>();
        _renderer = GetComponent<SpriteRenderer>();
        currentSpeed=normalSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        Rb.linearVelocityX = _renderer.flipX?1:-1* normalSpeed * Time.deltaTime;
        if ((PhysicsCheck._nearRightWall && _renderer.flipX) || (PhysicsCheck._nearLeftWall && !_renderer.flipX))
        {
               _renderer.flipX = !_renderer.flipX;
        }
    }
}
