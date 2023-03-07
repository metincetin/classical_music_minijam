using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Room: MonoBehaviour
{
    private List<Enemy> _enemies = new List<Enemy>();
    public List<Enemy> Enemies => _enemies;

    public event Action PlayerEntered;
    public event Action PlayerExited;

    [SerializeField]
    private bool _startAIActivated;

    private void Awake()
    {
        _enemies = GetComponentsInChildren<Enemy>().ToList();
        foreach (var enemy in Enemies)
        {
            enemy.GetComponent<AIController>().enabled = _startAIActivated;
        }
    }

    private void OnEnable()
    {
        foreach (var enemy in _enemies)
        {
            enemy.Died += OnEnemyDied;
        }
    }

    private void OnDisable()
    {
        foreach (var enemy in _enemies)
        {
            enemy.Died -= OnEnemyDied;
        }
    }
    
    private void OnEnemyDied(Enemy enemy)
    {
        enemy.Died -= OnEnemyDied;

        _enemies.Remove(enemy);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<Player>(out var _))
        {
            PlayerEntered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<Player>(out var _))
        {
            PlayerExited?.Invoke();
        }
    }
}