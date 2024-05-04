using UnityEngine;

public class bulletScript : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public GameObject projectilePrefab; // Prefab of the projectile to shoot
    public float detectionRange = 10f; // Range within which the enemy can detect the player
    public float fireRate = 1f; // Rate of fire (projectiles per second)
    public float projectileSpeed = 10f; // Speed of the projectile
    public float destroy_timer = 1f;
    public enemyScript par;
    
    private float nextFireTime; // Time when the enemy can fire next
    

    void Update()
    {
        // Check if the player is within detection range
        if (par.hp>0)
        {
            if (Vector3.Distance(transform.position, player.position) <= detectionRange)
            {
                // Check if it's time to fire
                if (Time.time >= nextFireTime)
                {
                    // Shoot towards the player
                    Shoot();
                    //laser.playClip();
                    // Update next fire time
                    nextFireTime = Time.time + 1f / fireRate; // Calculate next fire time based on fire rate
                }
            }
        }
        
    }

    void Shoot()
    {
        // Calculate direction towards the player
        Vector3 directionToPlayer = (player.position - transform.position).normalized;

        // Instantiate projectile from the spawn point
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Set projectile velocity
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = directionToPlayer * projectileSpeed;
        Destroy(projectile, destroy_timer);
    }
}
