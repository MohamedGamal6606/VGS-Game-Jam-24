using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour
{
    public GameObject TutScreen;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void HowToPlay()
    {
        TutScreen.SetActive(true);
    }
    public void closeTutorial()
    {
        TutScreen.SetActive(false);
    }

}
