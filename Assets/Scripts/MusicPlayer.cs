using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    private Music _music;

    [SerializeField] private AudioSource _audioSource;

    public event Action<Music> MusicStarted;
    public event Action<Music> MusicStopped;
    public event Action<Music, Music> MusicChanged; 

    public Music Music
    {
        get => _music;
        set
        {
            if (_music == value) return;
            if (_music != value)
            {
                MusicChanged?.Invoke(value, _music);
            }
            _music = value;

            _delayBetweenNotes = MusicMath.DurationPerBeat(_music.BPM);
            _audioSource.clip = _music.Clip;

            Stop();
            Play();
        }
    }

    public float RelativePlayedTime
    {
        get { return Mathf.Max(0, Time.time - _musicStartTime); }
    }

    public float GlobalPlayedTime
    {
        get { return RelativePlayedTime + _musicStartTime; }
    }

    private float _delayBetweenNotes;
    public float DelayBetweenNotes => _delayBetweenNotes;

    private float _musicStartTime;
    public float MusicStartTime => _musicStartTime;

    public event Action PerfectNotePlayed;


    public void Play()
    {
        if (_music)
        {
            _audioSource.Play();
            _musicStartTime = Time.time;

            MusicStarted?.Invoke(_music);
        }
    }

    public void Stop()
    {
        _audioSource.Stop();
    }

    public void Pause()
    {
        _audioSource.Pause();
    }

    public float NextNoteTime
    {
        get { return GetNoteStartTime(PlayedNotes + 1); }
    }


    private void Update()
    {
        if (_audioSource.time >= _audioSource.clip.length)
        {
            Stop();
            Play();
        }
    }

    public int PlayedNotes
    {
        get
        {
            var playDuration = Mathf.Max(0, Time.time - _musicStartTime - _music.StartOffset);
            var bps = MusicMath.BPS(_music.BPM);

            return Mathf.FloorToInt(playDuration * bps);
        }
    }

    public float GetNoteStartTimeFromBeginning(int note)
    {
        return Mathf.Round((_musicStartTime + _delayBetweenNotes * note) / _delayBetweenNotes) * _delayBetweenNotes +
               _music.StartOffset;
    }

    public void InvokePerfectNote()
    {
        PerfectNotePlayed?.Invoke();
    }

    public float GetNoteEndTimeFromBeginning(int note)
    {
        return GetNoteStartTimeFromBeginning(note) + _delayBetweenNotes;
    }

    public float GetNoteStartTime(int xNoteAfterThis)
    {
        return Mathf.Round((Time.time + _delayBetweenNotes * xNoteAfterThis) / _delayBetweenNotes) *
            _delayBetweenNotes + _music.StartOffset;
    }

    public float GetNoteEndTime(int xNoteAfterThis)
    {
        return GetNoteStartTime(xNoteAfterThis) + _delayBetweenNotes;
    }

    public bool IsPlaying => _audioSource.isPlaying;
    public AudioSource AudioSource => _audioSource;
}