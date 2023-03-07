using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using DG.Tweening;
using Unity.Mathematics;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Transform _shootFrom;

    [SerializeField]
    private LayerMask _shootLayerMask;
    
    [SerializeField]
    private GameObject _hitFX;

    [SerializeField] private GameObject _trailFX;
    
    public void Shoot()
    {
        if (Physics.Raycast(_shootFrom.position, _shootFrom.forward, out var hit, Mathf.Infinity, _shootLayerMask))
        {
            CreateHitEffect(hit);
            void Apply(IDamageable damageable)
            {
                damageable.ApplyDamage(new DamageData
                    { Damage = Damage * DamageMultiplier, Causer = gameObject, HitPosition = hit.point });
            }

            if (hit.rigidbody)
            {
                var dir = (hit.rigidbody.position - hit.point).normalized;
                hit.rigidbody.AddForceAtPosition(dir * Damage * Damage, hit.point, ForceMode.Impulse);
            }
            
            {
                if (hit.rigidbody && hit.rigidbody.TryGetComponent<IDamageable>(out var damagable))
                {
                    Apply(damagable);
                }else if (hit.collider.TryGetComponent<IDamageable>(out var dmg))
                {
                    Apply(dmg);
                }
            }
        }
        else
        {
            CreateHitEffect(new RaycastHit{point = _shootFrom.position + _shootFrom.forward * 30});
        }
    }

    private void CreateHitEffect(RaycastHit hitInfo)
    {
        Instantiate(_hitFX, hitInfo.point, Quaternion.identity);
        var trailFx = Instantiate(_trailFX);
        trailFx.transform.DOMove(hitInfo.point, 0.06f).From(transform.position).SetEase(Ease.Linear);
    }

    public float Damage;
    public float DamageMultiplier { get; set; } = 1;
}

public struct DamageData
{
    public float Damage;
    public Vector3 HitPosition;
    public GameObject Causer;
}