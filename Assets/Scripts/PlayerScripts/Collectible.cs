using UnityEngine;

public class AmmoCollectible : MonoBehaviour
{
    public int ammoAmount = 10;

    private void OnTriggerEnter(Collider other)
    {
        PlayerScript playerScript = other.GetComponent<PlayerScript>();

        if (playerScript != null)
        {
            playerScript.CollectAmmo(ammoAmount);
            Destroy(gameObject);
        }
    }
}
