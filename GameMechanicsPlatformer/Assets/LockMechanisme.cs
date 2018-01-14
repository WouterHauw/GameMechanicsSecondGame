using UnityEngine;

public class LockMechanisme : MonoBehaviour
{
    public GameObject doorGameObject;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            doorGameObject.SetActive(false);
        }
    }
}
