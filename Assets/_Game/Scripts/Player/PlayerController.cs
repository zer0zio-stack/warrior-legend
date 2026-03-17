using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Vector2 inputDirection;
    public float speed = 200f;
    public float jumpForce = 36f;
    public bool isCrouch;

    public bool isHurt;
    public float hurtForce;
    public bool isDead;

    public bool isAttack;

    public bool isSlide;
    public float slideDistance;
    public float slideSpeed;

    public float JumpWallForce;

    public PhysicsMaterial2D _material2DNormal;
    public PhysicsMaterial2D _material2DWall;
    private PlayerAnimatiorController _animator;
    private CapsuleCollider2D _collider;

    private bool _isJumpingToWall;

    private PhysicsCheck _physicsCheck;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Rigidbody2D _rb;
    private SpriteRenderer _renderer;
    public InputSystemActions InputController;
    private Vector2 offsetOriginal;
    private float runSpeed;
    private Vector2 SizeOriginal;
    private float walkSpeed;

    private void Awake()
    {
        InputController = new InputSystemActions();
        _rb = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
        _physicsCheck = GetComponent<PhysicsCheck>();
        _animator = GetComponent<PlayerAnimatiorController>();
        InputController.Playing.Jump.started += Jump;
        InputController.Playing.Attack.started += Attack;
        InputController.Playing.Slide.started += Slide;
        runSpeed = speed;
        walkSpeed = speed / 2.5f;

        InputController.Playing.OnWalk.performed += ctx =>
        {
            if (_physicsCheck.isGrounded) speed = walkSpeed;
        };
        InputController.Playing.OnWalk.canceled += ctx =>
        {
            if (_physicsCheck.isGrounded) speed = runSpeed;
        };
        _collider = GetComponent<CapsuleCollider2D>();
        offsetOriginal = _collider.offset;
        SizeOriginal = _collider.size;
        isHurt = false;
        isDead = false;
    }

    // Update is called once per frame
    private void Update()
    {
        inputDirection = InputController.Playing.Move.ReadValue<Vector2>();
        isCrouch = inputDirection.y < -0.5f && _physicsCheck.isGrounded;
        if (isCrouch)
        {
            _rb.linearVelocityX = 0;
            _collider.size = new Vector2(0.9f, 1);
            _collider.offset = new Vector2(-0.1f, 0.55f);
        }
        else
        {
            _collider.size = SizeOriginal;
            _collider.offset = offsetOriginal;
        }

        //滑墙：这里直接调速度，要是变摩擦力就会黏住
        if (_physicsCheck.onWall) _rb.linearVelocityY = _rb.linearVelocityY / 3;
        if (_isJumpingToWall && _rb.linearVelocityY < 0) _isJumpingToWall = false;

        switchMaterial();
    }

    private void FixedUpdate()
    {
        if (!isHurt && !isAttack)
            move();
    }

    private void OnEnable()
    {
        InputController.Enable();
    }

    private void OnDisable()
    {
        InputController.Disable();
    }

    private void Slide(InputAction.CallbackContext obj)
    {
        if (!isSlide)
        {
            isSlide = true;
            var targetPoint = new Vector2(transform.position.x + transform.localScale.x * slideDistance,
                transform.position.y);
            gameObject.layer = LayerMask.NameToLayer("Ignore Raycast");
            StartCoroutine(SlideCoroutine(targetPoint));
        }
    }

    private IEnumerator SlideCoroutine(Vector2 targetPoint)
    {
        do
        {
            yield return null;
            if (_physicsCheck.isFaceCliff) break;

            if ((_physicsCheck._nearLeftWall && transform.localScale.x < 0) ||
                (_physicsCheck._nearRightWall && transform.localScale.x > 0))
                break;

            _rb.MovePosition(new Vector2(transform.position.x + transform.localScale.x * slideSpeed,
                transform.position.y));
        } while (MathF.Abs(targetPoint.x - transform.position.x) > 0.1f);
        isSlide = false;
        gameObject.layer = LayerMask.NameToLayer("player");
    }

    private void switchMaterial()
    {
        _collider.sharedMaterial = _physicsCheck.isGrounded ? _material2DNormal : _material2DWall;
    }

    private void Attack(InputAction.CallbackContext obj)
    {
        isAttack = true;
        _animator.PlayAttack();
    }


    private string Jump()
    {
        throw new NotImplementedException();
    }

    private void move()
    {
        if (!isCrouch && !_isJumpingToWall)
            _rb.linearVelocityX = inputDirection.x * speed * Time.deltaTime;


        //int faceDir=inputDirection.x>=0?1:-1;
        //transform.localScale = new Vector3(faceDir,1,1);
        if (_rb.linearVelocityX < 0)
            transform.localScale = new Vector3(-1, 1, 1);
        else if (_rb.linearVelocityX > 0)
            transform.localScale = new Vector3(1, 1, 1);
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (isSlide)
        {
            StopAllCoroutines();
            isSlide = false;
        }

        if (_physicsCheck.isGrounded) _rb.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        if (_physicsCheck.onWall)
        {
            _rb.AddForce(new Vector2(-inputDirection.x, 4.5f) * JumpWallForce, ForceMode2D.Impulse);
            _isJumpingToWall = true;
        }
    }

    public void getHurt(Transform attacker)
    {
        isHurt = true;
        //硬直,消除惯性
        _rb.linearVelocity = Vector2.zero;
        //添加瞬时力，模仿被推开
        var dric = new Vector2(transform.position.x - attacker.position.x, 0).normalized;
        _rb.AddForce(dric * hurtForce, ForceMode2D.Impulse);
    }

    public void Dead()
    {
        isDead = true;
        InputController.Playing.Disable();
    }
}