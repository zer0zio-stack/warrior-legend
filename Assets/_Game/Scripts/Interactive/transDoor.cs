using UnityEngine;

public class transDoor : MonoBehaviour,IInteractive
{
    public Vector3 targetPos;

    public void Interactive()
    {
        Debug.Log("传送");
    }
}
