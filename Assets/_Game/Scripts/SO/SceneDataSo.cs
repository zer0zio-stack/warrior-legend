using UnityEngine.AddressableAssets;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/LoadSceneDataSo")]
public class SceneDataSo : ScriptableObject
{
    public AssetReference SceneRef;
    public bool isAdditive;
}
