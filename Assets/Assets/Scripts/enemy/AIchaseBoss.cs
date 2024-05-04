using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIchaseBoss : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    [SerializeField] private float speed;

    private float distance;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        float explosionDistance = 0.5f;

        Vector3 direction = player.transform.position - transform.position;
        rb.velocity = new Vector2(direction.x, direction.y).normalized * speed;

        distance = Vector2.Distance(transform.position, player.transform.position);
        if (distance <= explosionDistance)
        {
            Destroy(gameObject);
        }
    }
}