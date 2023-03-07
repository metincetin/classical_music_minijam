using System;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

public class DeathScreen : MonoBehaviour
{
    private CanvasGroup _canvasGroup;
    [SerializeField]
    private Player _player;

    [SerializeField]
    private TMP_Text _deathText;

    [SerializeField]
    private string[] _deathTexts;

    [SerializeField]
    private AudioSource _audioSource;
    
    private void Awake()
    {
        _canvasGroup = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        _player.Died += OnPlayerDied;
    }

    private void OnPlayerDied()
    {
        _audioSource.Play();
        _canvasGroup.DOFade(1f, 1f).OnComplete(() =>
        {
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        });

        _deathText.text = _deathTexts[Random.Range(0, _deathTexts.Length)];
        _deathText.DOFade(1f, 1f).SetDelay(1f);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }
}