using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/LoadSceneEventSo")]
public class LoadSceneEventSo:ScriptableObject
{
    public UnityAction<SceneDataSo> SceneLoadedAction;

    public void Raise(SceneDataSo sceneData)
    {
        SceneLoadedAction?.Invoke(sceneData);
    }

}
