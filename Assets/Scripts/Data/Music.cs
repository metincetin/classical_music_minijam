using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Music : ScriptableObject
{
    [SerializeField]
    private string _pieceName;
    public string PieceName => _pieceName;
    [SerializeField]
    private Composer _composer;
    public Composer Composer => _composer;
    
    [SerializeField]
    private int _bpm;
    public int BPM => _bpm;
    
    [SerializeField]
    private AudioClip _clip;
    public AudioClip Clip => _clip;

    [SerializeField]
    private float _startOffset;
    public float StartOffset => _startOffset;

    [SerializeField]
    private Instrument _requiredInstrument;
    public Instrument RequiredInstrument => _requiredInstrument;

    [SerializeField]
    private float _damage;
    public float Damage => _damage;
}
