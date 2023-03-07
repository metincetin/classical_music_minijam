using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class UIInput : MonoBehaviour
{

    private GameInput _gameInput;

    [SerializeField]
    private MusicSelector _musicSelector;
    
    private void Awake()
    {
        _gameInput = new GameInput();
    }

    private void OnEnable()
    {
        _gameInput.Enable();
        _gameInput.UI.MusicSelector.performed += OnMusicSelectorInput;
    }

    private void OnDisable()
    {
        _gameInput.Disable();
        _gameInput.UI.MusicSelector.performed -= OnMusicSelectorInput;
    }

    private void OnMusicSelectorInput(InputAction.CallbackContext obj)
    {
        _musicSelector.Toggle();
    }
}
