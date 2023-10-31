using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public EventLoader el;

    private void Start()
    {
        GameObject unoTimeslol = GameObject.Find("PlayerCardHandler");
        if ((unoTimeslol != null) && (SceneManager.GetActiveScene().name == "Main Menu"))
        {
            Destroy(unoTimeslol);
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void startGame()
    {
        SceneManager.LoadScene(1);
    }

    public void mainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
