using UnityEngine;
using System.Collections;

public class SnowballScript : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy")
        {
            //other.gameObject.SetActive(false);
            GameManagerScript.Instance.PlayHitClip();
            other.SendMessage("Die", SendMessageOptions.RequireReceiver);
        }

        if (other.tag != "Player" && other.tag != "Waypoint")
        {
            GameObject.Destroy(this.gameObject);
            
        }
    }
}
