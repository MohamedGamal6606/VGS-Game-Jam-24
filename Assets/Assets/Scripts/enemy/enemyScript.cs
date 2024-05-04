using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyScript : MonoBehaviour
{

    public float hp;
    public GameObject enemy;
    public GameObject enemy_death;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (hp<=0)
        {
            //handle enemy death
            enemy.SetActive(false);
            enemy_death.SetActive(true);

        }
    }

    public void getHit()
    {
        hp -= 50;
    }
    public void getPunched()
    {
        hp -= 100;
    }



}
