using System.Collections;
using UnityEngine;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;

public class LoadSceneController : MonoBehaviour
{
    public SceneDataSo firstScene;
    public LoadSceneEventSo loadSceneEventSo;
    public Transform playerTransform;
    public VoidEventSo afterLoadSceneEvent;
    public Vector3 startPosition;
    
    private SceneDataSo _currentScene;
    private SceneDataSo _targetScene;
    private bool _isFadeOut;
    private Vector3 _targetPosition;
    private bool _isLoading;

    private void OnEnable()
    {
        _currentScene = firstScene;
        _currentScene.SceneRef.LoadSceneAsync(firstScene.isAdditive?LoadSceneMode.Additive:LoadSceneMode.Single);
        
        loadSceneEventSo.SceneLoadedAction += LoadScene;
    }

    private void Start()
    {
        LoadScene(firstScene,startPosition,true);
    }

    private void LoadScene(SceneDataSo scene,Vector3 position,bool isFadeOut)
    {
        if (_isLoading)
            return;
        _isLoading = true;
        _isFadeOut = isFadeOut;
        _targetScene = scene;
        _targetPosition = position;
        StartCoroutine(LoadSceneCoroutine(scene,isFadeOut));
    }

    private IEnumerator LoadSceneCoroutine(SceneDataSo scene,bool isFadeOut)
    {
        playerTransform.gameObject.SetActive(false);
        if(_currentScene is not null)
            yield return _currentScene.SceneRef.UnLoadScene();
        LoadNewScene();
    }

    private void LoadNewScene()
    {
        var mode = _targetScene.isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;
        var asyncOperationHandle = _targetScene.SceneRef.LoadSceneAsync(mode,true);
        asyncOperationHandle.Completed += AfterLoad;
    }

    private void AfterLoad(AsyncOperationHandle<SceneInstance> obj)
    {
        _currentScene = _targetScene;
        playerTransform.position = _targetPosition;
        playerTransform.gameObject.SetActive(true);
        if (_isFadeOut)
        {
            //todo:渐隐
        }
        _isLoading = false;
        afterLoadSceneEvent.Raise();
    }

    private void OnDisable()
    {
        loadSceneEventSo.SceneLoadedAction -= LoadScene;
    }
}
