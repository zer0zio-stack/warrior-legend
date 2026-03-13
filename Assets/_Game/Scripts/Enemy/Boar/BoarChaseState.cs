using UnityEngine;

public class BoarChaseState : BaseState
{
    
    public override void OnEnter(Enemy enemy)
    {
        CEnemy = enemy;
        CEnemy.Anim.SetBool("isRun", true);
    }
    
    public override void LogicUpdate()
    {
        //撞墙立刻回头
        var localScaleX = CEnemy.transform.localScale.x;
        if (!CEnemy.PhysicsCheck.isGrounded || (CEnemy.PhysicsCheck._nearRightWall && localScaleX == -1f) ||
            (CEnemy.PhysicsCheck._nearLeftWall && localScaleX == 1f))
            CEnemy.transform.localScale = new Vector3(-CEnemy.transform.localScale.x, 1, 1);
        //TOdo
        //检测是否丢失目标
        if (!CEnemy.LookedPlayer())
        {
            CEnemy.missTimeCount += Time.deltaTime;
            //丢失目标超过 2 秒就退出追击状态
            if (CEnemy.missTimeCount >= CEnemy.missTime)
            {
                //退出
                OnExit();
                CEnemy.CurrentState = CEnemy.PatrolState;
                CEnemy.CurrentState.OnEnter(CEnemy);
            }
        }
        else
        {
            //重新发现目标，重置计时器
            CEnemy.missTimeCount = 0f;
        }
    }

    public override void PhysicsUpdate()
    {
        CEnemy.Rb.linearVelocityX = CEnemy.runSpeed * Time.deltaTime * -CEnemy.transform.localScale.x;
    }

    public override void OnExit()
    {
        CEnemy.Anim.SetBool("isRun", false);
    }
}