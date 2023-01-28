using System;
using Dreamteck.Splines;
using UnityEngine.UI;
using UnityEngine;

public class LevelProgressBar : MonoBehaviour
{
    private SplineFollower _playerFollower;
    
    [SerializeField] private Slider slider;
    [SerializeField] private GameObject player;


    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        _playerFollower = player.GetComponent<SplineFollower>();
    }

    private void Update()
    {
        float lerp = Mathf.Lerp(slider.value, (float)_playerFollower.GetPercent(), 0.4f);
        slider.value = lerp;
    }
}