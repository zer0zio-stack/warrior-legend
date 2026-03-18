using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/AudioEventSo")]
public class AudioEventSo : ScriptableObject
{
    public UnityAction<AudioClip> audioEventAction;

    public void Raise(AudioClip clip)
    {
        audioEventAction?.Invoke(clip);
    }
}