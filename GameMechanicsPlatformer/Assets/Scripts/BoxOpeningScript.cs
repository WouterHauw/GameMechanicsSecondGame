using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoxOpeningScript : MonoBehaviour
{

    public Animator animator;
    public GameObject projectilePrefab;
    public Text AmmoText;
    public Text PressE;
    public Slider AmmoSlider;

    void Start()
    {
        animator = GetComponent<Animator>();
        AmmoText.gameObject.SetActive(false);
        AmmoSlider.gameObject.SetActive(false);
        PressE.gameObject.SetActive(false);
    }






    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            StartCoroutine(OpeningChest());
            var shootBehaviour = other.GetComponent<PlayerShoot>();
            shootBehaviour.projectilePrefab = projectilePrefab;
            AmmoText.gameObject.SetActive(true);
            AmmoSlider.gameObject.SetActive(true);
        }
    }

    IEnumerator OpeningChest()
    {
        animator.SetTrigger("OpeningChest");
        AnimatorStateInfo currInfo = animator.GetCurrentAnimatorStateInfo(0);
        PressE.gameObject.SetActive(true);
        yield return new WaitForSeconds(currInfo.normalizedTime);
        gameObject.SetActive(false);
        PressE.gameObject.SetActive(false);

    }
}
