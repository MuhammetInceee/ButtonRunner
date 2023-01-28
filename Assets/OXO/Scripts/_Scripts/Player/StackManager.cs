using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using Dreamteck.Splines;
using MuhammetInce.DesignPattern.Singleton;
using Unity.VisualScripting;
using UnityEngine;

public class StackManager : LazySingleton<StackManager>
{
    private bool _firstButton = true;
    public Material _targetMat;

    private Vector3 _stackAngle = new Vector3(0, -90, 0);

    [SerializeField] private GameObject stackTarget;
    [SerializeField] private float afterStackScaleFactor; 
    public List<GameObject> HolderList;
    public List<GameObject> StackedObjList;

    private void Start()
    {
        stackTarget = GameObject.FindWithTag("StackTarget");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Button"))
        { 
            ButtonTrigger(other);
            if (other.GetComponent<MoveButtonController>() != null)
            {
                other.GetComponent<MoveButtonController>().enabled = false;
                other.GetComponent<SplineFollower>().enabled = false;
                other.transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }
    
    #region Button Interaction

    private void TargetMatChanger()
    {
        _targetMat = StackedObjList[^1].GetComponent<MeshRenderer>().material;
    }
    
    private Transform TargetHolderTransform()
    {
        GameObject target = HolderList.FirstOrDefault(m => m.transform.childCount == 1);
        if (target != null)
        {
            return target!.transform;
        }
        return HolderList[^1].transform;
    }

    private void ButtonTrigger(Collider other)
    {
        Transform target = TargetHolderTransform();
        Transform otherTr = other.transform;
        otherTr.SetParent(HolderList[0].transform);
        
        other.transform.DOScale(Vector3.one * afterStackScaleFactor, 0.25f);
        

        //Real One
        otherTr.DOLocalMove(Vector3.zero, 0.07f).OnComplete(() =>
        {
            otherTr.SetParent(target);
            
            if (_firstButton)
            {
                otherTr.DOLocalMove(Vector3.zero, 0.2f);
                _firstButton = false;
            }
            else
            {
                otherTr.localPosition = new Vector3(0 + 0.3f,0,0);
                otherTr.DOLocalMove(Vector3.zero, 0.2f);
            }
            
            otherTr.localEulerAngles = _stackAngle;
            StackedObjList.Add(other.gameObject);
            TargetMatChanger();
            CheckCameraPos();
            StartCoroutine(MakeObjectsBigger());
        });
    }
    
    private IEnumerator MakeObjectsBigger()
    {
        if (StackedObjList.Count <= 0) yield break;
        
        for (int i = 0; i < StackedObjList.Count; i++)
        {
            Vector3 currentScale = StackedObjList[i].transform.localScale;

            Vector3 targetScale = currentScale * 1.5f;

            int index = i;
            StackedObjList[index].transform.DOScale(targetScale, 0.2f).OnComplete(() =>
            {
                StackedObjList[index].GetComponent<MeshRenderer>().material = _targetMat;
                StackedObjList[index].transform.DOScale(Vector3.one * afterStackScaleFactor, 0.2f);
            });
            yield return new WaitForSeconds(0.05f);
        }
    }

    public void CheckCameraPos()
    {
        if (StackedObjList.Count < 11)
        {
            CameraController.Instance.MoveCamera(CameraController.Instance.cameraPosList[0]);
            return;

        }
        if (StackedObjList.Count is > 11 and < 15)
        {
            CameraController.Instance.MoveCamera(CameraController.Instance.cameraPosList[1]);
            return;
        }
        if (StackedObjList.Count > 15)
        {
            CameraController.Instance.MoveCamera(CameraController.Instance.cameraPosList[2]);
        }
    }
    #endregion
}
