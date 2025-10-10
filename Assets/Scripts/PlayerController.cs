using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float _acceleration; 
    [SerializeField] private float _maxVelocity; // max spd the player can go, so change the velocity
    [SerializeField] private float _jumpPower;
    [SerializeField] private float _dragWhileGrounded;
    [SerializeField] private float _dragWhileMoving;
    [SerializeField] private GameObject _playerSprite;
    public Animator _animator;

    //changes - jolin
    [SerializeField] private float _climbValue;
    [SerializeField] private float _dropValue;

    private Rigidbody myRigidbody;
    private Vector3 _moveDirection = Vector3.zero;
    private bool _isMoving = false;
    private bool _isJumping = false;

    //changes - jolin
    [SerializeField] private float _holdTimer = 2f;
    private float _holdCountdown;
    private bool _climb = false;
    private bool _drop = false;
    private bool _ifOnLedge = false;
    private bool _isInMud = false;
    private bool _isInWater = false;
    private bool _isSpacePressed = false;

    [Header("For other scripts to access")]
    public bool _swing = false; //changes - jolin
    public bool isGrounded = false;
    public bool limitSpeed = true;
    public bool inMenu = false;

    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        _holdCountdown = _holdTimer;
    }

    private void Update()
    {
        // Jumping
        if (isGrounded && _isJumping && !inMenu)
        {
            myRigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            _animator.SetTrigger("Jump");
        }
        _isJumping = false;

        //changes - jolin
        if (_climb && !inMenu && _ifOnLedge)
        {
            myRigidbody.AddForce(Vector3.up * _climbValue, ForceMode.Impulse);
            _isSpacePressed = true;
            _holdCountdown = _holdTimer;
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

        if (_isSpacePressed)
        {
            myRigidbody.constraints &= ~RigidbodyConstraints.FreezePosition;
            myRigidbody.constraints = RigidbodyConstraints.FreezeRotation;

            _holdCountdown -= Time.deltaTime;

            if (_holdCountdown <= 0)
            {
                _isSpacePressed = false;
            }
        }

        if (_isMoving && !inMenu)
        {
            float val;
            if (_ifOnLedge && !_isSpacePressed)
            {
                _animator.SetBool("isOnLedge", true);
                val = 0;
                //myRigidbody.useGravity = false;
                myRigidbody.constraints = RigidbodyConstraints.FreezePosition;
            }
            else
            {
                _animator.SetBool("isOnLedge", false);
                val = _acceleration;
                //myRigibody.constraints &= ~RigidbodyConstraints.FreezePositionY;
                //myRigidbody.useGravity = true;
            }

            //check for mud / water
            if (_isInMud) val = 5;
            if (_isInWater) val = 8;

            Vector3 worldMoveDirection = _moveDirection.y * transform.forward + _moveDirection.x * transform.right;
            worldMoveDirection.Normalize();
            myRigidbody.linearVelocity += val * Time.fixedDeltaTime * worldMoveDirection;
        }

        if (myRigidbody.linearVelocity.x > 0)
        {
            _playerSprite.transform.localScale = new Vector3(1, 1, 1);
        }
        else/* if (myRigidbody.linearVelocity.x > 0)*/
        {
            _playerSprite.transform.localScale = new Vector3(-1, 1, 1);
        }

        // Ground drag (slows horizontal velocity only)
        if (isGrounded)
        {
            if (_isMoving)
            {
                print("iyvyid");
                _animator.SetBool("isWalking", true);
                myRigidbody.linearDamping = _dragWhileMoving;
            }
            else
            {
                _animator.SetBool("isWalking", false);
                myRigidbody.linearDamping = _dragWhileGrounded;
            }
            _animator.SetBool("isGrounded", true);
        }
        else
        {
            _animator.SetBool("isGrounded", false);
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

    public void ToggleInMenu(bool inmenu)
    {
        inMenu = inmenu;
    }

    // Movement Input
    private void OnEnable()
    {
        var map = InputSystem.actions;
        map.Enable();
        map.FindAction("Move").performed += MoveActionPerformed;
        map.FindAction("Move").canceled += MoveActionCancelled;
        map.FindAction("Jump").performed += JumpActionPerformed;

        //changes - jolin
        map.FindAction("Climb").performed += climbActionPerformed;
        map.FindAction("JumpDown").performed += dropActionPerformed;
        map.FindAction("Swing").performed += swingActionPerformed;
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
        var swing = map.FindAction("Swing");
        if (swing != null)
        {
            swing.performed -= swingActionPerformed;
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
        _isSpacePressed = true;
    }

    private void dropActionPerformed(InputAction.CallbackContext ctx)
    {
        _drop = true;
    }

    private void swingActionPerformed(InputAction.CallbackContext ctx)
    {
        _swing = true;
    }

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

    private void OnCollisionEnter(Collision collision)
    {

        //if on mud and water, slower
        if (collision.gameObject.CompareTag("Mud"))
            _isInMud = true;
        else
            _isInMud = false;

        if (collision.gameObject.CompareTag("Water"))
            _isInWater = true;
        //if not, faster
        else
            _isInWater = false;
    }

    public void ResetValues()
    {
        myRigidbody.linearVelocity = Vector3.zero;
    }
}
