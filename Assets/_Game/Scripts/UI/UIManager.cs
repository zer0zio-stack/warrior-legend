using UnityEngine;

public class UIManager : MonoBehaviour
{
    public CharactorEventSO CharactorEvent;
    public PlayerStateBar playerStateBar;

    private void OnEnable()
    {
        CharactorEvent.CharacterEventAction += _UpdateHealth;
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