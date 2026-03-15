using UnityEngine;

public class BeePatrolState : BaseState
{
    private Vector2 _targetDir;
    private Vector2 _targetPos;

    public override void OnEnter(Enemy enemy)
    {
        CEnemy = enemy;
        _targetPos = CEnemy.GetRandomPoint();
    }

    public override void LogicUpdate()
    {
        //等待倒计时
        CEnemy.WaitTimeCount();
        //倒计时改变dir
        if (CEnemy.waitTimeCount < Time.deltaTime)
        {
            _targetPos = CEnemy.GetRandomPoint();
            CEnemy.isWait = false;
        }
        //巡逻
        if (CEnemy.LookedPlayer()) CEnemy.SwitchState(Enemy.State.Chase);
        //到达target等待
        if (Mathf.Abs(_targetPos.x - CEnemy.transform.position.x) <= 0.2f &&
            Mathf.Abs(_targetPos.y - CEnemy.transform.position.y) <= 0.2f)
            CEnemy.isWait = true;
        //速度方向
        _targetDir = (_targetPos - (Vector2)CEnemy.transform.position).normalized;
        //控制面朝向
        if (_targetDir.x > 0) CEnemy.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);

        if (_targetDir.x < 0) CEnemy.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    

    public override void PhysicsUpdate()
    {
        if (!CEnemy.isWait && !CEnemy.isHurt && !CEnemy.isDead)
        {
            CEnemy.Rb.linearVelocity = _targetDir * (CEnemy.normalSpeed * Time.deltaTime);
        }
        else
        {
            CEnemy.Rb.linearVelocity = Vector2.zero;
        }
    }

    public override void OnExit()
    {
    }
}