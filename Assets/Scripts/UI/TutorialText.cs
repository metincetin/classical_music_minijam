using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class TutorialText : MonoBehaviour
{
    private TMP_Text _text;
    
    public static TutorialText Instance { get; private set; }

    private void Awake()
    {
        _text = GetComponent<TMP_Text>();
        Instance = this;
    }

    public void ShowText(string text)
    {
        _text.text = text;
        DOTween.Sequence()
            .Append(_text.DOFade(1f, 0.2f))
            .AppendInterval(3f)
            .Append(_text.DOFade(0f, .2f));
    }
}
