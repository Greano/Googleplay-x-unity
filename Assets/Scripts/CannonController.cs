using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
using System.Collections.Generic;

public class CannonController : MonoBehaviour
{
    public Transform barrel;
    public float barrelSpeed = 10f;
    public float barrelminrotation = -10f;
    public float barrelmaxrotation = 10f;

    public float turnspeed = 30f;
    public float turnminrotation = -30f;
    public float turnmaxrotation = 30f;

    [SerializeField] private Transform BombSpawnLocation;
    public GameObject BombPrefab;
    public float ProjectileForce = 30f;
    public float ProjectileTorque = 20f;

    public float ReloadTime = 1f;
    private float _ReloadExpectedtime = 0f;


    [SerializeField] private Transform RightFrontWheel;
    [SerializeField] private Transform LeftFrontWheel;
    [SerializeField] private Transform RightRearWheel;
    [SerializeField] private Transform LeftRearWheel;
    
    [SerializeField] private Transform AimDirection;
    
    private Vector2 _aimrotation = Vector2.zero;
    private Vector2 _turnrotation;
    
    private Vector2 _turnWheel = Vector2.zero;
    private Vector2 _aimDirection = Vector2.zero;
    private SimpleControls _simpleControls;
    
    public bool BarrelClamping = true;
    public bool TurnClamping = true;
    
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
        _simpleControls.gameplay.fire.performed += ctx => { Fire(); };
        _simpleControls.gameplay.MainMenu.performed += ctx => { MenuManager.Instance.ToggleMenu(); };
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

        _ReloadExpectedtime += Time.deltaTime;
    }

    private void aim(float aim)
    {
        if (BarrelClamping)
        {
            float scaledaimspeed = barrelSpeed * Time.deltaTime;
            float aimamount = aim * scaledaimspeed;
            _aimrotation.x = Mathf.Clamp(_aimrotation.x + aimamount, barrelminrotation, barrelmaxrotation);
            barrel.localEulerAngles = _aimrotation;
        }
        else
        {
            _aimrotation.x = _aimrotation.x + aim;
            barrel.localEulerAngles = _aimrotation;
        }

    }

    private void turn(float turn)
    {
        if (TurnClamping)
        {
            float scaledturnspeed = turnspeed * Time.deltaTime;
            float turnamount = turn * scaledturnspeed;
            _turnrotation.y  = Mathf.Clamp(_turnrotation.y + turnamount, turnminrotation, turnmaxrotation);
            transform.eulerAngles = _turnrotation;
        }
        else
        {
            _turnrotation.y = _turnrotation.y + turn;
            transform.eulerAngles = _turnrotation;
        }

        _aimDirection.y = _turnrotation.y;
        AimDirection.localEulerAngles = _aimDirection * 5;
        _turnWheel.x = _turnrotation.y;
        //rotation Wheel
        LeftFrontWheel.localEulerAngles = _turnWheel;
        RightFrontWheel.localEulerAngles = -_turnWheel;
        LeftRearWheel.localEulerAngles = _turnWheel;
        RightRearWheel.localEulerAngles = -_turnWheel;
        
    }

    private void Fire()
    {
        if (_ReloadExpectedtime >= ReloadTime)
        {
            _ReloadExpectedtime = 0f;
            var cannonball = Instantiate(BombPrefab, BombSpawnLocation.position, BombSpawnLocation.rotation);

            var rb = cannonball.GetComponent<Rigidbody>();

            rb.AddTorque(Random.insideUnitSphere.normalized * ProjectileTorque, ForceMode.Impulse);
            rb.AddForce(cannonball.transform.forward * ProjectileForce, ForceMode.Impulse);

        }
    }

}
