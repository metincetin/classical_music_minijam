using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class InteractionText : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _text;

    [SerializeField]
    private Interactor _interactor;

    private void OnEnable()
    {
        _interactor.TargetInteractableChanged += OnInteractableChanged;
    }

    private void OnDisable()
    {
        _interactor.TargetInteractableChanged -= OnInteractableChanged;
    }

    private void OnInteractableChanged(IInteractable obj)
    {
        GetComponent<Canvas>().enabled = (Object)obj;

        if ((Object)obj)
        {
            _text.text = $"{obj.Name}\n<b>{obj.InteractionBehaviour}</b>";
            StartCoroutine(UpdateLayoutDelayed());
        }
    }

    private IEnumerator UpdateLayoutDelayed()
    {
        yield return new WaitForEndOfFrame();
        LayoutRebuilder.ForceRebuildLayoutImmediate(transform as RectTransform);
    }
}
