using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Rigidbody _rigidbody;
    public GameObject Caster { get; set; }
    public Vector3 Direction { get; set; }

    [SerializeField]
    private float _speed = 8;

    [SerializeField]
    private float _damage;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        var casterCollider = Caster.GetComponent<Collider>();
        var selfCollider = GetComponentInChildren<Collider>();
        Physics.IgnoreCollision(casterCollider, selfCollider, true);

        _rigidbody.velocity = Direction * _speed;
        Destroy(gameObject, 5);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent<IDamageable>(out var damageable))
        {
            damageable.ApplyDamage(new DamageData{Damage = _damage, Causer = Caster, HitPosition = collision.GetContact(0).point});
        }
        Destroy(gameObject);
    }
}
