using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class NoteDisplay : MonoBehaviour
{
    [SerializeField]
    private MusicPlayer _musicPlayer;

    [SerializeField]
    private GameObject _correctTimeNoteFX;

    [SerializeField]
    private Transform _perfectNoteFXContainer;

    [SerializeField]
    private Note _note;

    private List<Note> _activeNotes = new List<Note>();

    private void Awake()
    {
        for (int i = 0; i < 3; i++)
        {
            _activeNotes.Add(Instantiate(_note, transform));
        }
    }

    private void OnEnable()
    {
        _musicPlayer.MusicStarted += OnMusicStarted;
        _musicPlayer.PerfectNotePlayed += OnPerfectNotePlayed;
    }

    private void OnDisable()
    {
        _musicPlayer.MusicStarted -= OnMusicStarted;
        _musicPlayer.PerfectNotePlayed -= OnPerfectNotePlayed;
    }

    private void OnPerfectNotePlayed()
    {
        var inst = Instantiate(_correctTimeNoteFX, _perfectNoteFXContainer);
        inst.transform.localPosition = Vector3.zero;
        inst.GetComponentInChildren<TMP_Text>().DOFade(0, 1.5f).SetDelay(0.5f);
        inst.transform.DOBlendableLocalMoveBy(Vector3.up * 200, 2)
            .OnComplete(() => Destroy(inst.gameObject));
    }

    private void OnMusicStarted(Music obj)
    {
        for (var i = 0; i < _activeNotes.Count; i++)
        {
            var activeNote = _activeNotes[i];
            activeNote.NoteIndex = i;
        }
    }
    
    private void Update()
    {
        if (_musicPlayer.IsPlaying && _musicPlayer.Music != null)
        {
            var currentNote = _musicPlayer.PlayedNotes;
            for (var i = _activeNotes.Count - 1; i >= 0; i--)
            {
                var activeNote = _activeNotes[i];
                if (activeNote.NoteIndex < currentNote)
                {
                    activeNote.NoteIndex = currentNote + _activeNotes.Count - 1;
                }

                var rectTransform = transform as RectTransform;
                var noteRectTransform = activeNote.transform as RectTransform;
                
                var noteStartTime = _musicPlayer.GetNoteStartTimeFromBeginning(activeNote.NoteIndex);
                var noteEndTime = _musicPlayer.GetNoteEndTimeFromBeginning(activeNote.NoteIndex);
                var playedTime = _musicPlayer.GlobalPlayedTime;

                
                var t = Mathf.InverseLerp(noteStartTime, noteEndTime, playedTime);

                activeNote.gameObject.SetActive(playedTime >= noteStartTime);
                activeNote.transform.localPosition = new Vector3(
                    Mathf.Lerp(rectTransform.rect.min.x + noteRectTransform.rect.size. x * 0.5f, rectTransform.rect.max.x - noteRectTransform.rect.size. x * 0.5f, 1 - t),
                    0, 0);
            }
        }
    }
    
}
