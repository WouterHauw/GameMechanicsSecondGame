using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    public bool canShoot { get; set; }
    public float shootDelay = .5f;
    public GameObject projectilePrefab;
    public Transform ShootPosition;

    private float timeElapsed = 0f;

	// Use this for initialization
	void Awake ()
	{
	    canShoot = false;
	}
	
	// Update is called once per frame
	void Update () {
	    if (projectilePrefab == null)
	    {
	        return;
	    }
	    if (Input.GetKeyDown(KeyCode.E) && timeElapsed > shootDelay)
	    {
	        CreateProjectile(ShootPosition.position);
	        timeElapsed = 0;
	    }

	    timeElapsed += Time.deltaTime;
	}

    public void CreateProjectile(Vector2 pos)
    {
        
        var clone = Instantiate(projectilePrefab, pos, Quaternion.identity) as GameObject;
       
        
    }
}
