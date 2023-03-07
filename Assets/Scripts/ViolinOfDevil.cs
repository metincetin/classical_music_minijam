using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ViolinOfDevil : MonoBehaviour, IInteractable
{
    public string Name => "Violin of the Devil";
    public string InteractionBehaviour => "Take";

    [SerializeField]
    private GameObject _endTimeline;
    
    public bool CanInteract(Interactor interactor)
    {
        return true;
    }

    public void Interact(Interactor interactor)
    {
        if (interactor.TryGetComponent<Player>(out var player))
        {
            player.enabled = false;
            player.GetComponent<MusicPlayer>().AudioSource.DOFade(0, 1f);
            Cursor.lockState = CursorLockMode.None;
            _endTimeline.SetActive(true);
        }
    }
}
