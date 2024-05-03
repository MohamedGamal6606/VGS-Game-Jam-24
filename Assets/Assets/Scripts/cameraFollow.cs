using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{

    public Transform tr;
    public float followDistance = 1f;
    public float smoothTime = 0.3f; // Smoothing time
    public playerMovement pm;
    


    private Vector3 velocity = Vector3.zero;
    private int timer = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer = (int)Time.time;
        Vector3 res = transform.position - tr.position;
        if (res.x >= followDistance || res.y>= followDistance || res.x <= -followDistance || res.y <= -followDistance)
        {
            Vector3 temp = new Vector3(tr.position.x, tr.position.y, -10);
            this.transform.position = Vector3.SmoothDamp(transform.position, temp, ref velocity, smoothTime);
        }
        if(timer%2 == 1 && !pm.moving)
        {
            Vector3 temp = new Vector3(tr.position.x, tr.position.y, -10);
            this.transform.position = Vector3.SmoothDamp(transform.position, temp, ref velocity, smoothTime);
            
        }
        if (pm.moving)
        {
            timer = 0;
        }
        
    }
}
