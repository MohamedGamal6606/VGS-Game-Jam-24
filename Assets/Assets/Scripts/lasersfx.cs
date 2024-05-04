using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lasersfx : MonoBehaviour
{
    public AudioClip[] lasers;
    public AudioSource src;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void playClip()
    {
        if (lasers.Length>0)
        {
            int r = Random.Range(0, lasers.Length);
            src.PlayOneShot(lasers[r]);
        }
        
    }

}
