using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        CEnemy = enemy;
        enemy.Anim.SetBool("isWalk", true);
        enemy.waitTimeCount = enemy.waitTime;
    }

    public override void LogicUpdate()
    {
        //发现敌人就追击
        if (CEnemy.LookedPlayer())
        {
            CEnemy.CurrentState.OnExit();
            CEnemy.CurrentState = CEnemy.ChaseState;
            CEnemy.CurrentState.OnEnter(CEnemy);
        }

        //野猪巡逻逻辑
        if (CEnemy.isWait)
        {
            CEnemy.waitTimeCount -= Time.deltaTime;
            if (CEnemy.waitTimeCount <= 0)
            {
                CEnemy.isWait = false;
                CEnemy.transform.localScale = new Vector3(-CEnemy.transform.localScale.x, 1, 1);
                CEnemy.Anim.SetBool("isWalk", true);
                CEnemy.waitTimeCount = CEnemy.waitTime;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        if (!CEnemy.isHurt && !CEnemy.isDead && !CEnemy.isWait)
            Move();
    }

    public void Move()
    {
        var localScaleX = CEnemy.transform.localScale.x;
        CEnemy.Rb.linearVelocityX = -localScaleX * CEnemy.normalSpeed * Time.deltaTime;
        if (!CEnemy.PhysicsCheck.isGrounded || (CEnemy.PhysicsCheck._nearRightWall && localScaleX == -1f) ||
            (CEnemy.PhysicsCheck._nearLeftWall && localScaleX == 1f))
        {
            CEnemy.isWait = true;
            CEnemy.Anim.SetBool("isWalk", false); 
        }
    }

    public override void OnExit()
    {
        CEnemy.Anim.SetBool("isWalk", false);
    }
}