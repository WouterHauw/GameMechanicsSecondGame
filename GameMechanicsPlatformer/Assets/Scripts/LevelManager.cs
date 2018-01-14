using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public GameObject respawnPoint;

    private PlayerInputScript player;

    private void Start()
    {
        player = FindObjectOfType<PlayerInputScript>();
    }
	

    public void RespawnPlayer()
    {
        Debug.Log("Player Respawn");
        player.transform.position = respawnPoint.transform.position;
    }
}
