using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/CharacterEventSO")]
public class CharactorEventSO : ScriptableObject
{
    public UnityAction<Charactor> CharacterEventAction;

    public void Raise(Charactor c)
    {
        CharacterEventAction?.Invoke(c);
    }
}