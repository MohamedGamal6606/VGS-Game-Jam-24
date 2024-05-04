using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    public Rigidbody2D rb;
    public float speed = 5f;
    public float fireRate = 0.5f; 
    public LayerMask enemyLayer;
    public int bullets = 8;
    public float footstepDelay = 0.5f; // Minimum time between footstep sounds
    public weapon weaponScript;
    public GameObject weaponObject;
    public changeUI uiScript;
    public int hp = 3;
    public GameObject shotPosition;
    public bool moving;
    public Texture2D crosshairTexture;
    public int parts = 0;
    public Slider sfx;
    public bool main_menu = false;
    public GameObject menu;
    public int batteries_Inserted = 0;
    //public AudioSource src2;

    [SerializeField] float destroy_timer = 3f;
    [SerializeField] GameObject[] projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] List<AudioClip> gunsounds = new List<AudioClip>();
    [SerializeField] List<AudioClip> footsteps = new List<AudioClip>();
    [SerializeField] List<AudioClip> reloadSounds = new List<AudioClip>();
    [SerializeField] List<AudioClip> grunt = new List<AudioClip>();
    [SerializeField] AudioClip punch;

    private AudioSource src;
    private Animator anim;
    private Transform teleport;
    private float nextFireTime = 0f;
    private GameObject checkpoint;
    private float nextFootstepTime; // Time when the next footstep can be played
    private bool weapon = false;
    private bool punching;
    Collider2D hitbox;
    ContactFilter2D enemyFilter;
    // Start is called before the first frame update
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        src = gameObject.GetComponent<AudioSource>();
        teleport = null;

        hitbox = transform.GetChild(1).GetComponent<Collider2D>();
        enemyFilter = new ContactFilter2D();
        enemyFilter.SetLayerMask(LayerMask.GetMask("Enemies"));
        enemyFilter.useLayerMask = true;

    }

    // Update is called once per frame
    void Update()
    {
        LookAtMouse();
        nextFootstepTime -= Time.deltaTime;
        float Horizontal = Input.GetAxis("Horizontal");
        float Vertical = Input.GetAxis("Vertical");
        if (Horizontal != 0 || Vertical!= 0)
        {
            moving = true;
            anim.SetBool("isWalking",true);
            
            
        }
        else
        {
            moving = false;
            anim.SetBool("isWalking", false);
        }
        if (hp<=0)
        {
            SceneManager.LoadScene("LoseScreen");
        }
        //opening and closing menu
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (main_menu)
            {
                menu.SetActive(false);
                main_menu = false;
            }
            else
            {
                menu.SetActive(true);
                main_menu = true;
            }

        }
        //if main menu not opened walk normally
        if (!main_menu)
        {
            rb.velocity = new Vector2(Horizontal * speed, Vertical * speed);


            uiScript.RefreshHealth(hp);


            //footsteps
            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D))
            {
                PlayFootstepSound();
            }

            //punch
            if (Input.GetMouseButtonDown(1))
            {
                anim.SetBool("isPunching", true);
                
                //punch method
                src.PlayOneShot(punch);

            }
            else
            {
                anim.SetBool("isPunching", false);
                
            }
            //main menu
            src.volume = sfx.value;
            //src2.volume = sfx.value;



            //pickup weapon
            if (!weapon)
            {
                if (Input.GetKeyDown(KeyCode.Q))
                {

                    if (weaponScript.weaponRadius)
                    {
                        weapon = true;
                        anim.SetBool("gun", true);
                        anim.SetTrigger("pickupGun");
                        Destroy(weaponObject);
                        int r = Random.Range(0, reloadSounds.Count);
                        src.PlayOneShot(reloadSounds[r]);

                    }

                }
            }



            uiScript.MissionTimer();
            // Shoot projectile
            if (Input.GetMouseButtonDown(0) && Time.timeSinceLevelLoad >= nextFireTime) // Change 0 to 1 for right-click or 2 for middle-click
            {
                if (weapon)
                {
                    if (bullets != 0)
                    {

                        if (gunsounds.Count > 0)
                        {
                            //play sounds
                            int r = Random.Range(0, gunsounds.Count);
                            src.PlayOneShot(gunsounds[r]);
                            //projectile shooting
                            ShootProjectile();
                            nextFireTime = Time.timeSinceLevelLoad + fireRate;
                            uiScript.DecreaseAmmo();
                            bullets--;
                        }

                    }
                }



            }
            //put checkpoint
            if (Input.GetKeyDown(KeyCode.E))
            {
                if (checkpoint == null)
                {
                    checkpoint = Instantiate(projectilePrefab[1], transform.position, Quaternion.identity);
                    teleport = checkpoint.transform;
                    teleport.position = checkpoint.transform.position;
                }
                else
                {
                    checkpoint.transform.position = transform.position;
                    teleport = checkpoint.transform;
                    teleport.position = checkpoint.transform.position;
                }

            }
            //teleport
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if (teleport != null)
                {
                    transform.position = teleport.position;
                }

            }

            //reload
            if (Input.GetKeyDown(KeyCode.R))
            {

                reload();
            }

        }
    }

    void reload()
    {
        //check if there is weapon
        if (weapon)
        {
            //play reload sound
            int r = Random.Range(0, reloadSounds.Count);
            src.PlayOneShot(reloadSounds[r]);
            
            bullets = 8;
            //reload UI
            uiScript.ReloadAmmo();
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
        GameObject projectile = Instantiate(projectilePrefab[0], shotPosition.transform.position, Quaternion.identity);

        // Calculate direction towards cursor
        Vector3 cursorPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        cursorPosition.z = 0f;
        Vector3 direction = cursorPosition - transform.position;
        direction.z = 0f;
        direction.Normalize();

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
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
            hit.collider.GetComponent<enemyScript>().getHit();
        }
        Destroy(projectile, destroy_timer);
    }


    

    // Function to play a footstep sound if enough time has passed since the last footstep
    public void PlayFootstepSound()
    {
        // Check if it's time to play a footstep sound
        if (nextFootstepTime <= 0 && footsteps.Count > 0 && src != null)
        {
            // Choose a random footstep clip from the array
            AudioClip footstepClip = footsteps[Random.Range(0, footsteps.Count)];

            // Play the chosen footstep clip
            src.PlayOneShot(footstepClip);

            // Update the next allowed footstep time
            nextFootstepTime = footstepDelay;
        }
    }

    public void getHit()
    {
        hp--;
        uiScript.DecreaseHealth();
        //play grunt sound
        if (grunt.Count > 0)
        {
            int r = Random.Range(0, grunt.Count);
            src.PlayOneShot(grunt[r]);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            getHit();
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer.Equals("Enemies")&& punching)
        {
            collision.gameObject.GetComponent<enemyScript>().getPunched();
        }
    }

    public void punchStart()
    {
        punching = true;
    }
    public void punchEnd()
    {
        punching = false;
    }

    public void puncher()
    {
        hitbox.enabled = true;
        Collider2D[] enemiesToDamage = new Collider2D[10];
        Physics2D.OverlapCollider(hitbox, enemyFilter, enemiesToDamage);

        foreach (Collider2D enemy in enemiesToDamage)
        {
            if (enemy != null)
            {
                enemy.GetComponent<enemyScript>().getPunched();
            }
        }
        hitbox.enabled = false;
    }
}
