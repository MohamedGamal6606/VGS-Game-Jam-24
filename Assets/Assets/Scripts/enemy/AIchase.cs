using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIchase : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] float chasingDistance = 4f;
    [SerializeField] float explosionDistance = 0.5f;
     [SerializeField]float distance;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 direction = player.transform.position - transform.position;
        direction.Normalize();
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        


        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= chasingDistance)
        {

            rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;
            transform.rotation = Quaternion.Euler(Vector3.forward * angle);
            if (distance <= explosionDistance)
            {
                player.GetComponent<playerMovement>().getHit();
                Destroy(gameObject);
            }
        }
    }
}