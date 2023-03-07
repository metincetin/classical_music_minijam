using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MusicSelector : MonoBehaviour
{
    [SerializeField] private Transform _musiciansContainer;
    [SerializeField] private Transform _musicsContainer;
    [SerializeField] private Player _player;

    [SerializeField] private MusicianEntry _musicianEntryPrefab;
    [SerializeField] private MusicEntry _musicEntryPrefab;

    private Dictionary<Composer, List<Music>> _composerMusicTable = new Dictionary<Composer, List<Music>>();

    public Composer SelectedComposer { get; set; }
    public Music SelectedMusic { get; set; }


    private void Start()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        //_composerMusicTable.Clear();

        foreach (var music in _player.UnlockedMusics)
        {
            if (!_composerMusicTable.ContainsKey(music.Composer))
            {
                _composerMusicTable.Add(music.Composer, new List<Music>());

                var inst = Instantiate(_musicianEntryPrefab, _musiciansContainer);
                inst.Composer = music.Composer;

                inst.Selected += () =>
                {
                    SelectedComposer = inst.Composer;
                    UpdateMusicList();
                };
            }

            if (!_composerMusicTable[music.Composer].Contains(music))
            {
                _composerMusicTable[music.Composer].Add(music);
            }
        }

        SelectedComposer = _composerMusicTable.Keys.First();
        UpdateMusicList();
    }

    private void OnEnable()
    {
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0;
        UpdateUI();
    }


    private void OnDisable()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UpdateMusicList()
    {
        foreach (Transform c in _musicsContainer)
        {
            Destroy(c.gameObject);
        }

        var musics = _composerMusicTable[SelectedComposer];
        foreach (var music in musics)
        {
            var inst = Instantiate(_musicEntryPrefab, _musicsContainer);
            inst.Music = music;
            inst.Selected += () => { SelectedMusic = inst.Music; };
        }
    }

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeSelf);
    }

    public void Play()
    {
        if (SelectedMusic == null) return;
        if (_player.GetComponent<EquipmentController>().EquippedInstrument == SelectedMusic.RequiredInstrument)
        {
            _player.GetComponent<MusicPlayer>().Music = SelectedMusic;
        }
    }
}