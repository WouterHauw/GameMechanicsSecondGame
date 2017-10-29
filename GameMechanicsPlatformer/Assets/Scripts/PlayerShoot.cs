using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    
    public float shootDelay = .5f;
    public GameObject projectilePrefab;
    public Transform ShootPosition;
    public int Ammunition;
    public Text AmmoText;
    public Slider Slider;
    public float CurrentAmmoBarValue { get; set; }
    public float MaxAmmoBarValue = 20f;

    [SerializeField] private int _addedAmmo = 53;
    private float _timeElapsed = 0f;

    void Start()
    {
        Ammunition = 10;
        CurrentAmmoBarValue = 0;
        AmmoText.text = Ammunition.ToString();
        AmmoText.gameObject.SetActive(false);
        Slider.value = CalculateAmmoBar();
        Slider.gameObject.SetActive(false);
        
    }


	// Update is called once per frame
	void Update () {

	    if (projectilePrefab == null)
	    {
	        return;
	    }
	    AmmoText.text = Ammunition.ToString();
	    if (Input.GetKeyDown(KeyCode.E) && _timeElapsed > shootDelay)
	    {
	        if (Ammunition > 0)
	        {
	            Ammunition--;
	            CreateProjectile(ShootPosition.position);
            }
	        _timeElapsed = 0;
	    }
	    CurrentAmmoBarValue += Time.fixedDeltaTime;
	    _timeElapsed += Time.deltaTime;
	    if (CurrentAmmoBarValue >= MaxAmmoBarValue)
	    {
	        Ammunition += _addedAmmo;
	        CurrentAmmoBarValue = 0;
	    }
	    Slider.value = CalculateAmmoBar();
	}

    public void CreateProjectile(Vector2 pos)
    {
        
        var clone = Instantiate(projectilePrefab, pos, Quaternion.identity) as GameObject;
       
    }

    float CalculateAmmoBar()
    {
        return CurrentAmmoBarValue / MaxAmmoBarValue;
    }

}
