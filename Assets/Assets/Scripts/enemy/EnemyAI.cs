using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float detectionRange = 10f; // Range within which the enemy can detect the player
    public float angleOffset = 45f; // Angle offset from player's direction
    public GameObject projectilePrefab; // Prefab of the projectile to shoot
    public float fireRate = 1f; // Rate of fire (projectiles per second)
    public float projectileSpeed = 10f; // Speed of the projectile

    public Transform projectileSpawnPoint; // Reference to the spawn point of projectiles

    private float nextFireTime; // Time when the enemy can fire next

    void Update()
    {
        // Check if the player is within detection range
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            // Calculate direction towards the player
            Vector3 directionToPlayer = player.position - transform.position;

            // Apply angle offset to the direction
            Quaternion rotationOffset = Quaternion.Euler(0, 0, angleOffset);
            Vector3 rotatedDirection = rotationOffset * directionToPlayer;

            // Rotate towards the player
            float angle = Mathf.Atan2(rotatedDirection.y, rotatedDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, angle);

         
        }
    }

    
}
