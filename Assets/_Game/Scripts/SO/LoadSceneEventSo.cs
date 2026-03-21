using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Event/LoadSceneEventSo")]
public class LoadSceneEventSo:ScriptableObject
{
    public UnityAction<SceneDataSo,Vector3,bool> SceneLoadedAction;

    public void Raise(SceneDataSo sceneData,Vector3 position,bool isFadeOut)
    {
        SceneLoadedAction?.Invoke(sceneData,position,isFadeOut);
    }

}
