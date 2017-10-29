using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInputScript : MonoBehaviour
{
    //groundcheck
    public Transform GroundCheck;

    public Vector3 spawnPosition;
    public LayerMask whatIsGround;
    public Text CoinText;

    //stats
    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 700f;
    [SerializeField] private bool _facingRight = true;
    [SerializeField] private bool _gravityReversed = false;
    [SerializeField] private int _coin = 0;

    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private bool _isWalking;
    private bool _grounded = false;
    private float _groundRadius = 0.2f;
    private Health _characterUi;
    private PlayerShoot _characterShoot;

    

	// Use this for initialization
	void Start () {
        //get the components of the player
	    _rigidbody = GetComponent<Rigidbody2D>();
	    _animator = GetComponent<Animator>();
	    _characterUi = GetComponent<Health>();
	    _characterShoot = GetComponent<PlayerShoot>();
        _rigidbody.gravityScale = 1;
	    spawnPosition = gameObject.transform.position;
	}

    void Update()
    {
        CoinText.text = _coin.ToString();
        if (transform.position.y < -50f)
        {
            Die();
        }
    }


	// Update is called once per frame
	void FixedUpdate ()
	{
        //true or false if ground is hit
	    _grounded = Physics2D.OverlapCircle(GroundCheck.position, _groundRadius, whatIsGround);
        //get the horizontal value of unity
	    float horizontValue = Input.GetAxis("Horizontal");

        //check if walking
	    if (_rigidbody.velocity.magnitude > 0.01 && _grounded)
	    {
	        _isWalking = true;
	    }
	    else
	    {
	        _isWalking = false;
	    }
        //set animator value to running value for animations
        _animator.SetBool("IsRunning",_isWalking);
        
        //add velocity to the rigidbody in the move direction
        _rigidbody.velocity = new Vector2(horizontValue * _speed,_rigidbody.velocity.y);

        //flip the player when going into negative direction
	    if (horizontValue > 0 && !_facingRight)
	    {
	        FlipXAxis();
	    }else if (horizontValue < 0 && _facingRight)
	    {
	        FlipXAxis();
	    }


	    if (Input.GetKeyDown(KeyCode.Space) && _grounded)
	    {
            //trigger jump animation
	        _animator.SetTrigger("IsJumping");
            //add jump force to the y axis of the rigidbody
            _rigidbody.AddForce(new Vector2(0,_jumpForce));
	    }
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


    private void Die()
    {
        //restart
        SceneManager.LoadScene("_MainScene");
        gameObject.transform.position = spawnPosition;
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

    void OnTriggerEnter2D(Collider2D collider)
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
    }
}
