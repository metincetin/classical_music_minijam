using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class MusicianEntry : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text _nameText;

    private Composer _composer;

    public event Action Selected;
    
    public Composer Composer
    {
        get => _composer;
        set
        {
            _composer = value;
            _nameText.text = _composer.ComposerName;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Selected?.Invoke();
    }
}