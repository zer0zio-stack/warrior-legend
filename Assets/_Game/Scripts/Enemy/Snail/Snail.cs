using System.Collections;
using UnityEngine;

public class Snail : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        PatrolState = new SnailPatrolState();
        HideState = new SnailHideState();
    }

    public override void OnHurt(Transform attackTransform)
    {
        StartCoroutine(hurtIEnumerator(new Vector2(-transform.localScale.x,0), attackTransform));
    }

    private IEnumerator hurtIEnumerator(Vector2 dir, Transform attackTransform)
    {
        isHurt = true;
        Anim.SetTrigger("isHurt");
        yield return new WaitForSeconds(0.5f);
        isHurt = false;
        Rb.AddForce(dir*hurtForce, ForceMode2D.Impulse);
        transform.localScale=new Vector3(attackTransform.position.x > transform.position.x ? -1 : 1, 1, 1);

    }
}