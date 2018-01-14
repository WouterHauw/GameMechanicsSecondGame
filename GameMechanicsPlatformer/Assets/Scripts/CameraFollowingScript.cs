using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowingScript : MonoBehaviour
{
    [SerializeField]private GameObject _player;

    [SerializeField]private float _smoothTimeX;
    [SerializeField]private float _smoothTimeY;

    private Vector2 _velocity;
	// Use this for initialization
	void Start () {
		_player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    var posX = Mathf.SmoothDamp(transform.position.x, _player.transform.position.x, ref _velocity.x, _smoothTimeX);
	    var posY = Mathf.SmoothDamp(transform.position.y, _player.transform.position.y +2f, ref _velocity.y, _smoothTimeY);

        transform.position = new Vector3(posX,posY,transform.position.z);


    }
}
