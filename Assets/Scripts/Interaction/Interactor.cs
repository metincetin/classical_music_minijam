using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class Interactor: MonoBehaviour
{

    [SerializeField]
    private float _distance = 10f;
    public float Distance => _distance;
    
    public event Action<IInteractable> TargetInteractableChanged;
    
    private IInteractable _targetInteractable;
    public IInteractable TargetInteractable
    {
        get => _targetInteractable;
        set
        {
            if (value == _targetInteractable) return;
            if (value == null)
            {
                _targetInteractable = null;
                TargetInteractableChanged?.Invoke(value);
                return;
            }
            if (value.CanInteract(this) == false) return;
            _targetInteractable = value;
            TargetInteractableChanged?.Invoke(value);
        }
    }

    public void Interact()
    {
        if (!(Object)TargetInteractable) return;
        TargetInteractable.Interact(this);
    }
}