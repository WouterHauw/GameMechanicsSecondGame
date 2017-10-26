using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxOpeningScript : MonoBehaviour
{

    public Animator animator;
    public GameObject projectilePrefab;

    void Start()
    {
        animator = GetComponent<Animator>();
    }






    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.name == "Player")
        {
            StartCoroutine(OpeningChest());
            var shootBehaviour = other.GetComponent<PlayerShoot>();
            shootBehaviour.projectilePrefab = projectilePrefab;
        }
    }

    IEnumerator OpeningChest()
    {
        animator.SetTrigger("OpeningChest");
        AnimatorStateInfo currInfo = animator.GetCurrentAnimatorStateInfo(0);
        yield return new WaitForSeconds(currInfo.normalizedTime);
        gameObject.SetActive(false);

    }
}
