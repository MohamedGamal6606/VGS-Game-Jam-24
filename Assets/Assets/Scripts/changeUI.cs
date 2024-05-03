using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class changeUI : MonoBehaviour
{

    public TMP_Text Timer;
    public TMP_Text Timer2;
    [SerializeField] GameObject[] bullets;
    [SerializeField] GameObject[] health;
    [SerializeField] GameObject[] healthDepleted;

    private int ammoCount = 7;
    private int healthCount = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DefenseMissionTimer()
    {
        int full_timer = 30;
        int t = (int)Time.timeSinceLevelLoad;
        int temp = (full_timer - t);
        if(temp <= 0)
        {
            temp = 0;
        }
        Timer.SetText(""+ temp );
        Timer2.SetText("" + temp );
    }


    public void DecreaseAmmo()
    {
        if (ammoCount >= 0)
        {
            bullets[ammoCount].SetActive(false);
            ammoCount--;
        }
    }
    public void ReloadAmmo()
    {
        ammoCount = 7;
        for(int i = 0; i < bullets.Length; i++)
        {
            bullets[i].SetActive(true);
        }

    }
    public void DecreaseHealth()
    {
        if (healthCount >= 0)
        {
            health[healthCount].SetActive(false);
            healthDepleted[healthCount].SetActive(true);
            healthCount--;
            
        }
    }
    public void RefreshHealth()
    {
        healthCount = 2;
        for (int i = 0; i < health.Length; i++)
        {
            health[i].SetActive(true);
            healthDepleted[healthCount].SetActive(false);
        }

    }

}
