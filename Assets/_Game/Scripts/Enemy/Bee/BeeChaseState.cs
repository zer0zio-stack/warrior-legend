using System;

public class BeeChaseState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        CEnemy = enemy;
        //Todo:完善追击状态
    }

    public override void LogicUpdate()
    {
        throw new NotImplementedException();
    }

    public override void PhysicsUpdate()
    {
        throw new NotImplementedException();
    }

    public override void OnExit()
    {
        throw new NotImplementedException();
    }
}