using System;
using DG.Tweening;
using UnityEngine;

public class SpiralCameraController : MonoBehaviour
{
    [SerializeField] private float speed;
    
    private void Update()
    {
        transform.Translate(-transform.forward * (speed * Time.deltaTime));
    }
}
