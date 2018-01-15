using System;
using UnityEngine;

public class PlayerShoot : MonoBehaviour
{
    
    public float shootDelay = .5f;
    public GameObject projectilePrefab;
    public Transform shootPosition;

    [SerializeField] private int _addedAmmo = 53;
    private float _timeElapsed = 0f;




	// Update is called once per frame
	void Update () {

	    if (projectilePrefab == null)
	    {
	        return;
	    }
	    //AmmoText.text = Ammunition.ToString();
	    if (Input.GetKeyDown(KeyCode.E) && _timeElapsed > shootDelay)
	    {
	        
	            CreateProjectile(shootPosition.position);
	        _timeElapsed = 0;
	    }
	    _timeElapsed += Time.deltaTime;
	}

    public void CreateProjectile(Vector2 pos)
    {

        var clone = Instantiate(projectilePrefab, pos, transform.rotation);
        if (gameObject.transform.localScale.x < 0)
        {
            clone.GetComponent<Rigidbody2D>().velocity = clone.transform.right * -6;
        }
        else
        {
            clone.GetComponent<Rigidbody2D>().velocity = clone.transform.right * 6;
        }
        Destroy(clone, 2.0f);

    } 

}
