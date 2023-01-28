using System;
using System.Collections;
using System.Collections.Generic;
using Dreamteck.Splines;
using UnityEngine;

public class SplineDebugger : MonoBehaviour
{
    private SplineFollower sc;

    private void Awake()
    {
        sc = GetComponent<SplineFollower>();
    }

    private void FixedUpdate()
    {
        //Debug.Log(sc.GetPercent());
    }
}
