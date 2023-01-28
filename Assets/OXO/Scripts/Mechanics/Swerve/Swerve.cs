using Dreamteck.Splines;
using FIMSpace.FTail;
using MuhammetInce.DesignPattern.Singleton;
using UnityEngine;
using System.Threading.Tasks;

public enum PlayerState
{
    Move,
    Spiral,
    Stop,
    End
}
public class Swerve : LazySingleton<Swerve>
{
    private readonly float _springinessValue = 0.5f;

    public SplineComputer _comp;
    public bool canMove = true;
    private float _clampRot; 
    public GameObject _needle;
    public SplineFollower _needleFollower;
    public SplineFollower playerFollower;
    public TailAnimator2 _tailAnimator;

    public float sensitivity = 0.01f;
    public float clampValue = 2.5f;
    public float rotateClamp;
    public float lerp;
    public PlayerState playerState;
    [SerializeField] private float playerSpeed;


    private Vector2 Offset
    {
        get => _needleFollower.motion.offset;
        set => _needleFollower.motion.offset = value;
    }

    private Vector3 Rotator
    {
        get => _needleFollower.motion.rotationOffset;
        set => _needleFollower.motion.rotationOffset = value;
    }
    private Vector3 PlayerPos
    {
        get => transform.position;
        set => transform.position = value;
    }

    public bool IsDragging => _deltaMousePosition.x != 0;

    private async void Start()
    {
        _needleFollower = _needle.GetComponent<SplineFollower>();
        playerFollower = GetComponent<SplineFollower>();
        await Task.Delay(50);
        _comp = GameObject.Find("Computer").GetComponent<SplineComputer>();
        playerFollower.spline = _comp;
        _needleFollower.spline = _comp;
    }
    void Update()
    {
        VerticalParentMovement();
        VerticalNeedleMovement();
        Move();
        #region MobileInputCheck
        if (Input.GetMouseButtonDown(0)) FingerDown();
        if (Input.GetMouseButton(0)) FingerDrag();
        if (Input.GetMouseButtonUp(0)) FingerUp();
        #endregion
    }

    


    #region MobileInputFunctions

    private Vector2 _firstMousePosition;
    private Vector2 _lastMousePosition;
    private Vector2 _deltaMousePosition;
    private Vector2 _movementVector;

    void FingerDown()
    {
        _tailAnimator.Springiness = _springinessValue;
        _firstMousePosition = Input.mousePosition;
        playerState = PlayerState.Move;
    }

    private float posX;
    void FingerDrag()
    {
        PlayerRotator();
        _lastMousePosition = Input.mousePosition;
        _deltaMousePosition = _lastMousePosition - _firstMousePosition;
        _movementVector = _deltaMousePosition * sensitivity;
         posX = Mathf.Clamp(Offset.x + _movementVector.x, -clampValue, clampValue);
         _firstMousePosition = _lastMousePosition;    
    }

    void FingerUp()
    {
        if (transform.eulerAngles.y != 0)
        {
            Rotator = Vector3.zero;
        }
        
        _tailAnimator.Springiness = 0;
        playerState = PlayerState.Stop; 
    }

    #endregion
    
    private void VerticalParentMovement()
    {
        playerFollower.followSpeed = playerState == PlayerState.Move ? playerSpeed : 0;
    }

    private void VerticalNeedleMovement()
    {
        if (_needleFollower is null) return;
        _needleFollower.followSpeed = playerState == PlayerState.Move ? playerSpeed : 0;
    }
    
    public void Move()
    {
        if (playerState != PlayerState.Move) return;
        if(canMove == false) return;
        Offset = Vector2.MoveTowards(Offset, new Vector2(posX,Offset.y),lerp);
    }
    private void PlayerRotator()
    {
        if(playerState != PlayerState.Move) return;
        if(canMove == false) return;
        _clampRot = Mathf.Clamp(_deltaMousePosition.x, -rotateClamp, rotateClamp);
        float smooth = Mathf.Lerp(Rotator.y, _clampRot, 0.5f);
        Rotator = new Vector3(Rotator.x, smooth, Rotator.z);
    }

    public void EndState()
    {
        playerState = PlayerState.End;
        GetComponent<SplineFollower>().enabled = false;
        _needleFollower.enabled = false;
    }
    
    
}
