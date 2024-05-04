using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenuScript : MonoBehaviour
{
    public GameObject image;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startGame()
    {
        SceneManager.LoadScene("MainScene");
    }

    public void setActive()
    {
        image.SetActive(true);
    }
    public void DeactivateImage()
    {
        image.SetActive(false);
    }

    public void closeGame()
    {
        Application.Quit();
    }

}
