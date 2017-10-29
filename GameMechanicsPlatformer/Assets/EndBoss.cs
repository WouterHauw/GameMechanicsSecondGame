using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndBoss : MonoBehaviour
{
    public float ShootDelay = 8f;
    public int Health = 10;
    public float Step;
    public Text WinText;
    public Transform ShootPositionOne;
    public Transform ShootPositionTwo;
    public GameObject Spike;
    [SerializeField]private float _timeElapsed;

    public float Offset;
	// Use this for initialization
	void Start ()
	{
	    var InitialPosition = transform.position;

	    Offset = transform.position.y + transform.localScale.y;
        WinText.gameObject.SetActive(false);
	    _timeElapsed = 6;
	}

    void Update()
    {
        if (_timeElapsed > ShootDelay)
        {
            StartCoroutine(FireBullet());
            _timeElapsed = 0;
        }
        _timeElapsed += Time.fixedDeltaTime;
    }
	
	// Update is called once per frame
	void FixedUpdate ()
	{
	    Step += 0.07f;
	    if (Step > 999999)
	    {
	        Step = 1;
	    }
        transform.position = new Vector3(transform.position.x,Mathf.Sin(Step) + Offset);
	}

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Fireball"))
        {
            other.gameObject.SetActive(false);
            Health--;
            if (Health < 0)
            {
                StartCoroutine(WinTheGame());
            }
        }
    }

    IEnumerator WinTheGame()
    {
        WinText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3.0f);
        WinText.gameObject.SetActive(false);
        SceneManager.LoadScene("_MainScene");
    }

    IEnumerator FireBullet()
    {
        var myQuaternion = Quaternion.identity;
        myQuaternion *= Quaternion.Euler(Vector3.up * 180);
        //firstBullet
        var clone1 = Instantiate(Spike, ShootPositionOne.position, myQuaternion) as GameObject;
        clone1.GetComponent<Rigidbody2D>().velocity = new Vector2(-4,0);
        yield return new WaitForSeconds(3f);
        //secondBullet
        var clone2 = Instantiate(Spike, ShootPositionTwo.position, myQuaternion) as GameObject;
        clone2.GetComponent<Rigidbody2D>().velocity = new Vector2(-4, 0);
        Destroy(clone1,2.0f);
        Destroy(clone2, 2.0f);
    }
}
