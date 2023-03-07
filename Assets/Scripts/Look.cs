using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Look : MonoBehaviour
{
    [SerializeField]
    private Vector2 _verticalRange;

    private float _xAnge;

    public void AddInput(float v)
    {
        _xAnge += v * Time.deltaTime;
        _xAnge = Mathf.Clamp(_xAnge, _verticalRange.x, _verticalRange.y);

        transform.localRotation = Quaternion.Euler(_xAnge, 0, 0);
    }
    
}
