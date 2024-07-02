using UnityEngine;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(PlayerStatus))]
[RequireComponent(typeof(MobAttack))]

public class PlayerController : MonoBehaviour
{
    [SerializeField] private PlayerData _data;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpPower = 6;
    [SerializeField] private Animator animator;
    //[SerializeField] private float jumpCooltime = 1;
    [SerializeField] private GameObject _camera;
    [SerializeField] private Vector3 _position;

    private string PositionX = "PositionX";
    private string PositionY = "PositionY";
    private string PositionZ = "PositionZ";

    private CharacterController _characterController;
    private Transform _transform;
    private Vector3 _moveVelocity;
    private Vector3 move;
    private int _jumpCount = 0;//二段ジャンプできるカウント.
    private PlayerStatus _status;
    private MobAttack _attack;

    // Start is called before the first frame update
    void Start()
    {
        _characterController = GetComponent<CharacterController>();
        _transform = transform;
        _status = GetComponent<PlayerStatus>();
        _attack = GetComponent<MobAttack>();
        Positioning();
    }

    // Update is called once per frame
    private void Update()
    {
        Debug.Log(_characterController.isGrounded ? "地上にいます" : "空中にいます");

        if (Input.GetButtonDown("Fire1"))
        {
           _status.Combo();
        }

        if (_status.IsMovable)
        {

            moveSpeed = Input.GetButton("Run 1") || Input.GetButton("Run 2") ? 6 : 3;
            _moveVelocity.x = Input.GetAxis("Horizontal");
            _moveVelocity.z = Input.GetAxis("Vertical");
            move = moveSpeed * new Vector3(_moveVelocity.x, 0, _moveVelocity.z).normalized;
            _characterController.Move(move * Time.deltaTime);
            _transform.LookAt(_transform.position + move);
            Debug.Log("クエリちゃんの速さは" + move.magnitude + "です");
            
        }
        else
        {
            _moveVelocity.x = 0;
            _moveVelocity.z = 0;
        }

        if (_status.IsAttackable)
        {
            _moveVelocity = Vector3.zero;
        }

        if (_characterController.isGrounded)
        {
            _jumpCount = 0;
            _moveVelocity.y = -2f;
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("ジャンプ!!");  
                _moveVelocity.y = jumpPower;
                _jumpCount++;
            }
        }
        else
        {
            if(_jumpCount < 2 && Input.GetButtonDown("Jump"))
            {
                _moveVelocity.y = jumpPower;
                _jumpCount++;
            }
            else if(_jumpCount == 2 && Input.GetButtonDown("Jump")){
                _jumpCount++;
            }
            else if (_jumpCount > 2)
            {
                _moveVelocity.y = 0;
                if (Input.GetButtonDown("Jump"))
                {
                    animator.SetTrigger("isCanseled");
                    _jumpCount = 2;
                }
            }
            else
            {
                _moveVelocity.y += Physics.gravity.y * Time.deltaTime;
            }
        }
        _characterController.Move(_moveVelocity * Time.deltaTime);
        animator.SetInteger("JumpCount", _jumpCount);
        animator.SetFloat("Jump", _moveVelocity.y);
        animator.SetBool("isGrounded", _characterController.isGrounded);
        animator.SetFloat("MoveSpeed", move.magnitude);
    }

    public float GetPositionX
    {
        get
        {
            return _transform.position.x;
        }
    }

    public float GetPositionY
    {
        get
        {
            return _transform.position.y;
        }
    }

    public float GetPositionZ
    {
        get
        {
            return _transform.position.z;
        }
    }

    void Positioning()
    {
        if (PlayerPrefs.HasKey(PositionX) && PlayerPrefs.HasKey(PositionY) && PlayerPrefs.HasKey(PositionZ))
        {
            _position = _data.LoadPosition();
        }
        _position = new Vector3(_position.x, _position.y, _position.z);
        _transform.position = _position;
    }
}