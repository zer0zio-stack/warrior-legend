using UnityEngine;

public class BeeChaseState : BaseState
{
    private bool _isAttacking;
    private Vector2 _targetDir;
    private Vector2 _targetPos;
    public Attack Attack;
    private float _frequencyCount;

    public override void OnEnter(Enemy enemy)
    {
        CEnemy = enemy;
        Attack = CEnemy.GetComponent<Attack>();
        _frequencyCount = Attack.frequency;
        CEnemy.Anim.SetBool("isChase", true);
    }

    public override void LogicUpdate()
    {
        _MissTimeCounter();
        if (CEnemy.missTimeCount <= 0) CEnemy.SwitchState(Enemy.State.Patrol);
        _targetPos = new Vector2(CEnemy.attackerTransform.position.x,
            CEnemy.attackerTransform.position.y + 1.8f);
        _frequencyCount-=Time.deltaTime;
        if (Mathf.Abs(_targetPos.x - CEnemy.transform.position.x) <= Attack.range &&
            Mathf.Abs(_targetPos.y - CEnemy.transform.position.y) <= Attack.range)
        {
            _isAttacking = true;
            CEnemy.Rb.linearVelocity = Vector2.zero;
            if (_frequencyCount <= 0)
            {
                //todo:播放动画
                CEnemy.Anim.SetTrigger("attack");
                _frequencyCount = Attack.frequency;
            }
        }
        else
        {
            _isAttacking = false;
        }

        _targetDir = (_targetPos - (Vector2)CEnemy.transform.position).normalized;
        if (_targetDir.x > 0) CEnemy.transform.localScale = new Vector3(-1.0f, 1.0f, 1.0f);
        if (_targetDir.x < 0) CEnemy.transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
    }

    private void _MissTimeCounter()
    {
        if (CEnemy.LookedPlayer())
            CEnemy.missTimeCount = CEnemy.missTime;
        else
            CEnemy.missTimeCount -= Time.deltaTime;
    }

    public override void PhysicsUpdate()
    {
        if (!CEnemy.isHurt && !CEnemy.isDead && !_isAttacking)
            CEnemy.Rb.linearVelocity = _targetDir * (CEnemy.normalSpeed * Time.deltaTime);
    }

    public override void OnExit()
    {
        CEnemy.Anim.SetBool("isChase", false);
    }
}