using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _dragWhileGrounded;
    [SerializeField] private float _dragWhileMoving;

    private Rigidbody myRigidbody;
    private Vector3 _moveDirection = Vector3.zero;
    private bool _isMoving = false;
    private bool _isJumping = false;

    [Header("For other scripts to access")]
    public bool isGrounded = false;
    public bool limitSpeed = true;
    public bool inMenu = false;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Jumping
        if (isGrounded && _isJumping && !inMenu)
        {
            myRigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
        _isJumping = false;
    }

    private void FixedUpdate()
    {
        // Movement
        if (_isMoving && !inMenu)
        {
            Vector3 worldMoveDirection = _moveDirection.y * transform.forward + _moveDirection.x * transform.right;
            worldMoveDirection.Normalize();

            myRigidbody.linearVelocity += _acceleration * Time.fixedDeltaTime * worldMoveDirection;

            //float speedInDir = Vector3.Dot(myRigidbody.linearVelocity, worldMoveDirection); // current velocity along that direction
        }

        // Ground drag (slows horizontal velocity only)
        if (isGrounded)
        {
            if (_isMoving && !inMenu)
            {
                myRigidbody.linearDamping = _dragWhileMoving;
            }
            else
            {
                myRigidbody.linearDamping = _dragWhileGrounded;
            }
        }
        else
        {
            myRigidbody.linearDamping = 0;
        }

        if (limitSpeed)
        {
            Vector3 horizontalVelocity = new Vector3(myRigidbody.linearVelocity.x, 0f, myRigidbody.linearVelocity.z);
            if (horizontalVelocity.magnitude > _maxVelocity)
            {
                horizontalVelocity = Mathf.Max(horizontalVelocity.magnitude - (_acceleration * Time.fixedDeltaTime), _maxVelocity) * horizontalVelocity.normalized;
            }
            myRigidbody.linearVelocity = new Vector3(horizontalVelocity.x, myRigidbody.linearVelocity.y, horizontalVelocity.z);
        }
        print("Speed In Direction: " + myRigidbody.linearVelocity.magnitude);
    }

    // Movement Input
    private void OnEnable()
    {
        var map = InputSystem.actions;
        map.Enable();
        map.FindAction("Move").performed += MoveActionPerformed;
        map.FindAction("Move").canceled += MoveActionCancelled;
        map.FindAction("Jump").performed += JumpActionPerformed;
    }

    private void OnDisable()
    {
        var map = InputSystem.actions;
        var move = map.FindAction("Move");
        if (move != null)
        {
            move.performed -= MoveActionPerformed;
            move.canceled -= MoveActionCancelled;
        }
        var jump = map.FindAction("Jump");
        if (jump != null)
        {
            jump.performed -= JumpActionPerformed;
        }
    }

    private void MoveActionPerformed(InputAction.CallbackContext ctx)
    {
        _isMoving = true;
        _moveDirection = ctx.ReadValue<Vector2>();
    }

    private void MoveActionCancelled(InputAction.CallbackContext ctx)
    {
        _isMoving = false;
    }

    private void JumpActionPerformed(InputAction.CallbackContext ctx)
    {
        _isJumping = true;
    }
}
