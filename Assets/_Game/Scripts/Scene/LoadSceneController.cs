using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour
{
    public SceneDataSo firstScene;
    public LoadSceneEventSo loadSceneEventSo;
    private SceneDataSo _currentScene;

    private void OnEnable()
    {
        _currentScene = firstScene;
        _currentScene.SceneRef.LoadSceneAsync(firstScene.isAdditive?LoadSceneMode.Additive:LoadSceneMode.Single);
        loadSceneEventSo.SceneLoadedAction += LoadScene;
    }

    private void LoadScene(SceneDataSo scene)
    {
        StartCoroutine(LoadSceneCoroutine(scene));
    }

    private IEnumerator LoadSceneCoroutine(SceneDataSo scene)
    {
        //todo:黑屏几秒
        yield return new WaitForSeconds(0.5f);
        
        yield return _currentScene.SceneRef.UnLoadScene();
        _currentScene = scene;
        _currentScene.SceneRef.LoadSceneAsync(_currentScene.isAdditive?LoadSceneMode.Additive:LoadSceneMode.Single);
    }

    private void OnDisable()
    {
        loadSceneEventSo.SceneLoadedAction -= LoadScene;
    }
}
