using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenuHandler : MonoBehaviour
{
    public GameObject pauseMenuCurtain;

    // Start is called before the first frame update
    public void HamburgerMenuButton()
    {
        pauseMenuCurtain.SetActive(true);
    }

    public void ResumeGame()
    {
        pauseMenuCurtain.SetActive(false);
    }

    public void HelpMenu()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
