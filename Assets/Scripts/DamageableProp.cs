using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class DamageableProp : MonoBehaviour, IDamageable
{
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void ApplyDamage(DamageData damageData)
    {
        if (damageData.Causer)
        {
            var dir = (transform.position - damageData.HitPosition).normalized;
            _rigidbody.AddForceAtPosition(dir * damageData.Damage, damageData.HitPosition, ForceMode.Impulse);
        }
    }
}