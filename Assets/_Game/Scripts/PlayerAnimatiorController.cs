using UnityEngine;

public class PlayerAnimatiorController : MonoBehaviour
{
    private static readonly int Velocity = Animator.StringToHash("Velocity");
    private static readonly int Velocityy = Animator.StringToHash("Velocityy");
    private static readonly int IsGround = Animator.StringToHash("isGround");
    private Animator _animator;
    private PhysicsCheck _physicsCheck;
    private PlayerController _playerController;

    private Rigidbody2D _rb;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _physicsCheck = GetComponent<PhysicsCheck>();
        _playerController = GetComponent<PlayerController>();
        _animator.ResetTrigger("Hurt");
        _animator.ResetTrigger("attackTrigger");
    }

    // Update is called once per frame
    private void Update()
    {
        _animator.SetFloat(Velocity, Mathf.Abs(_rb.linearVelocityX));
        _animator.SetFloat(Velocityy, _rb.linearVelocityY);
        _animator.SetBool(IsGround, _physicsCheck.isGrounded);
        _animator.SetBool("isCrouch", _playerController.isCrouch);
        _animator.SetBool("isDead", _playerController.isDead);
        _animator.SetBool("isAttack", _playerController.isAttack);
    }

    public void PlayHurt()
    {
        _animator.SetTrigger("Hurt");
    }

    public void PlayAttack()
    {
        _animator.SetTrigger("attackTrigger");
    }
}