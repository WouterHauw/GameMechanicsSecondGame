using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {

    public void GoToMainGame()
    {
        SceneManager.LoadScene("_MainScene");
    }
}
