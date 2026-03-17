using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CharactorEventSO CharactorEvent;
    public PlayerStateBar playerStateBar;

    private void OnEnable()
    {
        CharactorEvent.CharacterEventAction += _UpdateHealth;
        CharactorEvent.CharacterEventAction += _UpdatePower;
    }

    private void _UpdatePower(Charactor arg0)
    {
        playerStateBar.OnPowerBarChange(arg0.currentPower/arg0.maxPower);
    }

    private void OnDisable()
    {
        CharactorEvent.CharacterEventAction -= _UpdateHealth;
    }

    private void _UpdateHealth(Charactor arg0)
    {
        playerStateBar.OnHealthBarChange((float)arg0.currentHealth / arg0.maxHealth);
    }
}