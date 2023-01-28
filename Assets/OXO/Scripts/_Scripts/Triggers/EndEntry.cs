using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

public class EndEntry : MonoBehaviour
{
    private float _distance;
    private float _targetNumber;
    private bool _canGoingUp;
    private readonly float _levelEndSpeed = 0.6f;
    
    [SerializeField] private MeshRenderer playerMesh;
    
    [Header("About Cameras: "), Space]
    [SerializeField] private GameObject gamePlayCamera;

    
    [SerializeField] private GameObject finishPoint;

    private List<GameObject> StackedObj => StackManager.Instance.StackedObjList;
    private FinishRayHitter Hitter => finishPoint.GetComponent<FinishRayHitter>();

    private async void Start()
    {
        await Task.Delay(50);
        
        gamePlayCamera = GameObject.FindWithTag("BurasiCamera");
    }

    private void Update()
    {
        LetsGo();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerMesh = other.GetComponent<MeshRenderer>();
            
            CameraSettings();
            FinishEntry();
            FinishButtonSetter();
            StartCoroutine(GoingUpTime());
        }
    }

    private void FinishEntry()
    {
        Swerve.Instance.EndState();
        playerMesh.enabled = false;
    }

    private void CameraSettings()
    {
        CameraController.Instance.FinalCameraPos();
    }

    private void FinishButtonSetter()
    {
        for (int i = 0; i < StackedObj.Count; i++)
        {
            GameObject button = StackedObj[i];
            
            button.transform.SetParent(finishPoint.transform);
            button.transform.localPosition = Vector3.zero;

            button.transform.DORotate(new Vector3(-90, 140, 45), 0);

            Vector3 localScale = button.transform.localScale;

            button.transform.DOScale(Vector3.one * 2.3f, 0);
            button.transform.DOScale(new Vector3(localScale.x * 5.2f, localScale.y * 5.3f, localScale.z * 10.4f), 0f);
            
            button.transform.localPosition = new Vector3(button.transform.localPosition.x,
                button.transform.localPosition.y - _distance, button.transform.localPosition.z);
            
            _distance += 0.6f;
        }
    }

    private IEnumerator GoingUpTime()
    {
        yield return new WaitForSeconds(1.2f);
        TargetSetter();       
        _canGoingUp = true;
        Hitter.enabled = true;
    }

    private void LetsGo()
    {
        if(!_canGoingUp) return;
        gamePlayCamera.transform.parent = finishPoint.transform;
        
        if (finishPoint.transform.localPosition.y <= _targetNumber)
        {
            finishPoint.transform.Translate(Vector3.up * _levelEndSpeed);
        }
        else
        {
            StartCoroutine(LevelEndVisualiser());
        }
    }

    private IEnumerator LevelEndVisualiser()
    {
        yield return new WaitForSeconds(0.3f);
        switch (StackedObj.Count)
        {
            case 0:
                UIManager.Instance.LevelEndVisualiser(false, "TryAgain");
                break;
            case > 0 and <= 4:
                UIManager.Instance.LevelEndVisualiser(false, "KeepOn");
                break;
            case > 4 and <= 10:
                UIManager.Instance.LevelEndVisualiser(true, "Great");
                break;
            case > 10 and <= 15:
                UIManager.Instance.LevelEndVisualiser(true, "Amazing");
                break;
            case > 15:
                UIManager.Instance.LevelEndVisualiser(true, "Awesome");
                break;
        }
    }
    private void TargetSetter()
    {
        _targetNumber = StackedObj.Count * 2;
    }
}
