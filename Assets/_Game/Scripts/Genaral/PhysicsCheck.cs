using UnityEngine;

public class PhysicsCheck : MonoBehaviour
{
    public bool isGrounded;
    public float radius;
    public LayerMask groundLayer;
    public Vector2 offset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    // Update is called once per frame
    void Update()
    {
        check();
    }

    void check()
    {
        isGrounded = Physics2D.OverlapCircle((Vector2)transform.position+offset, radius, groundLayer);
    }
    //钩子函数，不需要主动调用
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position+offset, radius);
    }
}
