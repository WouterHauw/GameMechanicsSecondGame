using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{

    public string targetTag = "Player";

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.tag == targetTag)
        {
            
        }
    }


}
