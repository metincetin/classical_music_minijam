using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomAIActivator : MonoBehaviour
{
    [SerializeField]
    private Room _room;

    private void OnEnable()
    {
        _room.PlayerEntered += Activate;
        _room.PlayerExited += Deactivate;
    }

    private void OnDisable()
    {
        _room.PlayerEntered -= Activate;
        _room.PlayerExited -= Deactivate;
    }

    private void Activate()
    {
        ToggleAI(true);
    }

    private void Deactivate()
    {
        ToggleAI(false);
    }

    private void ToggleAI(bool b)
    {
        foreach (var enemy in _room.Enemies)
        {
            enemy.GetComponent<AIController>().enabled = b;
        }
    }
}
