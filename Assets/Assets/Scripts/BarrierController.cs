using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrierController : MonoBehaviour
{



    [SerializeField] GameObject[] parts;
    [SerializeField] GameObject[] barriers;
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (parts[0]==null && parts[3]==null)
        {
            DeactivateBarriers(0);
        }
        if (parts[1] == null)
        {
            DeactivateBarriers(1);
        }
        if (parts[2] == null)
        {
            DeactivateBarriers(2);
        }
    }


    public void DeactivateBarriers(int x)
    {
        if (x < barriers.Length)
        {
            barriers[x].SetActive(false);
        }
        
    }
    public void ActivateBarriers(int x)
    {
        if (x < barriers.Length)
        {
            barriers[x].SetActive(true);
        }
            
    }

    public void DeactivateAllBarriers()
    {
        for (int i = 0; i < barriers.Length; i++)
        {
            barriers[i].SetActive(false);
        }
            
        

    }
    public void ActivateAllBarriers()
    {
        for (int i = 0; i < barriers.Length; i++)
        {
            barriers[i].SetActive(true);
        }
    }


}
