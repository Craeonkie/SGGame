using TreeEditor;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using UnityEngine.UIElements;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxVelocity;
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _dragWhileGrounded;
    [SerializeField] private float _dragWhileMoving;

    //changes - jolin
    [SerializeField] private float _climbValue;
    [SerializeField] private float _dropValue;

    private Rigidbody myRigidbody;
    private Vector3 _moveDirection = Vector3.zero;
    private bool _isMoving = false;
    private bool _isJumping = false;

    //changes - jolin
    private bool _climb = false;
    private bool _drop = false;

    [Header("For other scripts to access")]
    public bool isGrounded = false;
    public bool limitSpeed = true;
    public bool inMenu = false;

    //changes - jolin
    private bool _ifOnLedge = false;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Debug.Log("On ledge check: " + _ifOnLedge);
        // Jumping
        if (isGrounded && _isJumping && !inMenu)
        {
            myRigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
        }
        _isJumping = false;

        //changes - jolin
        if (_climb && !inMenu && _ifOnLedge)
        {
            myRigidbody.AddForce(Vector3.up * _climbValue, ForceMode.Impulse);
        }
        _climb = false;

        if (_drop && !inMenu && _ifOnLedge)
        {
            //drop down
            myRigidbody.AddForce(Vector3.up * -_dropValue, ForceMode.Impulse);
        }
        _drop = false;
    }

    private void FixedUpdate()
    {
        // Movement
            //changes made here - jolin
        if (_isMoving && !inMenu)
        {
            float val;
            if (_ifOnLedge)
            {
                val = 0;
                //myRigidbody.constraints = RigidbodyConstraints.FreezePositionY;
                myRigidbody.useGravity = false;
            }
            else
            {
                val = _acceleration;
                //myRigidbody.constraints &= ~RigidbodyConstraints.FreezePositionY;
                myRigidbody.useGravity = true;
            }
            Vector3 worldMoveDirection = _moveDirection.y * transform.forward + _moveDirection.x * transform.right;
            worldMoveDirection.Normalize();
            myRigidbody.linearVelocity += val * Time.fixedDeltaTime * worldMoveDirection;

            //float speedInDir = Vector3.Dot(myRigidbody.linearVelocity, worldMoveDirection); // current velocity along that direction
            //print("Speed In Direction: " + myRigidbody.linearVelocity.magnitude);
        }

        // Ground drag (slows horizontal velocity only)
        if (isGrounded)
        {
            if (_isMoving)
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
    }

    public void ToggleInMenu(bool inmenu)
    {
        inMenu = inmenu;
    }

    // Movement Input
    private void OnEnable()
    {
        var map = InputSystem.actions;
        map.FindAction("Move").performed += MoveActionPerformed;
        map.FindAction("Move").canceled += MoveActionCancelled;
        map.FindAction("Jump").performed += JumpActionPerformed;

        //changes - jolin
        map.FindAction("Climb").performed += climbActionPerformed;
        map.FindAction("JumpDown").performed += dropActionPerformed;
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

        //changes - jolin
        var climb = map.FindAction("Climb");
        if (climb != null)
        {
            climb.performed -= climbActionPerformed;
        }
        var drop = map.FindAction("JumpDown");
        if (drop != null)
        {
            drop.performed -= dropActionPerformed;
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

    //changes - jolin
    private void climbActionPerformed(InputAction.CallbackContext ctx)
    {
        _climb = true;
    }

    private void dropActionPerformed(InputAction.CallbackContext ctx)
    {
        _drop = true;
    }

    //changes - jolin
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ledge"))
        {
            _ifOnLedge = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        _ifOnLedge = false;
    }
}
