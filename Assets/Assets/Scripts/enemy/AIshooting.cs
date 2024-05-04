using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIshooting : MonoBehaviour
{
    public Transform player; // Reference to the player's transform
    public float detectionRange = 10f; // Range within which the enemy can detect the player
    public GameObject projectilePrefab; // Prefab of the projectile to shoot
    public float projectileSpeed = 10f; // Speed of the projectile

    void Update()
    {
        // Check if the player is within detection range
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            // Rotate towards the player
            Vector3 direction = player.position - transform.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            // Shoot at the player
            Shoot();
        }
    }

    void Shoot()
    {
        // Instantiate projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

        // Set projectile velocity
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * projectileSpeed; // Assuming the projectile moves along the object's local right direction

        // Destroy projectile after some time (to prevent cluttering)
        Destroy(projectile, 3f);

        // Destroy the enemy game object after 1 second
        Destroy(gameObject, 1f);
    }
}