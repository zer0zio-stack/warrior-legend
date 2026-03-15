public class Boar : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        PatrolState = new BoarPatrolState();
        ChaseState = new BoarChaseState();
    }
}