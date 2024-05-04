using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class insertPart : MonoBehaviour
{
    public playerMovement pm;
    public bool part;
    public bool inserted;
    public SpriteRenderer sr;
    public Sprite changedShape;
    // Start is called before the first frame update
    void Start()
    {
        sr = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        if (pm.parts > 0)
        {
            part = true;
        }
        else
        {
            part = false;
        }



    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (part && !inserted)
            {
                
                sr.sprite = changedShape;
                pm.parts--;
                pm.batteries_Inserted++;
                inserted = true;
            }
            
                
        }
    }

}
