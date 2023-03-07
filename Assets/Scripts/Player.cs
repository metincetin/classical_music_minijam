using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, IDamageable
{
    private GameInput _input;
    private Shooter _shooter;

    private Look _look;
    private CharacterMovement _movement;

    private MusicPlayer _musicPlayer;

    [SerializeField]
    private List<Music> _unlockedMusics = new List<Music>();
    public List<Music> UnlockedMusics => _unlockedMusics;
    public event Action<Music> MusicLearned;
    
    [SerializeField] private GameSettings _settings;


    [SerializeField] private float _sensitivity = 4;
    

    private Interactor _interactor;
    
    [SerializeField]
    private LayerMask _interactionLayerMask;

    [SerializeField]
    private float _health;

    public float Health => _health;
    
    [SerializeField]
    private float _maxHealth;

    public float MaxHealth => _maxHealth;
    
    private Camera _camera;
    private EquipmentController _equipmentController;

    public event Action Died;

    [SerializeField]
    private Instrument _startInstrument;

    private void Awake()
    {
        _input = new GameInput();
        _movement = GetComponent<CharacterMovement>();
        _look = GetComponentInChildren<Look>();
        _shooter = GetComponent<Shooter>();
        _musicPlayer = GetComponent<MusicPlayer>();
        _interactor = GetComponent<Interactor>();
        _camera = Camera.main;
        _musicPlayer.Music = _unlockedMusics[0];

        _equipmentController = GetComponent<EquipmentController>();
    }

    private void Start()
    {
        _health = _maxHealth;
        Cursor.lockState = CursorLockMode.Locked;
        _equipmentController.Equip(_startInstrument);
        
        TutorialText.Instance.ShowText("Follow the rhythm to deal extra damage");
    }

    private void OnEnable()
    {
        _input.Enable();

        _input.Player.Shoot.performed += OnShootInput;
        _input.Player.Jump.performed += OnJumpInput;
        _input.Player.Interact.performed += OnInteractInput;
        
        
        _equipmentController.EquipmentChanged += OnEquipmentChanged;
        _musicPlayer.MusicChanged += OnMusicChanged;
    }

    private void OnDisable()
    {
        _input.Disable();
        _input.Player.Shoot.performed -= OnShootInput;
        _input.Player.Jump.performed -= OnJumpInput;
        _input.Player.Interact.performed += OnInteractInput;

        _equipmentController.EquipmentChanged -= OnEquipmentChanged;
        _musicPlayer.MusicChanged -= OnMusicChanged;
    }

    
    private void OnMusicChanged(Music curMusic, Music prevMusic)
    {
        if (prevMusic != null)
        {
            _shooter.Damage -= prevMusic.Damage;
        }

        _shooter.Damage += curMusic.Damage;
    }
    
    private void OnEquipmentChanged(Instrument instrument)
    {
        if (instrument == null)
        {
            _shooter.Damage = 0;
            return;
        }
        _shooter.Damage = instrument.Damage;

        if (_musicPlayer.Music.RequiredInstrument != instrument)
        {
            var playableMusic = _unlockedMusics.First(x => x.RequiredInstrument == instrument);
            _musicPlayer.Music = playableMusic;
        }
    }

    private void OnInteractInput(InputAction.CallbackContext obj)
    {
        _interactor.Interact();
    }

    private void OnJumpInput(InputAction.CallbackContext obj)
    {
        _movement.Jump();
    }

    private void OnShootInput(InputAction.CallbackContext obj)
    {
        float timeMiss = Mathf.Abs(_musicPlayer.GlobalPlayedTime -
                                   _musicPlayer.GetNoteStartTimeFromBeginning(_musicPlayer.PlayedNotes));
        bool perfectBeat = timeMiss < _settings.CorrectRhythmMaxOffset;
        
        print($"Time miss: {timeMiss}");
        if (perfectBeat)
        {
            _shooter.DamageMultiplier += 2;
            _musicPlayer.InvokePerfectNote();
        }

        _shooter.Shoot();

        if (perfectBeat)
        {
            _shooter.DamageMultiplier -= 2;
        }
    }

    private void Update()
    {
        var look = _input.Player.Look.ReadValue<Vector2>();
        _look.AddInput(-look.y * _sensitivity);
        _movement.Rotate(look.x * _sensitivity);

        var movementInput = _input.Player.Movement.ReadValue<Vector2>();
        _movement.Move(movementInput);


        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hit, _interactor.Distance, _interactionLayerMask))
        {
            if (hit.rigidbody && hit.rigidbody.TryGetComponent<IInteractable>(out var interactable))
            {
                _interactor.TargetInteractable = interactable;
            }
            else if (hit.collider && hit.collider.TryGetComponent<IInteractable>(out var interactableCol))
            {
                _interactor.TargetInteractable = interactableCol;
            }
            else
            {
                _interactor.TargetInteractable = null;
            }
        }
        else
        {
            _interactor.TargetInteractable = null;
        }
    }

    public void ApplyDamage(DamageData damageData)
    {
        if (_health <= 0) return;
        _health -= damageData.Damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        _musicPlayer.Stop();
        Died?.Invoke();
        enabled = false;
        Cursor.lockState = CursorLockMode.None;
    }

    public void UnlockMusic(Music music)
    {
        if (_unlockedMusics.Contains(music)) return;

        if (_unlockedMusics.Count == 2)
        {
            TutorialText.Instance.ShowText("Press M to change music.");
        }
        
        _unlockedMusics.Add(music);
        MusicLearned?.Invoke(music);
    }
}