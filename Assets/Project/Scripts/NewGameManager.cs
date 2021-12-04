using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class NewGameManager : MonoBehaviour {
    public int StartingLives = 3;

	// Use this for initialization
	void Start () {
        PlayerPrefsManager.SetLives(StartingLives);
        PlayerPrefsManager.SetFish(0);
        PlayerPrefsManager.SetPopscicles(0);
	}

    public void StartGame()
    {
        SceneManager.LoadScene("level01");
    }
}
