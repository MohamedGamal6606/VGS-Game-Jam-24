using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    [SerializeField] float speed = 5f;
    public GameObject projectilePrefab;
    public float projectileSpeed = 10f;
    public float fireRate = 0.5f; 
    public LayerMask enemyLayer;



    private float nextFireTime = 0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        LookAtMouse();
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");

        rb.velocity = new Vector2(Horizontal * speed, Vertical * speed);

        // Shoot projectile
        if (Input.GetMouseButton(0)&& Time.time >= nextFireTime) // Change 0 to 1 for right-click or 2 for middle-click
        {
           
            ShootProjectile();
            nextFireTime = Time.time + fireRate;
        }


    }

    void LookAtMouse()
    {
        // Get the position of the cursor in world space
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0f; // Assuming z-coordinate is 0 in 2D

        // Calculate the direction from the character to the cursor
        Vector3 direction = cursorPosition - transform.position;

        // Ensure the character only rotates in the 2D plane
        direction.z = 0f;

        // Rotate the character towards the cursor
        if (direction != Vector3.zero)
        {
            transform.up = direction;
        }
    }
    void ShootProjectile()
    {
        // Instantiate projectile
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.identity);

        // Calculate direction towards cursor
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0f;
        Vector3 direction = cursorPosition - transform.position;
        direction.z = 0f;
        direction.Normalize();

        // Set projectile velocity
        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>(); // Use Rigidbody for 3D
        rb.velocity = direction * projectileSpeed;

        // Raycast to detect hits
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, Mathf.Infinity, enemyLayer); // Use Physics.Raycast for 3D
        if (hit.collider != null)
        {
            // Enemy hit, handle damage
            Debug.Log("Enemy hit: " + hit.collider.name);
            // Apply damage to enemy
            // For example, you might have a script on the enemy to handle health and damage
            // hit.collider.GetComponent<EnemyHealth>().TakeDamage(damageAmount);
        }
    }

}
