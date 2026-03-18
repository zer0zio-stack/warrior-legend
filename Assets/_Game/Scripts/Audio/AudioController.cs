using System;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [Header("source")]
    public AudioSource _bgmSource;
    public AudioSource _fxSource;

    [Header("So")]
    public AudioEventSo BgmEventSo;
    public AudioEventSo FxEventSo;

    private void OnEnable()
    {
        BgmEventSo.audioEventAction += bgmPlay;
        FxEventSo.audioEventAction += FxPlay;
    }

    private void FxPlay(AudioClip arg0)
    {
        _fxSource.clip = arg0;
        _fxSource.Play();
    }

    private void OnDisable()
    {
        BgmEventSo.audioEventAction -= bgmPlay;
        FxEventSo.audioEventAction -= FxPlay;
    }

    private void bgmPlay(AudioClip clip)
    {
        _bgmSource.clip = clip;
        _bgmSource.Play();
    }
}
