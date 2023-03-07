using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLocker : MonoBehaviour
{
    private Room _room;
    
    private void Awake()
    {
        _room = GetComponentInParent<Room>();
    }

    private void Start()
    {
        Unlock();
    }

    private void OnEnable()
    {
        _room.PlayerEntered += PlayerEntered;
    }

    private void OnDisable()
    {
        _room.PlayerEntered -= PlayerEntered;
    }

    private void PlayerEntered()
    {
        Lock();
    }

    private void Lock()
    {
        foreach (var c in GetComponentsInChildren<Collider>())
        {
            c.enabled = true;
        }
    }

    private void Unlock()
    {
        foreach (var c in GetComponentsInChildren<Collider>())
        {
            c.enabled = false;
        }
    }

    private void Update()
    {
        if (_room.Enemies.Count == 0)
        {
            Unlock();
            enabled = false;
        }
    }
}
