using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField]
    private Image _fillImage;

    [SerializeField]
    private Player _player;

    private void Update()
    {
        _fillImage.transform.localScale = new Vector3(_player.Health / _player.MaxHealth,1,1);
    }
}
