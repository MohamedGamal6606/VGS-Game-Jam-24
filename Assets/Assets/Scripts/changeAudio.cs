using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changeAudio : MonoBehaviour
{

    public Slider main_volume;


    AudioSource src;
    
    // Start is called before the first frame update
    void Start()
    {
        src = gameObject.GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()
    {
        src.volume = main_volume.value;
    }
}
