using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawSpawnScript : MonoBehaviour
{
    public GameObject Saw;
    private GameObject _cloneSaw;
    [SerializeField]private float _timeleft = 10.0f;
    private float _originaltime;


    void Start()
    {
        _originaltime = _timeleft;
        _cloneSaw = Instantiate(Saw);
        _cloneSaw.transform.position = transform.position;
    }

    // Update is called once per frame
	void Update ()
	{
	    _timeleft -= Time.deltaTime;
	    if (_timeleft < 0)
	    {
	        _timeleft = _originaltime;
            _cloneSaw.SetActive(false);
	        _cloneSaw.gameObject.transform.position = transform.position;
            _cloneSaw.SetActive(true);
	    }
	}
}
