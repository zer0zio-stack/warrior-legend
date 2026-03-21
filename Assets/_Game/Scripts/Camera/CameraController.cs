using System;
using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    private CinemachineConfiner2D cinemachineConfiner;
    public CinemachineImpulseSource source;
    public VoidEventSo VoidEventSo;
    public VoidEventSo AfterLoadSceneEvent;

    private void Awake()
    {
        cinemachineConfiner=GetComponent<CinemachineConfiner2D>();
    }

    private void Start()
    {
        _setConfiner();
    }

    private void OnEnable()
    {
        VoidEventSo.VoidEventAction += _cameraShake;
        AfterLoadSceneEvent.VoidEventAction+=_setConfiner;
    }

    private void OnDisable()
    {
        VoidEventSo.VoidEventAction -= _cameraShake;
        AfterLoadSceneEvent.VoidEventAction -= _setConfiner;
    }

    private void _cameraShake()
    {
        source.GenerateImpulse();
    }

    private void _setConfiner()
    {
        var findGameObjectWithTag = GameObject.FindGameObjectWithTag("bounds");
        if (findGameObjectWithTag != null)
        {
            cinemachineConfiner.BoundingShape2D = findGameObjectWithTag.GetComponent<Collider2D>();
            cinemachineConfiner.InvalidateBoundingShapeCache();
        }
    }
}
