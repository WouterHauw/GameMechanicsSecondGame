using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyWall : MonoBehaviour
{

    public Color[] colors;
    [SerializeField] private int health = 4;
    private SpriteRenderer rend;

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Fireball"))
        {
            other.gameObject.SetActive(false);
            health--;
            if (health <= 0)
            {
                gameObject.SetActive(false);
                return;
            }
            rend.color = colors[health - 1];
        }
    }
}
//0 is colour 1