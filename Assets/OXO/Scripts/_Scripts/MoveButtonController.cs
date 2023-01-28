using Dreamteck.Splines;
using UnityEngine;

public class MoveButtonController : MonoBehaviour
{
    private float _posX;
    private float _clampX;
    private float _forSmooth;
    private float itDoesntMatter = 3f;
    private GameObject _player;
    
    [SerializeField] private float speed;
    [SerializeField] private float minDistanceForMove;
    private SplineFollower Follower => GetComponent<SplineFollower>();

    private float RandomValue => Random.Range(-2f, 2f);
    private TrailRenderer TrailRenderer => transform.GetChild(0).GetComponent<TrailRenderer>();
    private ButtonRandomMaterial randomMaterial => GetComponent<ButtonRandomMaterial>();

    private Vector2 Offset
    {
        get => Follower.motion.offset;
        set => Follower.motion.offset = value;
    }

    void Start()
    {
        Follower.followSpeed = 0;
        Offset = new Vector2(RandomValue, 1.3f);
        _player = GameObject.FindWithTag("Player");
        TrailRenderer.startColor = randomMaterial.targetMat.color;
 
    }

    void Update()
    {
        if(Vector3.Distance(transform.position, _player.transform.position) > minDistanceForMove) return;

        Follower.followSpeed = speed;
        _clampX = Mathf.Clamp(Offset.x, -2f, 2f);
        _forSmooth = Mathf.Lerp(Offset.x, _clampX + itDoesntMatter, 0.01f);

        Offset = new Vector2(_forSmooth, Offset.y);
        if (_forSmooth >= 2)
        {
            itDoesntMatter = -itDoesntMatter;
        }

        if (_forSmooth <= -2)
        {
            itDoesntMatter = -itDoesntMatter;
        }

        RoadEndCheck();
    }

    private void RoadEndCheck()
    {
        if (Follower.GetPercent() == 1)
        {
            Destroy(gameObject);
        }
    }
}
