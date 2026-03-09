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
    
    private void Awake()
    {
        Rb = GetComponent<Rigidbody2D>();
        Anim = GetComponent<Animator>();
        currentSpeed=normalSpeed;
    }

    private void FixedUpdate()
    {
        Move();
    }

    public virtual void Move()
    {
        Rb.linearVelocityX = -transform.localScale.x* normalSpeed * Time.deltaTime;
    }
}
