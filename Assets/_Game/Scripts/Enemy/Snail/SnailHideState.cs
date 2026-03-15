using UnityEngine;

public class SnailHideState : BaseState
{
    public override void OnEnter(Enemy enemy)
    {
        CEnemy = enemy;
        enemy.Anim.SetTrigger("Skill");
        enemy.Anim.SetBool("isHide", true);
        enemy.Charactor._isInvincible = true;
        enemy.missTimeCount = enemy.missTime;
    }

    public override void LogicUpdate()
    {
        if (CEnemy.LookedPlayer())
        {
            CEnemy.missTimeCount = CEnemy.missTime;
        }
        else
        {
            CEnemy.missTimeCount -= Time.deltaTime;
            if (CEnemy.missTimeCount <= 0)
            {
                OnExit();
                CEnemy.CurrentState = CEnemy.PatrolState;
                CEnemy.CurrentState.OnEnter(CEnemy);
            }
        }
    }

    public override void PhysicsUpdate()
    {
    }

    public override void OnExit()
    {
        CEnemy.Anim.SetBool("isHide", false);
        CEnemy.Charactor._isInvincible = false;
    }
}