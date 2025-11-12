using UnityEngine;
using UnityEngine.InputSystem;

public class CannonController : MonoBehaviour
{
    public Transform barrel;
    public float barrelSpeed = 10f;
    public float barrelminrotation = -10f;
    public float barrelmaxrotation = 10f;

    public float turnspeed = 30f;
    public float turnminrotation = -30f;
    public float turnmaxrotation = 30f;
    
    private Vector2 _aimrotation = Vector2.zero;
    private Vector2 _turnrotation;
    private SimpleControls _simpleControls;
    
    private void Awake()
    {
        _simpleControls = new SimpleControls();
        _turnrotation = transform.localEulerAngles;
        turnminrotation += _turnrotation.y;
        turnmaxrotation += _turnrotation.y;
    }

    private void OnEnable()
    {
        _simpleControls.Enable();
    }    
    private void OnDisable()
    {
        _simpleControls.Disable();
    }

    private void Update()
    {
        Vector2 move = _simpleControls.gameplay.move.ReadValue<Vector2>();
        turn(move.x);
        aim(move.y);
    }

    private void aim(float aim)
    {
        float scaledaimspeed = barrelSpeed * Time.deltaTime;
        float aimamount = aim * scaledaimspeed;
        _aimrotation.x = Mathf.Clamp(_aimrotation.x + aimamount, barrelminrotation, barrelmaxrotation);
        barrel.localEulerAngles = _aimrotation;
    }

    private void turn(float turn)
    {
        float scaledturnspeed = turnspeed * Time.deltaTime;
        float turnamount = turn * scaledturnspeed;
        _turnrotation.y  = Mathf.Clamp(_turnrotation.y + turnamount, turnminrotation, turnmaxrotation);
        transform.eulerAngles = _turnrotation;
        
    }
}
