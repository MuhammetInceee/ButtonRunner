using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class NewHighScoreEffect : MonoBehaviour
{
    private readonly float _delay = 0.5f;
    private void Awake()
    {
        StartCoroutine(Boing());
    }
    private IEnumerator Boing()
    {
        yield return new WaitForSeconds(_delay * 2);
        transform.DOScale(Vector3.one * 2f, _delay).OnComplete(() =>
        {
            transform.DOScale(Vector3.one, _delay);
            StartCoroutine(Boing());
        });
    }
    
}
