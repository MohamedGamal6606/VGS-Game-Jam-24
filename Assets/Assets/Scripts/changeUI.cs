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
    public playerMovement pm;

    private int ammoCount = 7;
    private int healthCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthCount = pm.hp - 1;
    }

    public void MissionTimer()
    {
        
        int t = (int)Time.timeSinceLevelLoad;
        Timer.SetText(""+ t );
        Timer2.SetText("" + t );
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
    public void RefreshHealth(int hp)
    {
        if (hp>=3)
        {
            hp = 3;
        }
        for (int i = 0; i < hp; i++)
        {
            health[i].SetActive(true);
            healthDepleted[healthCount].SetActive(false);
        }

    }

}
