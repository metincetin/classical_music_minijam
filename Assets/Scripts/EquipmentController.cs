using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class EquipmentController : MonoBehaviour
{
    private Instrument _equippedInstrument;

    public Instrument EquippedInstrument
    {
        get => _equippedInstrument;
        private set
        {
            _equippedInstrument = value;
            ClearGraphics();
            CreateGraphics();
            
            EquipmentChanged?.Invoke(_equippedInstrument);
        }
    }

    private GameObject _graphics;

    [SerializeField] private Transform _handContainer;

    public event Action<Instrument> EquipmentChanged;

    public void Equip(Instrument instrument)
    {
        if (EquippedInstrument != null)
        {
            Drop();
        }

        EquippedInstrument = instrument;
    }

    private void CreateGraphics()
    {
        if (!EquippedInstrument) return;
        var inst = Instantiate(_equippedInstrument.Prefab, _handContainer);



        inst.transform.DOLocalMove(EquippedInstrument.HandPosition, 0.2f)
            .From(new Vector3(0, -1f))
            .SetEase(Ease.OutBack);
        
        inst.transform.DOLocalRotate(EquippedInstrument.HandRotation, .2f)
            .From(new Vector3(0, 0, -12));

        foreach (var c in inst.GetComponentsInChildren<Transform>())
        {
            c.gameObject.layer = LayerMask.NameToLayer("FPSHand");
        }
        
        _graphics = inst;
    }

    private void ClearGraphics()
    {
        Destroy(_graphics);
    }

    private void Drop()
    {
        var inst = Instantiate(EquippedInstrument.Prefab);
        inst.transform.position = transform.position + transform.forward * 1 + Vector3.up;
        inst.AddComponent<Rigidbody>();
        inst.AddComponent<DroppedInstrument>().Instrument = EquippedInstrument;
        EquippedInstrument = null;
    }
}