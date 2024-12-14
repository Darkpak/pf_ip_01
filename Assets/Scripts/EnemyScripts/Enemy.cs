using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float lifespan = 10.0f;
    public int damage = 10;
    private float spawnTime;

    private void Start()
    {
        spawnTime = Time.time;
    }

    private void Update()
    {
        if (Time.time - spawnTime >= lifespan)
        {
            DamagePlayer();
            Destroy(gameObject);
        }
    }

    private void DamagePlayer()
    {
        PlayerHealth playerHealth = FindObjectOfType<PlayerHealth>();
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Bullet"))
        {
            GameManager.instance.AddKill(1);
            Destroy(gameObject);
            Destroy(collision.gameObject);
        }
        
    }

}