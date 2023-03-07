using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroppedMusic : MonoBehaviour, IInteractable
{
    [SerializeField]
    private Music _music;

    public Music Music
    {
        get => _music;
        set => _music = value;
    }

    public string Name => _music.PieceName;
    public string InteractionBehaviour => "Learn";

    public bool CanInteract(Interactor interactor)
    {
        return interactor.TryGetComponent<Player>(out var _);
    }

    public void Interact(Interactor interactor)
    {
        if (interactor.TryGetComponent<Player>(out var player))
        {
            player.UnlockMusic(_music);
            Destroy(gameObject);
        }
    }
}
