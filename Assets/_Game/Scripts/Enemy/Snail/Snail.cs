using UnityEngine;

public class Snail : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        PatrolState= new SnailPatrolState();
    }
}
