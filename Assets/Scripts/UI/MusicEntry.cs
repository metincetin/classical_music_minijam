using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
public class MusicEntry: MonoBehaviour, IPointerClickHandler
{

    [SerializeField]
    private TMP_Text _titleText;
    [SerializeField]
    private TMP_Text _descriptionText;

    public event Action Selected;
    
    private Music _music;
    public Music Music
    {
        get => _music;
        set
        {
            _music = value;
            _titleText.text = _music.PieceName;
            _descriptionText.text = $"Damage: {_music.Damage}\nBeat Per Second: {MusicMath.BPS(_music.BPM).ToString("F")}\nInstrument: {_music.RequiredInstrument.Name}";
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Selected?.Invoke();
    }
}