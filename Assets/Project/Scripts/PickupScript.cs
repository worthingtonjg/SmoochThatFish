using UnityEngine;
using System.Collections;

public class PickupScript : MonoBehaviour {
    public enum EnumPickupType
    {
        Popsclicles,
        Fish
    }

    public EnumPickupType PickupType;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            if (PickupType == EnumPickupType.Fish)
                GameManagerScript.Instance.PickupFish();

            if (PickupType == EnumPickupType.Popsclicles)
                GameManagerScript.Instance.PickupPopscle();

            this.gameObject.SetActive(false);
        }
    }
}
