using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TMProParticle : MonoBehaviour
{
    [SerializeField] private TMP_Text _text;

    private ParticleSystemRenderer _particleSystem;

    private void Awake()
    {
        _particleSystem = GetComponent<ParticleSystemRenderer>();
    }

    private void Start()
    {
        _particleSystem.mesh = _text.mesh;
    }

    private void OnValidate()
    {
        GetComponent<ParticleSystemRenderer>().mesh = _text.GetComponentInChildren<TMP_SubMesh>().mesh;
    }
}
