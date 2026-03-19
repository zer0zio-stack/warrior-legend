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
    private IInteractive _interactive;
    public InputSystemActions InputActions;
    private bool _isBox;
    private Collider2D _otherCollider;

    private void Awake()
    {
        _renderer = sign.GetComponent<Renderer>();
        _animator = sign.GetComponent<Animator>();
        InputActions = new InputSystemActions();
        InputActions.Enable();
        InputActions.Playing.Cofirm.started += ConfirmInteractive;
    }

    private void ConfirmInteractive(InputAction.CallbackContext obj)
    {
        if(_isInteractive)
        {
            _interactive.Interactive();
            if (_isBox)
            {
                _otherCollider.tag="Untagged";
            }
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Interactive"))
        {
            _isInteractive = true;
            _otherCollider=other;
            _interactive = other.GetComponent<IInteractive>();
            _isBox = other.GetComponent<BoxInteractive>();
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
