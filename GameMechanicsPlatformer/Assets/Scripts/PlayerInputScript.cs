using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputScript : MonoBehaviour
{
    public Transform GroundCheck;
    public LayerMask whatIsGround;

    [SerializeField] private float _speed = 5f;
    [SerializeField] private float _jumpForce = 700f;
    [SerializeField] private bool _facingRight = true;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private bool _isWalking;
    private bool _grounded = false;
    private float _groundRadius = 0.1f;

	// Use this for initialization
	void Start () {
        //get the components of the player
	    _rigidbody = GetComponent<Rigidbody2D>();
	    _animator = GetComponent<Animator>();
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
	        Flip();
	    }else if (horizontValue < 0 && _facingRight)
	    {
	        Flip();
	    }


	    if (Input.GetKeyDown(KeyCode.Space) && _grounded)
	    {
            //trigger jump animation
	        _animator.SetTrigger("IsJumping");
            //add jump force to the y axis of the rigidbody
            _rigidbody.AddForce(new Vector2(0,_jumpForce));
	    }
	}

    private void Flip()
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
}
