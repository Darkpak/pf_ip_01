using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GunScript : MonoBehaviour
{
    public Rigidbody bulletRigidbody;
    public Transform bulletSpawnPoint;
    public float bulletSpeed = 100.0f;
    public float timeToLive = 5;

    public int bulletMax = 6; 
    private int currentBulletCount = 0;
    public int totalBullets = 12; // Total bullets available
    public int maxTotalBullets = 36; // Maximum total bullets the player can carry

    // UI Elements
    public TMP_Text bulletCountText;
    public TMP_Text reloadPromptText;
    public Image crosshairImage;

    // Audio Elements
    public AudioSource shootAudioSource;
    public AudioSource reloadAudioSource;

    private void Start()
    {
        currentBulletCount = bulletMax;
        UpdateBulletCountUI();
        reloadPromptText.gameObject.SetActive(false);
        crosshairImage.gameObject.SetActive(true);
    }

    private void Update()
    {
        if (currentBulletCount > 0 && Input.GetMouseButtonDown(0))
        {
            Shoot();
        }

        if (currentBulletCount == 0)
        {
            reloadPromptText.gameObject.SetActive(true);
            crosshairImage.gameObject.SetActive(false);
            if (Input.GetKeyDown(KeyCode.R))
            {
                Reload();
            }
        }
        else
        {
            reloadPromptText.gameObject.SetActive(false);
            crosshairImage.gameObject.SetActive(true);
        }
    }

    private void Shoot()
    {
        Rigidbody r = Instantiate(bulletRigidbody, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        r.velocity = bulletSpawnPoint.forward * bulletSpeed;
        Destroy(r.gameObject, timeToLive);
        currentBulletCount--;
        UpdateBulletCountUI();
        shootAudioSource.Play();
    }

    private void Reload()
    {
        if (totalBullets > 0)
        {
            int bulletsNeeded = bulletMax - currentBulletCount;
            int bulletsToReload = Mathf.Min(bulletsNeeded, totalBullets);

            currentBulletCount += bulletsToReload;
            totalBullets -= bulletsToReload;

            UpdateBulletCountUI();
            reloadPromptText.gameObject.SetActive(false);        
            crosshairImage.gameObject.SetActive(true);
            reloadAudioSource.Play();
        }
    }
    public void CollectAmmo(int ammoAmount)
    {
        totalBullets = Mathf.Min(totalBullets + ammoAmount, maxTotalBullets);
        UpdateBulletCountUI();
    }

    private void UpdateBulletCountUI()
    {
        bulletCountText.text = $"Bullets: {currentBulletCount}/{totalBullets}";
    }
}