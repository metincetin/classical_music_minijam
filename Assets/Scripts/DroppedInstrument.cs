using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedInstrument : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Instrument _instrument;

    public Instrument Instrument
    {
        get => _instrument;
        set => _instrument = value;
    }

    public string Name => _instrument.Name;
    public string InteractionBehaviour => "Take";

    public bool CanInteract(Interactor interactor)
    {
        return interactor.TryGetComponent<EquipmentController>(out var _);
    }

    public void Interact(Interactor interactor)
    {
        var equipmentController = interactor.GetComponent<EquipmentController>();
        equipmentController.Equip(Instrument);

        interactor.TargetInteractable = null;
        
        Destroy(gameObject);
    }
}
