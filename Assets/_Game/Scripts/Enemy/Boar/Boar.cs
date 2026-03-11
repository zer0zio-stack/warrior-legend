public class Boar : Enemy
{
    protected override void Awake()
    {
        base.Awake();
        PatrolState = new BoarPatrolState();
    }
}