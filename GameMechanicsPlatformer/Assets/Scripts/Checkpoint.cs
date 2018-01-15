using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Checkpoint : MonoBehaviour
{
    public LevelManager levelManager;
    public Text checkpointReachedText;

	// Use this for initialization
	void Start ()
	{
	    levelManager = FindObjectOfType<LevelManager>();
	}
    

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            levelManager.respawnPoint = gameObject;
            StartCoroutine(SetTextCheckpoint());
        }
    }
    IEnumerator SetTextCheckpoint()
    {
        checkpointReachedText.gameObject.SetActive(true);
        checkpointReachedText.text = "Checkpoint Reached";
        yield return new WaitForSeconds(2.0f);
        checkpointReachedText.gameObject.SetActive(false);

    }


}
