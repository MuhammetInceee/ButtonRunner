using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class ButtonRandomMaterial : MonoBehaviour
{
    public List<Material> colorList;

    public Material targetMat;
    private void Start()
    {
        targetMat = colorList[Random.Range(0, colorList.Count)];
        GetComponent<MeshRenderer>().material = targetMat;
    }
}
