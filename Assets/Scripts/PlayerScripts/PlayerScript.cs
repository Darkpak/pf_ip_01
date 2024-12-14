using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public GunScript gunScript; // Reference to the GunScript

    public void CollectAmmo(int ammoAmount)
    {
        if (gunScript != null)
        {
            gunScript.CollectAmmo(ammoAmount);
        }
    }
}