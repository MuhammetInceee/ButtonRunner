using DG.Tweening;
using UnityEngine;
public class FinishRayHitter : MonoBehaviour
{
    RaycastHit hit;
    public LayerMask layerMask;
    public Transform hittedTransform;
    Vector3 tempPos;

    private void Update()
    { 
        if (Physics.Raycast(transform.position, Vector3.back, out hit, Mathf.Infinity, layerMask.value))
        {
            if (Equals(hittedTransform, hit.transform))
            {
            }
            else
            {
                if (hittedTransform)
                {
                    hittedTransform.transform.DOScaleX(5.5f, .2f);
                    hittedTransform.transform.gameObject.name = "Pass";
                }
                hittedTransform = hit.transform;
                hittedTransform.transform.gameObject.name = "Current";
                tempPos = hittedTransform.localScale;
                hittedTransform.transform.DOScaleX(6.6f, .2f);
            }
        }
    }
}