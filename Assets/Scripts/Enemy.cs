using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour, IDamageable
{
    [SerializeField]
    private float _health;
    public float Health => _health;

    [SerializeField]
    private Animator _animator;

    [SerializeField]
    private float _maxHealth;

    private MaterialPropertyBlock _damagePropertyBlock;
    public float MaxHealth => _maxHealth;

    public event Action<Enemy> Died;

    [SerializeField]
    private DropData[] _dropData;

    private void Start()
    {
        _damagePropertyBlock = new MaterialPropertyBlock();
        _health = _maxHealth;
    }

    public void ApplyDamage(DamageData damageData)
    {
        if (_health <= 0) return;
        _health -= damageData.Damage;
        if (_health <= 0)
        {
            Die();
        }

        _animator.SetTrigger("Hit");
        
        StartCoroutine(ApplyDamageFX());
    }

    private IEnumerator ApplyDamageFX()
    {
        var rend = GetComponentInChildren<Renderer>();
     
        for (var i = 0; i < rend.materials.Length; i++)
        {
            rend.materials[i].SetColor("_EmissionColor", Color.red);
        }

        yield return new WaitForSeconds(0.2f);
      
        for (var i = 0; i < rend.materials.Length; i++)
        {
            rend.materials[i].SetColor("_EmissionColor", Color.black);
        }
        
    }

    private void Die()
    {

        _animator.SetTrigger("Death");
        Destroy(gameObject, 2f);

        GetComponent<AIController>().enabled = false;
        
        Died?.Invoke(this);

        foreach (var dropData in _dropData)
        {
            if (dropData.Chance >= Random.value)
            {
                var inst = dropData.Create(gameObject);
            }
        }
    }
}
