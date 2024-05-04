using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameWinScript : MonoBehaviour
{
    public playerMovement pm;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //if 4 batteries inserted
        if (pm.batteries_Inserted>=4)
        {
            winGame();
        }
    }


    public void winGame()
    {
        SceneManager.LoadScene("WinScreen");
    }


}
