using UnityEngine;

public class AudioScript : MonoBehaviour
{
    public AudioClip clip;
    public AudioEventSo audioEvent;
    public bool isEnabled;

    private void OnEnable()
    {
        if (isEnabled)
            audioEvent.Raise(clip);
    }
}