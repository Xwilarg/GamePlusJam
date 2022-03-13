using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MenuScript : MonoBehaviour
{
    public void playGame() 
    {
        SceneManager.LoadScene(0);
    }

    public void quitGame() 
    {
        Application.Quit();
    }

}
