using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public bool isGrounded;
    public bool _nearLeftWall;
    public bool _nearRightWall;
    public float radius;
    public LayerMask groundLayer;
    public Vector2 offset;

    private CapsuleCollider2D _capsuleCollider2d;

    private void Awake()
    {
        _capsuleCollider2d = GetComponent<CapsuleCollider2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        Check();
    }

    //钩子函数，不需要主动调用
    private void OnDrawGizmosSelected()
    {
        _capsuleCollider2d = GetComponent<CapsuleCollider2D>();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + offset + new Vector2(-1.4f * transform.localScale.x, 0),
            radius);
        Gizmos.DrawWireSphere(
            (Vector2)transform.position + _capsuleCollider2d.offset +
            new Vector2(_capsuleCollider2d.size.x / 2 + radius, 0),
            radius);
        Gizmos.DrawWireSphere(
            (Vector2)transform.position + _capsuleCollider2d.offset +
            new Vector2(-_capsuleCollider2d.size.x / 2 - radius, 0),
            radius);
    }

    private void Check()
    {
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + offset + new Vector2(-1.4f * transform.localScale.x, 0), radius, groundLayer);
        _nearRightWall = Physics2D.OverlapCircle(
            (Vector2)transform.position + _capsuleCollider2d.offset +
            new Vector2(_capsuleCollider2d.size.x / 2 + radius, _capsuleCollider2d.size.y),
            radius, groundLayer);
        _nearLeftWall = Physics2D.OverlapCircle(
            (Vector2)transform.position + _capsuleCollider2d.offset +
            new Vector2(-_capsuleCollider2d.size.x / 2 - radius, _capsuleCollider2d.size.y),
            radius, groundLayer);
    }
}