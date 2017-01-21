using UnityEngine;

public class PlayerController : MonoBehaviour
{
    #region fields & properties
    [SerializeField]
    private Transform _model;
    [SerializeField]
    private AnimationCurve _tiltCurve;


    private Rigidbody2D _rigidbody;

    public float WaterPressure()
    {
        float yPos = transform.position.y;
        yPos *= yPos;

        const float maxPressure = 100;
        return Mathf.Clamp(maxPressure - yPos, 0, maxPressure);
    }

    private InputDirection _input
    {
        get
        {
            return new InputDirection(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }
    }
    #endregion

    #region unity messages
    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _rigidbody.gravityScale = 0;
    }

    private void Update()
    {
        MovePlayer(_input);
        ApplyTilt();
    }
    #endregion

    private void MovePlayer(InputDirection direction)
    {
        _rigidbody.AddForce(CalculateDirectionVector(direction));
    }

    private Vector2 CalculateDirectionVector(InputDirection direction)
    {
        var directionVector = new Vector2(direction.X, direction.Y).normalized;

        return directionVector;
    }

    private void ApplyTilt()
    {
        _model.rotation = CalculateTilt();
    }

    private Quaternion CalculateTilt()
    {
        var angle = _tiltCurve.Evaluate(_rigidbody.velocity.y);
        return Quaternion.Euler(0, 0, angle);
    }
}