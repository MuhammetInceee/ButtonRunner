using System.Collections;
using System.Threading.Tasks;
using DG.Tweening;
using Dreamteck.Splines;
using UnityEngine;


public class SpiralTrigger : MonoBehaviour
{
    [SerializeField] private bool isStart;
    [SerializeField] private GameObject gameplayCamera;
    [SerializeField] private SplineComputer spiralComputer;
    [SerializeField] private SplineComputer roadFollower;
    [SerializeField] private GameObject spiralCamera;
    [SerializeField] private GameObject fog;
    private GameObject Player => Swerve.Instance.gameObject;
    private GameObject Needle => StackManager.Instance.gameObject;

    private SplineFollower PlayerFollower => Player.GetComponents<SplineFollower>()[0];
    private SplineFollower PlayerSecondFollower => Player.GetComponents<SplineFollower>()[1];
    private SplineFollower PlayerThirdFollower => Player.GetComponents<SplineFollower>()[2];

    private SplineFollower NeedleFollower => Needle.GetComponents<SplineFollower>()[0];
    private SplineFollower NeedleSecondFollower => Needle.GetComponents<SplineFollower>()[1];
    private SplineFollower NeedleThirdFollower => Needle.GetComponents<SplineFollower>()[2];
    
    private void OnEnable()
    {
        gameplayCamera = GameObject.FindWithTag("BurasiCamera");
        fog = GameObject.Find("Fog");
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            if (isStart)
            {
                spiralCamera.transform.position = gameplayCamera.transform.position;
                gameplayCamera.SetActive(false);
                spiralCamera.SetActive(true);

                PlayerFollower.enabled = false;
                PlayerSecondFollower.spline = spiralComputer;
                PlayerSecondFollower.SetPercent(0);
                
                NeedleFollower.enabled = false;
                NeedleSecondFollower.spline = spiralComputer;
                NeedleSecondFollower.SetPercent(0);
                
                PlayerSecondFollower.transform.DOMove(transform.position, 0.1f).OnComplete(() =>
                {
                    PlayerSecondFollower.enabled = true;
                    NeedleSecondFollower.enabled = true;
                });

                
                fog.transform.parent = spiralCamera.transform;
                Swerve.Instance.canMove = false;
            }
            else
            { 
                PlayerSecondFollower.enabled = false;
                PlayerThirdFollower.spline = roadFollower;
                PlayerThirdFollower.enabled = true;
                Swerve.Instance.playerFollower = Swerve.Instance.GetComponents<SplineFollower>()[2];
                
                NeedleSecondFollower.enabled = false;
                NeedleThirdFollower.spline = roadFollower;
                NeedleThirdFollower.enabled = true;
                Swerve.Instance._needleFollower = StackManager.Instance.GetComponents<SplineFollower>()[2];
                
                gameplayCamera.SetActive(true);
                spiralCamera.SetActive(false);
                fog.transform.parent = gameplayCamera.transform;
                Swerve.Instance.canMove = true;
            }
        }
    }
}
