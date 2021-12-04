using UnityEngine;
using System.Collections;

public class PenguinDestroyer : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            GameManagerScript.Instance.LoseLife();
        }
    }
}
