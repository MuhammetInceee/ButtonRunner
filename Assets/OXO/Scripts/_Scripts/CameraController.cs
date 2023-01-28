using System.Collections.Generic;
using DG.Tweening;
using MuhammetInce.DesignPattern.Singleton;
using UnityEngine;

public class CameraController : LazySingleton<CameraController>
{
    private readonly Vector3 _finalCameraPos = new Vector3(-0.44f, 4.45f, -8f);

    public List<Vector3> cameraPosList;
    private void Start()
    {
        transform.localPosition = cameraPosList[0];
    }

    public void MoveCamera(Vector3 targetPos)
    {
        transform.DOLocalMove(targetPos, 0.4f);
    }

    public void FinalCameraPos()
    {
        transform.DOLocalMove(_finalCameraPos, 1f);
        transform.localEulerAngles = Vector3.zero;
    }

}