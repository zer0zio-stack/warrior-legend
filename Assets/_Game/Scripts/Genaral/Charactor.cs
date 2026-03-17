using UnityEngine;
using UnityEngine.Events;

public class Charactor : MonoBehaviour
{
    public int maxHealth;

    public int currentHealth;

    public float maxPower;
    public float currentPower;
    public float recoverSpeed;

    //无敌时间最大
    public float _invincibleTimeMax = 1f;

    //是否死亡
    public bool isDie;

    public UnityEvent<Transform> HurtEvent;

    public UnityEvent OnDeadEvent;

    public UnityEvent<Charactor> OnChangeHealthEvent;

    //是否无敌
    public bool _isInvincible;

    //无敌时间
    private float _invincibleTime;

    public void Start()
    {
        //初始化
        currentHealth = maxHealth;
        currentPower = maxPower;
        _isInvincible = false;
        _invincibleTime = _invincibleTimeMax;
        isDie = false;
        OnChangeHealthEvent?.Invoke(this);
    }

    private void Update()
    {
        //计时，更新无敌时间状态
        if (_isInvincible)
        {
            _invincibleTime -= Time.deltaTime;
            if (_invincibleTime <= 0)
                _isInvincible = false;
        }

        //恢复power
        if (currentPower <= maxPower)
        {
            currentPower += Time.deltaTime * recoverSpeed;
        }
    }

    public void TakeDamage(Attack attack)
    {
        //活着才接受伤害
        if (!isDie)
        {
            if (_isInvincible)
                return;
            if (currentHealth > attack.damage)
            {
                currentHealth -= attack.damage;
                _isInvincible = true;
                _invincibleTime = _invincibleTimeMax;
                HurtEvent?.Invoke(attack.transform);
            }
            else
            {
                currentHealth = 0;
                isDie = true;
                OnDeadEvent?.Invoke();
            }

            OnChangeHealthEvent?.Invoke(this);
        }
    }
}