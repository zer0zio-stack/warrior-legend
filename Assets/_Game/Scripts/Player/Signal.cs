using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class Signal : MonoBehaviour
{
    public GameObject sign;
    public Transform playerTransform;
    private bool _isInteractive;
    private Renderer _renderer;
    private Animator _animator;


    private void Awake()
    {
        _renderer = sign.GetComponent<Renderer>();
        _animator = sign.GetComponent<Animator>();
    }
    
    

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactive"))
        {
            _isInteractive = true;
        }
    }

    private void Update()
    {
        _renderer.enabled=_isInteractive;
        sign.transform.localScale=playerTransform.localScale;
        _animator.Play("keyBoard");
        
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        _isInteractive = false;
    }
}
