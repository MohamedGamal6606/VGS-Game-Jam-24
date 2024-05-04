using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossScript : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform bulletPos;
    [SerializeField] private GameObject kjBot;
    [SerializeField] private Transform kjBotPos;
    private float bulletTimer;
    private float kjTimer;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        bulletTimer += Time.deltaTime;
        kjTimer += Time.deltaTime;
        float shootingCooldown = 1f;
        float kjBotCooldown = 5f;
        if (bulletTimer > shootingCooldown)
        {
            bulletTimer = 0;
            shoot();
        }
        if (kjTimer > kjBotCooldown)
        {
            kjTimer = 0;
            kjBotSpawn();
        }

    }

    void shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
    }

    void kjBotSpawn()
    {
        Instantiate(kjBot, kjBotPos.position, Quaternion.identity);
    }
}