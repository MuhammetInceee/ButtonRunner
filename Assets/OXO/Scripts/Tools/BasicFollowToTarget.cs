using System;
using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class BasicFollowToTarget : MonoBehaviour
{
    [Header("Target Object Transform")]
    public Transform target;

    public bool autoTarget;
    public bool x = true, y = true, z = true;

    protected Vector3 offset;
    public Vector3 followOffset;

    public float lerpValue = 1;
    public int updateMethod;

    private void OnEnable()
    {
        transform.position = target.transform.position;
    }

    private void FixedUpdate()
    {
        if (updateMethod == 0)
        {
            if (autoTarget && !target)
            {
                
            }
            Vector3 vector = new Vector3(target.position.x * BoolConverter(x), target.position.y * BoolConverter(y), target.position.z * BoolConverter(z));
            if (x == false)
            {
                vector.x = transform.position.x;
            }

            if (y == false)
            {
                vector.y =  transform.position.y;
            }
          //  vector += offset;

            transform.position = Vector3.Lerp(transform.position, vector, lerpValue);
        }
    }
    private void LateUpdate()
    {
        if (updateMethod == 1)
        {
            Vector3 vector = new Vector3(target.position.x * BoolConverter(x), target.position.y * BoolConverter(y), target.position.z * BoolConverter(z));
            vector += offset;

            transform.position = Vector3.Lerp(transform.position, vector, lerpValue);
        }
    }
    public int BoolConverter(bool boolen)
    {
        if (boolen)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}
