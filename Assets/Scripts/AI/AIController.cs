using System;
using DefaultNamespace;
using UnityEngine;

public class AIController: MonoBehaviour
{
    private Player _player;

    [SerializeField]
    private float _shootDistance;

    private CharacterMovement _movement;
    private ProjectileShooter _shooter;
    
    private void Awake()
    {
        _movement = GetComponent<CharacterMovement>();
        _shooter = GetComponent<ProjectileShooter>();
        
        // HEHEHEHEHEHEHEHHEHEHE
        _player = FindObjectOfType<Player>();
    }

    private void Start()
    {
    }

    private void Update()
    {
        var dist = Vector3.Distance(_player.transform.position, transform.position);
        var dir = (_player.transform.position - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(dir), 10f * Time.deltaTime);
        if (dist > _shootDistance)
        {
            _movement.Move(new Vector2(0, 1f).normalized);
        }
        else
        {
            _shooter.Shoot(_player.gameObject);
        }
        
    }
}