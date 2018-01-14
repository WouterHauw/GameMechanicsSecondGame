using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInputScript : MonoBehaviour
{
    //groundcheck
    public Transform groundcheck;

    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool _grounded;
    public Vector3 spawnPosition;
    public Text CoinText;

    //stats
    [SerializeField] private float _speed = 5f;

    [SerializeField] private float _jumpForce = 700f;
    [SerializeField] private bool _facingRight = true;
    [SerializeField] private bool _gravityReversed = false;
    [SerializeField] private int _coin = 0;

    private bool _doubleJumped;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private bool _isWalking;
    private Health _characterUi;
    private PlayerShoot _characterShoot;
    private bool _canDoubleJump = false;



    // Use this for initialization
    private void Start()
    {
        //get the components of the player
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _characterUi = GetComponent<Health>();
        _characterShoot = GetComponent<PlayerShoot>();
        _rigidbody.gravityScale = 1;
        spawnPosition = gameObject.transform.position;
        _canDoubleJump = false;
    }

    private void Update()
    {
        if (_grounded)
        {
            _doubleJumped = false;
        }
        if (Input.GetKeyDown(KeyCode.Space) && _grounded)
        {
            Jump();
        }
        if (_canDoubleJump)
        {
            if (Input.GetKeyDown(KeyCode.Space) && !_doubleJumped && !_grounded)
            {
                Jump();
                _doubleJumped = true;
            }
        }
        //get the horizontal value of unity
        var horizontValue = Input.GetAxis("Horizontal");

        //check if walking
        _animator.SetFloat("Speed", Mathf.Abs(horizontValue));

        //add velocity to the rigidbody in the move direction
        _rigidbody.velocity = new Vector2(horizontValue * _speed, _rigidbody.velocity.y);

        //flip the player when going into negative direction
        if (horizontValue > 0 && !_facingRight)
        {
            FlipXAxis();
        }
        else if (horizontValue < 0 && _facingRight)
        {
            FlipXAxis();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }

        CoinText.text = _coin.ToString();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        //check ground
        _grounded = Physics2D.OverlapCircle(groundcheck.position, groundCheckRadius, whatIsGround);
    }

    private void FlipXAxis()
    {
        //oposite direction
        _facingRight = !_facingRight;

        //get local scale
        var theScale = transform.localScale;

        //flip on x axis
        theScale.x *= -1;

        //apply that to the local scale
        transform.localScale = theScale;

    }


    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Saw"))
        {
            other.gameObject.SetActive(false);
            _characterUi.DealDamage(6);
            _characterShoot.CurrentAmmoBarValue = 0;
        }

    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Heart"))
        {
            collider.gameObject.SetActive(false);
            _characterUi.AddHealth(10);
        }
        if (collider.gameObject.CompareTag("Coin"))
        {
            _coin++;
            collider.gameObject.SetActive(false);
            _characterShoot.Ammunition += 1;

        }
        if (collider.gameObject.CompareTag("Spike"))
        {
            collider.gameObject.SetActive(false);
            _characterUi.DealDamage(6);
            _characterShoot.CurrentAmmoBarValue = 0;
        }
        if (collider.gameObject.CompareTag("DoubleJump"))
        {
            _canDoubleJump = true;
        }
    }

    private void Jump()
    {
        //trigger jump animation
        _animator.SetTrigger("IsJumping");
        //add jump force to the y axis of the rigidbody
        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }
}
