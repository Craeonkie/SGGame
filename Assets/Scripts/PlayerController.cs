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
    [SerializeField] private LedgeClimbScript _ledgeClimb;
    [SerializeField] private HouseManager _houseManager;
    public Animator _animator;

    //changes - jolin
    [SerializeField] private float _climbValue;
    [SerializeField] private float _dropValue;

    private Rigidbody myRigidbody;
    private Vector3 _moveDirection = Vector3.zero;
    private bool _isMoving = false;
    private bool _isJumping = false;

    //new changes - jolin
    [SerializeField] private Collider ledgeChecker;
    [SerializeField] private ParticleScript _particleScripts;
    [SerializeField] private GameObject _particle;

    //changes - jolin
    [SerializeField] private float _holdTimer = 2f;
    private float _holdCountdown;
    private bool _climb = false;
    private bool _drop = false;
    private bool _ifOnLedge = false;
    private bool _isInMud = false;
    private bool _isInWater = false;
    private bool _isSpacePressed = false;
    private bool _interact = false;
    private float _interactTimer = 1f;

    [Header("For other scripts to access")]
    public bool _swing = false; //changes - jolin
    public bool isGrounded = false;
    public bool limitSpeed = true;
    public bool inDialogue = false;
    public bool inMenu = false;

    //changes - Yu Chi
    private bool _isStillInAir = false;
    //changes - yu chi
    [Header("Sound Effects")]
    public AudioSource audioSource;
    public AudioClip[] audioClip;
    private void Start()
    {
        myRigidbody = GetComponent<Rigidbody>();
        _holdCountdown = _holdTimer;

        //changes - Yu Chi
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        // Jumping
        if (isGrounded && _isJumping && !inDialogue)
        {
            myRigidbody.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
            _animator.SetTrigger("Jump");
        }
        _isJumping = false;

        //changes - jolin
        if (_climb && !inDialogue && _ifOnLedge)
        {
            myRigidbody.AddForce(Vector3.up * _climbValue, ForceMode.Impulse);
            _isSpacePressed = true;
            _holdCountdown = _holdTimer;
        }
        _climb = false;

        if (_drop && !inDialogue && _ifOnLedge)
        {
            //drop down
            myRigidbody.AddForce(Vector3.up * -_dropValue, ForceMode.Impulse);
        }
        _drop = false;

        if (_ledgeClimb.ifOnLedge)
        {
            _animator.SetBool("isOnLedge", true);
            _ifOnLedge = true;
        }
        else
        {
            _animator.SetBool("isOnLedge", false);
            _ifOnLedge = false;
        }

        //change - Yu Chi
        if (!isGrounded && !_isStillInAir)
        {
            audioSource.PlayOneShot(audioClip[4]);
            _isStillInAir = true;
        }

        if (_interact)
        {
            _interactTimer -= Time.deltaTime;
            if (_interactTimer < 0)
            {
                _interactTimer = 0.5f;
                _interact = false;
            }
        }

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

        if (_isMoving && !inDialogue)
        {
            float val;
            if (_ifOnLedge && !_isSpacePressed)
            {
                val = 0;
                //myRigidbody.useGravity = false;
                myRigidbody.constraints = RigidbodyConstraints.FreezePosition;
            }
            else
            {
                val = _acceleration;
                //myRigibody.constraints &= ~RigidbodyConstraints.FreezePositionY;
                //myRigidbody.useGravity = true;
            }

            //check for mud / water
            if (_isInMud) 
            { 
                val = 5;
                _particleScripts.onMud();
            }
            else if (_isInWater)
            {
                val = 8;
                _particleScripts.onWater();
            }
            else  val = _acceleration;

                Vector3 worldMoveDirection = _moveDirection.y * transform.forward + _moveDirection.x * transform.right;
            worldMoveDirection.Normalize();
            myRigidbody.linearVelocity += val * Time.fixedDeltaTime * worldMoveDirection;
        }

        if (myRigidbody.linearVelocity.x > 0)
        {
            _playerSprite.transform.localScale = new Vector3(1, 1, 1);
            _particleScripts.flipParticle(0);
        }
        else/* if (myRigidbody.linearVelocity.x > 0)*/
        {
            _playerSprite.transform.localScale = new Vector3(-1, 1, 1);
            _particleScripts.flipParticle(180);
        }

        // Ground drag (slows horizontal velocity only)
        if (isGrounded)
        {
            if (_isMoving)
            {
                //print("iyvyid");
                _animator.SetBool("isWalking", true);
                myRigidbody.linearDamping = _dragWhileMoving;
                _particle.gameObject.SetActive(true);
            }
            else
            {
                _animator.SetBool("isWalking", false);
                myRigidbody.linearDamping = _dragWhileGrounded;
                _particle.gameObject.SetActive(false);
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

    public void ToggleinDialogue(bool indialogue)
    {
        inDialogue = indialogue;
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
        map.FindAction("Interact").performed += interactActionPerformed;
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
        var interact = map.FindAction("Interact");
        if (interact != null)
        {
            interact.performed -= interactActionPerformed;
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

    private void interactActionPerformed(InputAction.CallbackContext ctx)
    {
        _interact = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_interactTimer > 0)
        {
            if (_interact)
            {
                if (other.gameObject.CompareTag("EnterHome"))
                {
                    if (other.gameObject.name == "1")
                    {
                        _houseManager.enterHouse(0);
                    }
                    if (other.gameObject.name == "2")
                    {
                        _houseManager.enterHouse(1);
                    }
                    if (other.gameObject.name == "3")
                    {
                        _houseManager.enterHouse(2);
                    }
                }
                if (other.gameObject.CompareTag("ExitHouse"))
                {
                    if (other.gameObject.name == "1")
                    {
                        _houseManager.exitHouse(0);
                    }
                    if (other.gameObject.name == "2")
                    {
                        _houseManager.exitHouse(1);
                    }
                    if (other.gameObject.name == "3")
                    {
                        _houseManager.exitHouse(2);
                    }
                }
            }
        }
        
    }

    //changes - jolin this whole thang
    private void OnCollisionEnter(Collision collision)
    {

        //if on mud and water, slower
        if (collision.gameObject.CompareTag("Mud"))
        { 
            _isInMud = true;
            audioSource.PlayOneShot(audioClip[1]); //changes - Yu Chi
        }
        //if not, faster
        else 
        { 
            _isInMud = false;
        }

        if (collision.gameObject.CompareTag("Water"))
        {
            //changes - Yu Chi
            if (!_isInWater)
                audioSource.PlayOneShot(audioClip[2]);
            else
                audioSource.PlayOneShot(audioClip[3]);

            _isInWater = true;

        }
        //if not, faster
        else
            _isInWater = false;

        if (collision.gameObject.CompareTag("House"))
        {
            audioSource.PlayOneShot(audioClip[6]);
        }

        if (!collision.gameObject.CompareTag("Mud") && (!collision.gameObject.CompareTag("Water")) &&
            (!collision.gameObject.CompareTag("Interactable")) && (!collision.gameObject.CompareTag("Ledge")))
        {
            if (isGrounded)
            {
                _particleScripts.onGrass();
                audioSource.PlayOneShot(audioClip[0]);
                _isStillInAir = false;
            }
            else
                _particleScripts.onDirt();
        }
    }

    //added
    public void ResetValues()
    {
        myRigidbody.linearVelocity = Vector3.zero;
    }

}
