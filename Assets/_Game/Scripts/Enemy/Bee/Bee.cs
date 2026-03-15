using UnityEngine;

public class Bee : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        PatrolState = new BeePatrolState();
        ChaseState = new BeeChaseState();
    }

    public override void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, lookRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(originalPoint, patrolRadius);
    }

    public override bool LookedPlayer()
    {
        var obj = Physics2D.OverlapCircle(transform.position, lookRadius, enemyLayer);
        if (obj) attackerTransform = obj.transform;
        return obj;
    }
}