using UnityEngine;

public class BoarPatrolState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        CurrentEnemy = enemy;
        enemy.Anim.SetBool("isWalk", true);
    }

    public override void LogicUpdate()
    {
        //TODO:发现敌人就追击
        //野猪巡逻逻辑
        if (CurrentEnemy.isWait)
        {
            CurrentEnemy.waitTimeCount -= Time.deltaTime;
            if (CurrentEnemy.waitTimeCount <= 0)
            {
                CurrentEnemy.isWait = false;
                CurrentEnemy.transform.localScale = new Vector3(-CurrentEnemy.transform.localScale.x, 1, 1);
                CurrentEnemy.Anim.SetBool("isWalk", true);
                CurrentEnemy.waitTimeCount = CurrentEnemy.waitTime;
            }
        }
    }

    public override void PhysicsUpdate()
    {
        if (!CurrentEnemy.isHurt && !CurrentEnemy.isDead && !CurrentEnemy.isWait)
            Move();
    }

    public void Move()
    {
        var localScaleX = CurrentEnemy.transform.localScale.x;
        CurrentEnemy.Rb.linearVelocityX = -localScaleX * CurrentEnemy.normalSpeed * Time.deltaTime;
        if (!CurrentEnemy.PhysicsCheck.isGrounded || (CurrentEnemy.PhysicsCheck._nearRightWall && localScaleX == -1f) ||
            (CurrentEnemy.PhysicsCheck._nearLeftWall && localScaleX == 1f))
        {
            CurrentEnemy.isWait = true;
            CurrentEnemy.Anim.SetBool("isWalk", false);
        }
    }

    public override void OnExit()
    {
    }
}