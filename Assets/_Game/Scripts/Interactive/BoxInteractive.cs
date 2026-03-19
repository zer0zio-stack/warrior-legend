using System;
using UnityEngine;

public class BoxInteractive:MonoBehaviour,IInteractive
{

    public Sprite unDoneBox;
    public Sprite doneBox;
    private bool _isDone;
    private SpriteRenderer _renderer;
    private AudioScript _audioScript;

    private void Awake()
    {
        _renderer=GetComponent<SpriteRenderer>();
        _audioScript = GetComponent<AudioScript>();
    }

    public void Interactive()
    {
        if (!_isDone)
        {
            _isDone = true;
            _audioScript.PlayAudio();
            Debug.Log("打开宝箱！");
        }
    }

    private void Update()
    {
        _renderer.sprite=_isDone?doneBox:unDoneBox;
    }
}
