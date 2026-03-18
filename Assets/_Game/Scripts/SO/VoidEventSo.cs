using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/VoidEventSO")]
public class VoidEventSo : ScriptableObject
{
    public UnityAction VoidEventAction;

    public void Raise()
    {
        VoidEventAction?.Invoke();
    }
}