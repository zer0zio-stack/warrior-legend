using UnityEngine;

public class transDoor : MonoBehaviour,IInteractive
{
    public Vector3 targetPos;
    public LoadSceneEventSo LoadSceneEventSo;
    public SceneDataSo TargetSceneData;

    public void Interactive()
    {
        LoadSceneEventSo.Raise(TargetSceneData);
    }
}
