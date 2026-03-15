using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public bool isGrounded;
    public bool _nearLeftWall;
    public bool _nearRightWall;
    public float radius;
    public LayerMask groundLayer;
    public Vector2 offset;
    //玩家补丁，因为玩家的朝向和敌人不同，所以方向iaGround的检测范围也要相反
    public bool isPlayer;

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
        if (!isPlayer)
        {
            Gizmos.DrawWireSphere((Vector2)transform.position + offset + new Vector2(-1.4f * transform.localScale.x, 0),radius);
        }
        else
        {
            Gizmos.DrawWireSphere((Vector2)transform.position + offset, radius);
        }
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
        if (!isPlayer)
        {
            isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + offset + new Vector2(-1.4f * transform.localScale.x, 0), radius, groundLayer);
        }
        else
        {
            isGrounded = Physics2D.OverlapCircle((Vector2)transform.position + offset, radius, groundLayer);
        }
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