using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Instrument : ScriptableObject
{
    public string Name;
    public GameObject Prefab;

    public float Damage;

    public Vector3 HandPosition;
    public Vector3 HandRotation;
}
