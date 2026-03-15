public class Bee : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        PatrolState = new BeePatrolState();
    }
}