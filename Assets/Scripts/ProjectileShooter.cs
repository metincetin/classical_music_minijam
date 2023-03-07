using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class ProjectileShooter : MonoBehaviour
    {
        [SerializeField] private Projectile _projectilePrefab;

        [SerializeField]
        private float _shootRate;

        [SerializeField]
        private float _verticalShootOffset;

        private float _lastShoot;
        
        public void Shoot(GameObject target)
        {
            if (Time.time - _lastShoot > _shootRate)
            {
                var inst = Instantiate(_projectilePrefab, transform.position + Vector3.up * _verticalShootOffset, Quaternion.identity);
                inst.Caster = gameObject;
                inst.Direction = ((target.transform.position + Vector3.up * 1) - (transform.position + Vector3.up * _verticalShootOffset)).normalized;
                _lastShoot = Time.time;
            }
        }

    }
}